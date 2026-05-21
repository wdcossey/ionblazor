using System.Reflection;
using IonBlazor.Abstractions;

namespace IonBlazor.Mcp.Resources;

public static class ValueSetRegistry
{
    private const string ComponentsNamespace = "IonBlazor.Components";
    private const string IonPrefix = "Ion";

    private static readonly Lazy<IReadOnlyList<Type>> _valueSetTypes = new(DiscoverValueSets);
    private static readonly Lazy<IReadOnlyDictionary<string, Type>> _byName =
        new(() => _valueSetTypes.Value.ToDictionary(t => t.Name, t => t, StringComparer.Ordinal));

    public static IReadOnlyList<ValueSetSummary> ListAll() =>
        _valueSetTypes.Value
            .Select(BuildSummary)
            .OrderBy(s => s.Name, StringComparer.Ordinal)
            .ToList();

    public static ValueSetMetadata? GetMetadata(string name)
    {
        if (!_byName.Value.TryGetValue(name, out Type? type))
            return null;
        return BuildMetadata(type);
    }

    /// <summary>
    /// Resolves the value-set class associated with a string-typed property on a component, using the
    /// IonBlazor naming conventions. Returns the value-set class name (e.g. "IonInputType") or null
    /// if no matching set exists.
    /// </summary>
    public static string? ResolveForProperty(Type componentType, string propertyName)
    {
        var rawComponentName = componentType.Name;
        var tick = rawComponentName.IndexOf('`');
        if (tick >= 0) rawComponentName = rawComponentName[..tick];

        var suffix = rawComponentName.StartsWith(IonPrefix, StringComparison.Ordinal)
            ? rawComponentName[IonPrefix.Length..]
            : rawComponentName;

        // Most specific first → least specific last.
        var candidates = new[]
        {
            IonPrefix + suffix + propertyName,   // e.g. IonInputType, IonRangeLabelPlacement
            IonPrefix + propertyName,            // e.g. IonMode, IonColor (cross-component)
            suffix + propertyName,               // e.g. SearchbarAutoComplete (no Ion prefix)
        };

        foreach (var candidate in candidates)
        {
            if (_byName.Value.ContainsKey(candidate))
                return candidate;
        }
        return null;
    }

    private static IReadOnlyList<Type> DiscoverValueSets()
    {
        var assembly = typeof(IonComponent).Assembly;
        return assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: true, IsSealed: true, IsPublic: true })
            .Where(t => t.Namespace == ComponentsNamespace)
            .Where(t => t.GetCustomAttribute<ObsoleteAttribute>() is null)
            .Where(IsValueSet)
            .ToList();
    }

    private static bool IsValueSet(Type type)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        if (fields.Length == 0)
            return false;

        foreach (var field in fields)
        {
            if (!field.IsLiteral || field.IsInitOnly)
                return false;
            if (field.FieldType != typeof(string))
                return false;
        }

        // Reject extension/helper classes that happen to also expose const strings.
        var declaredMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        return declaredMethods.All(m => m.IsSpecialName);
    }

    private static ValueSetSummary BuildSummary(Type type) => new(
        Name: type.Name,
        FullName: type.FullName ?? type.Name,
        ValueCount: GetValueFields(type).Count,
        Description: XmlDocReader.GetSummary(type));

    private static ValueSetMetadata BuildMetadata(Type type) => new(
        Name: type.Name,
        FullName: type.FullName ?? type.Name,
        Values: GetValueFields(type)
            .Select(f => new ValueSetEntry(
                ConstantName: f.Name,
                Value: (string?)f.GetRawConstantValue(),
                Description: XmlDocReader.GetSummary(f)))
            .ToList(),
        Description: XmlDocReader.GetSummary(type));

    private static IReadOnlyList<FieldInfo> GetValueFields(Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f is { IsLiteral: true, IsInitOnly: false })
            .Where(f => f.FieldType == typeof(string))
            .OrderBy(f => f.Name, StringComparer.Ordinal)
            .ToList();
}
