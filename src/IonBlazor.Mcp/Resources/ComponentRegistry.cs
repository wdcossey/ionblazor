using System.Reflection;
using IonBlazor.Abstractions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.Mcp.Resources;

public static class ComponentRegistry
{
    private static readonly Lazy<IReadOnlyList<Type>> _componentTypes = new(DiscoverComponents);
    private static readonly Lazy<IReadOnlyDictionary<string, Type>> _componentTypesByName =
        new(() => _componentTypes.Value.ToDictionary(GetDisplayName, t => t, StringComparer.Ordinal));

    public static IReadOnlyList<ComponentSummary> ListAll() =>
        _componentTypes.Value
            .Select(BuildSummary)
            .OrderBy(s => s.Name, StringComparer.Ordinal)
            .ToList();

    public static ComponentMetadata? GetMetadata(string componentName)
    {
        if (!_componentTypesByName.Value.TryGetValue(componentName, out Type? type))
            return null;

        return BuildMetadata(type);
    }

    private static IReadOnlyList<Type> DiscoverComponents()
    {
        var assembly = typeof(IonComponent).Assembly;
        return assembly.GetTypes()
            .Where(t => t.IsClass
                        && !t.IsAbstract
                        && t.IsPublic
                        && typeof(IonComponent).IsAssignableFrom(t)
                        && t.Namespace == "IonBlazor.Components")
            .ToList();
    }

    private static ComponentSummary BuildSummary(Type type) => new(
        Name: GetDisplayName(type),
        BaseClass: GetBaseClassName(type),
        HasChildContent: HasChildContent(type),
        HasJsInterop: typeof(IonJsComponent).IsAssignableFrom(type),
        JsImportName: GetJsImportName(type),
        Interfaces: GetIonInterfaces(type),
        BindProperties: GetBinds(type).Select(b => b.PropertyName).ToList(),
        Description: XmlDocReader.GetSummary(type));

    private static ComponentMetadata BuildMetadata(Type type) => new(
        Name: GetDisplayName(type),
        FullName: type.FullName ?? type.Name,
        BaseClass: GetBaseClassName(type),
        HasChildContent: HasChildContent(type),
        HasJsInterop: typeof(IonJsComponent).IsAssignableFrom(type),
        JsImportName: GetJsImportName(type),
        Interfaces: GetIonInterfaces(type),
        Parameters: GetParameters(type),
        CascadingParameters: GetCascadingParameters(type),
        Events: GetEvents(type),
        Binds: GetBinds(type),
        JsMethods: GetJsMethods(type),
        Description: XmlDocReader.GetSummary(type));

    private static string GetDisplayName(Type type)
    {
        if (!type.IsGenericType)
            return type.Name;

        var baseName = type.Name[..type.Name.IndexOf('`')];
        var args = string.Join(", ", type.GetGenericArguments().Select(a => a.Name));
        return $"{baseName}<{args}>";
    }

    private static string GetBaseClassName(Type type)
    {
        if (typeof(IonJsContentComponent).IsAssignableFrom(type)) return nameof(IonJsContentComponent);
        if (typeof(IonJsComponent).IsAssignableFrom(type)) return nameof(IonJsComponent);
        if (typeof(IonContentComponent).IsAssignableFrom(type)) return nameof(IonContentComponent);
        return nameof(IonComponent);
    }

    private static bool HasChildContent(Type type) =>
        typeof(IonContentComponent).IsAssignableFrom(type)
        || typeof(IonJsContentComponent).IsAssignableFrom(type);

    private static string? GetJsImportName(Type type)
    {
        if (!typeof(IonJsComponent).IsAssignableFrom(type))
            return null;

        // JsImportName is `internal abstract string` overridden as `=> nameof(ConcreteType)` by convention.
        // The convention is consistent enough that we surface the type's display name here.
        return GetDisplayName(type);
    }

    private static IReadOnlyList<string> GetIonInterfaces(Type type)
    {
        var ionAbstractionsAssembly = typeof(IonComponent).Assembly;
        return type.GetInterfaces()
            .Where(i => i.Assembly == ionAbstractionsAssembly && i.Name.StartsWith("IIon", StringComparison.Ordinal))
            .Where(i => i.Name is not nameof(IIonComponent) and not nameof(IIonContentComponent))
            .Select(i => i.Name)
            .OrderBy(n => n, StringComparer.Ordinal)
            .ToList();
    }

    private static IReadOnlyList<PropertyInfo> GetParameterProperties(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<ParameterAttribute>() is not null)
            .ToList();

    private static IReadOnlyList<ComponentParameter> GetParameters(Type type)
    {
        var nullCtx = new NullabilityInfoContext();
        return GetParameterProperties(type)
            .Where(p => !IsEventCallback(p.PropertyType))
            .Select(p => new ComponentParameter(p.Name, FormatType(p, nullCtx), XmlDocReader.GetSummary(p)))
            .OrderBy(p => p.Name, StringComparer.Ordinal)
            .ToList();
    }

    private static IReadOnlyList<ComponentCascadingParameter> GetCascadingParameters(Type type)
    {
        var nullCtx = new NullabilityInfoContext();
        return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<CascadingParameterAttribute>() is not null)
            .Where(p => p.GetMethod is { IsPublic: true } || p.SetMethod is { IsPublic: true })
            .Select(p => new ComponentCascadingParameter(
                p.Name,
                FormatType(p, nullCtx),
                p.GetCustomAttribute<CascadingParameterAttribute>()?.Name,
                XmlDocReader.GetSummary(p)))
            .OrderBy(p => p.Name, StringComparer.Ordinal)
            .ToList();
    }

    private static IReadOnlyList<ComponentEvent> GetEvents(Type type)
    {
        var nullCtx = new NullabilityInfoContext();
        return GetParameterProperties(type)
            .Where(p => IsEventCallback(p.PropertyType))
            .Select(p => new ComponentEvent(p.Name, FormatEventPayload(p, nullCtx), XmlDocReader.GetSummary(p)))
            .OrderBy(e => e.Name, StringComparer.Ordinal)
            .ToList();
    }

    private static string? FormatEventPayload(PropertyInfo prop, NullabilityInfoContext nullCtx)
    {
        if (prop.PropertyType == typeof(EventCallback))
            return null;

        var payloadType = prop.PropertyType.GetGenericArguments()[0];
        var formatted = FormatType(payloadType);
        if (IsValueType(payloadType))
            return formatted;

        var info = nullCtx.Create(prop);
        var payloadInfo = info.GenericTypeArguments.Length > 0 ? info.GenericTypeArguments[0] : null;
        return payloadInfo?.ReadState == NullabilityState.Nullable && !formatted.EndsWith('?')
            ? formatted + "?"
            : formatted;
    }

    private static IReadOnlyList<ComponentBind> GetBinds(Type type)
    {
        var paramProps = GetParameterProperties(type);
        var valueProps = paramProps
            .Where(p => !IsEventCallback(p.PropertyType))
            .ToDictionary(p => p.Name, StringComparer.Ordinal);

        var callbackNames = paramProps
            .Where(p => IsEventCallback(p.PropertyType))
            .Select(p => p.Name)
            .ToHashSet(StringComparer.Ordinal);

        var binds = new List<ComponentBind>();
        foreach (var (propName, propInfo) in valueProps)
        {
            var changedName = propName + "Changed";
            if (!callbackNames.Contains(changedName))
                continue;

            var inputName = propName + "Input";
            binds.Add(new ComponentBind(
                propName,
                changedName,
                callbackNames.Contains(inputName) ? inputName : null,
                XmlDocReader.GetSummary(propInfo)));
        }

        return binds.OrderBy(b => b.PropertyName, StringComparer.Ordinal).ToList();
    }

    private static IReadOnlyList<ComponentMethod> GetJsMethods(Type type)
    {
        if (!typeof(IonJsComponent).IsAssignableFrom(type))
            return [];

        var nullCtx = new NullabilityInfoContext();
        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)
            .Where(m => m.GetCustomAttribute<ObsoleteAttribute>() is null)
            .Where(m => m.Name is not nameof(IAsyncDisposable.DisposeAsync))
            .Where(IsAsyncReturn)
            .Select(m => new ComponentMethod(
                m.Name,
                FormatType(m.ReturnType),
                m.GetParameters()
                    .Select(p => new ComponentMethodParameter(p.Name ?? "_", FormatParameterType(p, nullCtx)))
                    .ToList(),
                XmlDocReader.GetSummary(m)))
            .OrderBy(m => m.Name, StringComparer.Ordinal)
            .ToList();
    }

    private static bool IsAsyncReturn(MethodInfo m)
    {
        var rt = m.ReturnType;
        if (rt == typeof(Task) || rt == typeof(ValueTask))
            return true;
        if (!rt.IsGenericType)
            return false;
        var genericDef = rt.GetGenericTypeDefinition();
        return genericDef == typeof(Task<>) || genericDef == typeof(ValueTask<>);
    }

    private static bool IsEventCallback(Type t)
    {
        if (t == typeof(EventCallback))
            return true;
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(EventCallback<>);
    }

    private static string FormatType(PropertyInfo prop, NullabilityInfoContext nullCtx)
    {
        var formatted = FormatType(prop.PropertyType);
        if (IsValueType(prop.PropertyType))
            return formatted;

        var info = nullCtx.Create(prop);
        return info.ReadState == NullabilityState.Nullable && !formatted.EndsWith('?')
            ? formatted + "?"
            : formatted;
    }

    private static string FormatParameterType(ParameterInfo param, NullabilityInfoContext nullCtx)
    {
        var formatted = FormatType(param.ParameterType);
        if (IsValueType(param.ParameterType))
            return formatted;

        var info = nullCtx.Create(param);
        return info.WriteState == NullabilityState.Nullable && !formatted.EndsWith('?')
            ? formatted + "?"
            : formatted;
    }

    private static bool IsValueType(Type t) =>
        t.IsValueType && (!t.IsGenericType || t.GetGenericTypeDefinition() != typeof(Nullable<>));

    private static string FormatType(Type t)
    {
        if (Nullable.GetUnderlyingType(t) is Type underlying)
            return FormatType(underlying) + "?";

        if (t == typeof(void)) return "void";
        if (t == typeof(string)) return "string";
        if (t == typeof(bool)) return "bool";
        if (t == typeof(int)) return "int";
        if (t == typeof(long)) return "long";
        if (t == typeof(double)) return "double";
        if (t == typeof(float)) return "float";
        if (t == typeof(decimal)) return "decimal";
        if (t == typeof(object)) return "object";

        if (!t.IsGenericType)
            return t.Name;

        var baseName = t.Name[..t.Name.IndexOf('`')];
        var args = string.Join(", ", t.GetGenericArguments().Select(FormatType));
        return $"{baseName}<{args}>";
    }
}
