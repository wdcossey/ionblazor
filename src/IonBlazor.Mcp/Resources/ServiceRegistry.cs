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
                        && typeof(ComponentBase).IsAssignableFrom(t)
                        && !typeof(IonComponent).IsAssignableFrom(t))
            .ToList();
    }

    private static ServiceSummary BuildSummary(Type type)
    {
        var methods = GetPublicStaticMethods(type);
        var optionsType = DetectOptionsType(methods);
        return new ServiceSummary(
            Name: type.Name,
            OptionsTypeName: optionsType?.Name,
            MethodCount: methods.Count,
            Description: XmlDocReader.GetSummary(type));
    }

    private static ServiceMetadata BuildMetadata(Type type)
    {
        var methodInfos = GetPublicStaticMethods(type);
        var optionsType = DetectOptionsType(methodInfos);

        return new ServiceMetadata(
            Name: type.Name,
            FullName: type.FullName ?? type.Name,
            Methods: BuildMethods(methodInfos),
            Options: optionsType is null ? null : BuildOptions(optionsType),
            Description: XmlDocReader.GetSummary(type));
    }

    private static IReadOnlyList<MethodInfo> GetPublicStaticMethods(Type type) =>
        type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)
            .Where(m => m.GetCustomAttribute<ObsoleteAttribute>() is null)
            .ToList();

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
        var properties = optionsType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(p => p.GetMethod is { IsPublic: true })
            .Where(p => p.SetMethod is { IsPublic: true })
            .Select(p => new ServiceOptionProperty(
                p.Name,
                TypeFormatter.FormatProperty(p, nullCtx),
                XmlDocReader.GetSummary(p)))
            .OrderBy(p => p.Name, StringComparer.Ordinal)
            .ToList();

        return new ServiceOptions(
            Name: optionsType.Name,
            FullName: optionsType.FullName ?? optionsType.Name,
            Properties: properties,
            Description: XmlDocReader.GetSummary(optionsType));
    }
}
