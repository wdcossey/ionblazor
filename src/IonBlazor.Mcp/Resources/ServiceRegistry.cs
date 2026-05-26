using System.Reflection;
using IonBlazor.Abstractions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.Mcp.Resources;

public static class ServiceRegistry
{
    private const string ServicesNamespace = "IonBlazor.Services";

    private static readonly Lazy<IReadOnlyList<Type>> _serviceTypes = new(DiscoverServices);
    private static readonly Lazy<IReadOnlyDictionary<string, Type>> _serviceTypesByName =
        new(() => _serviceTypes.Value.ToDictionary(t => t.Name, t => t, StringComparer.Ordinal));

    public static IReadOnlyList<ServiceSummary> ListAll() =>
        _serviceTypes.Value
            .Select(BuildSummary)
            .OrderBy(s => s.Name, StringComparer.Ordinal)
            .ToList();

    public static ServiceMetadata? GetMetadata(string serviceName)
    {
        if (!_serviceTypesByName.Value.TryGetValue(serviceName, out Type? type))
            return null;

        return BuildMetadata(type);
    }

    private static IReadOnlyList<Type> DiscoverServices()
    {
        var assembly = typeof(IonComponent).Assembly;
        return assembly.GetTypes()
            .Where(t => t.IsClass
                        && !t.IsAbstract
                        && t.IsSealed
                        && t.IsPublic
                        && t.Namespace == ServicesNamespace
                        && !typeof(IonComponent).IsAssignableFrom(t)
                        && t.GetCustomAttribute<ObsoleteAttribute>() is null
                        && IsServiceShape(t))
            .ToList();
    }

    private static bool IsServiceShape(Type t)
    {
        // Legacy: ComponentBase + static methods. Injected: anything else with a usable instance method.
        if (typeof(ComponentBase).IsAssignableFrom(t))
            return GetCallableMethods(t, instance: false).Count > 0;
        return GetCallableMethods(t, instance: true).Count > 0;
    }

    private static ServiceKind GetKind(Type t) =>
        typeof(ComponentBase).IsAssignableFrom(t) ? ServiceKind.Legacy : ServiceKind.Injected;

    private static ServiceSummary BuildSummary(Type type)
    {
        var kind = GetKind(type);
        var methods = GetCallableMethods(type, kind == ServiceKind.Injected);
        var optionsType = DetectOptionsType(methods);
        return new ServiceSummary(
            Name: type.Name,
            Kind: kind,
            OptionsTypeName: optionsType?.Name,
            MethodCount: methods.Count,
            Description: XmlDocReader.GetSummary(type));
    }

    private static ServiceMetadata BuildMetadata(Type type)
    {
        var kind = GetKind(type);
        var methodInfos = GetCallableMethods(type, kind == ServiceKind.Injected);
        var optionsType = DetectOptionsType(methodInfos);

        return new ServiceMetadata(
            Name: type.Name,
            FullName: type.FullName ?? type.Name,
            Kind: kind,
            Methods: BuildMethods(methodInfos),
            Options: optionsType is null ? null : BuildOptions(optionsType),
            Description: XmlDocReader.GetSummary(type));
    }

    private static IReadOnlyList<MethodInfo> GetCallableMethods(Type type, bool instance)
    {
        var flags = BindingFlags.Public | BindingFlags.DeclaredOnly
                    | (instance ? BindingFlags.Instance : BindingFlags.Static);
        return type.GetMethods(flags)
            .Where(m => !m.IsSpecialName)
            .Where(m => m.DeclaringType != typeof(object))
            .Where(m => m.GetCustomAttribute<ObsoleteAttribute>() is null)
            .ToList();
    }

    private static IReadOnlyList<ServiceMethod> BuildMethods(IReadOnlyList<MethodInfo> methods)
    {
        var nullCtx = new NullabilityInfoContext();
        return methods
            .Select(m => new ServiceMethod(
                m.Name,
                TypeFormatter.Format(m.ReturnType),
                m.GetParameters()
                    .Select(p => new ServiceMethodParameter(p.Name ?? "_", TypeFormatter.FormatParameter(p, nullCtx)))
                    .ToList(),
                XmlDocReader.GetSummary(m)))
            .OrderBy(m => m.Name, StringComparer.Ordinal)
            .ThenBy(m => m.Parameters.Count)
            .ToList();
    }

    private static Type? DetectOptionsType(IReadOnlyList<MethodInfo> methods)
    {
        foreach (var method in methods.OrderBy(m => m.Name is "PresentAsync" ? 0 : 1))
        {
            var firstParam = method.GetParameters().FirstOrDefault();
            if (firstParam is null)
                continue;

            var paramType = firstParam.ParameterType;
            if (!paramType.IsGenericType)
                continue;

            if (paramType.GetGenericTypeDefinition() != typeof(Action<>))
                continue;

            var arg = paramType.GetGenericArguments()[0];
            if (arg.Namespace == ServicesNamespace)
                return arg;
        }

        return null;
    }

    private static ServiceOptions BuildOptions(Type optionsType)
    {
        var nullCtx = new NullabilityInfoContext();
        var instance = TryCreateInstance(optionsType);

        var allProps = optionsType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(p => p.GetMethod is { IsPublic: true })
            .Where(p => p.SetMethod is { IsPublic: true })
            .ToList();

        var properties = new List<ServiceOptionProperty>();
        var builders = new List<ServiceOptionBuilder>();

        foreach (var p in allProps)
        {
            if (TryBuildBuilder(p, nullCtx) is { } builder)
            {
                builders.Add(builder);
                continue;
            }

            properties.Add(new ServiceOptionProperty(
                p.Name,
                TypeFormatter.FormatProperty(p, nullCtx),
                FormatDefault(ReadDefault(instance, p)),
                XmlDocReader.GetSummary(p)));
        }

        return new ServiceOptions(
            Name: optionsType.Name,
            FullName: optionsType.FullName ?? optionsType.Name,
            Properties: properties.OrderBy(p => p.Name, StringComparer.Ordinal).ToList(),
            Builders: builders.OrderBy(b => b.PropertyName, StringComparer.Ordinal).ToList(),
            Description: XmlDocReader.GetSummary(optionsType));
    }

    private static object? TryCreateInstance(Type type)
    {
        try { return Activator.CreateInstance(type); }
        catch { return null; }
    }

    private static object? ReadDefault(object? instance, PropertyInfo prop)
    {
        if (instance is null) return null;
        try { return prop.GetValue(instance); }
        catch { return null; }
    }

    private static string? FormatDefault(object? value) => value switch
    {
        null => null,
        string s => $"\"{s}\"",
        bool b => b ? "true" : "false",
        _ => value.ToString(),
    };

    /// <summary>
    /// If the property is a builder-style delegate (a delegate whose invocation receives a fluent
    /// <c>*Builder</c> instance), expands the delegate signature and the builder's public methods.
    /// Returns null for plain data and callback (event-args) delegate properties.
    /// </summary>
    private static ServiceOptionBuilder? TryBuildBuilder(PropertyInfo prop, NullabilityInfoContext nullCtx)
    {
        var type = prop.PropertyType;
        if (!typeof(Delegate).IsAssignableFrom(type))
            return null;

        var invoke = type.GetMethod("Invoke");
        if (invoke is null)
            return null;

        var parameters = invoke.GetParameters();
        var builderParam = parameters.FirstOrDefault(
            p => p.ParameterType.Name.EndsWith("Builder", StringComparison.Ordinal));
        if (builderParam is null)
            return null;

        var paramList = string.Join(", ",
            parameters.Select(p => $"{TypeFormatter.FormatParameter(p, nullCtx)} {p.Name}"));
        var signature = $"{TypeFormatter.Format(invoke.ReturnType)} ({paramList})";

        return new ServiceOptionBuilder(
            PropertyName: prop.Name,
            DelegateSignature: signature,
            BuilderTypeName: builderParam.ParameterType.Name,
            BuilderMethods: BuildInstanceMethods(builderParam.ParameterType),
            Description: XmlDocReader.GetSummary(prop));
    }

    private static IReadOnlyList<ServiceMethod> BuildInstanceMethods(Type type)
    {
        var nullCtx = new NullabilityInfoContext();
        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)
            .Where(m => m.GetCustomAttribute<ObsoleteAttribute>() is null)
            .Select(m => new ServiceMethod(
                FormatMethodName(m),
                TypeFormatter.Format(m.ReturnType),
                m.GetParameters()
                    .Select(p => new ServiceMethodParameter(p.Name ?? "_", TypeFormatter.FormatParameter(p, nullCtx)))
                    .ToList(),
                XmlDocReader.GetSummary(m)))
            .OrderBy(m => m.Name, StringComparer.Ordinal)
            .ThenBy(m => m.Parameters.Count)
            .ToList();
    }

    private static string FormatMethodName(MethodInfo m)
    {
        if (!m.IsGenericMethodDefinition)
            return m.Name;
        var args = string.Join(", ", m.GetGenericArguments().Select(a => a.Name));
        return $"{m.Name}<{args}>";
    }
}
