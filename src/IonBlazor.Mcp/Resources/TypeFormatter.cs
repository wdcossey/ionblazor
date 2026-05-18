using System.Reflection;

namespace IonBlazor.Mcp.Resources;

internal static class TypeFormatter
{
    public static string Format(Type t)
    {
        if (Nullable.GetUnderlyingType(t) is Type underlying)
            return Format(underlying) + "?";

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
        var args = string.Join(", ", t.GetGenericArguments().Select(Format));
        return $"{baseName}<{args}>";
    }

    public static string FormatProperty(PropertyInfo prop, NullabilityInfoContext nullCtx)
    {
        var formatted = Format(prop.PropertyType);
        if (IsValueType(prop.PropertyType))
            return formatted;

        var info = nullCtx.Create(prop);
        return info.ReadState == NullabilityState.Nullable && !formatted.EndsWith('?')
            ? formatted + "?"
            : formatted;
    }

    public static string FormatParameter(ParameterInfo param, NullabilityInfoContext nullCtx)
    {
        var formatted = Format(param.ParameterType);
        if (IsValueType(param.ParameterType))
            return formatted;

        var info = nullCtx.Create(param);
        return info.WriteState == NullabilityState.Nullable && !formatted.EndsWith('?')
            ? formatted + "?"
            : formatted;
    }

    public static bool IsValueType(Type t) =>
        t.IsValueType && (!t.IsGenericType || t.GetGenericTypeDefinition() != typeof(Nullable<>));
}
