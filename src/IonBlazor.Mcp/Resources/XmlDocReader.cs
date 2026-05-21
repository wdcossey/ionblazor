using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using IonBlazor.Abstractions;

namespace IonBlazor.Mcp.Resources;

internal static class XmlDocReader
{
    private static readonly Lazy<ParsedDocs> _docs = new(LoadDocs);

    public static string? GetSummary(Type type)
    {
        var id = GetTypeXmlId(type);
        if (id is null) return null;

        var docs = _docs.Value;
        if (docs.Summaries.TryGetValue(id, out var direct))
            return direct;

        if (!docs.InheritsDoc.Contains(id))
            return null;

        for (var bt = type.BaseType; bt is not null; bt = bt.BaseType)
        {
            var baseId = GetTypeXmlId(bt);
            if (baseId is not null && docs.Summaries.TryGetValue(baseId, out var inherited))
                return inherited;
        }
        return null;
    }

    public static string? GetSummary(PropertyInfo prop)
    {
        var id = GetPropertyXmlId(prop);
        if (id is null) return null;

        var docs = _docs.Value;
        if (docs.Summaries.TryGetValue(id, out var direct))
            return direct;

        if (!docs.InheritsDoc.Contains(id))
            return null;

        return ResolveInheritedPropertySummary(prop, docs);
    }

    public static string? GetSummary(FieldInfo field)
    {
        var id = GetFieldXmlId(field);
        if (id is null) return null;

        var docs = _docs.Value;
        return docs.Summaries.TryGetValue(id, out var direct) ? direct : null;
    }

    public static string? GetSummary(MethodInfo method)
    {
        var id = GetMethodXmlId(method);
        if (id is null) return null;

        var docs = _docs.Value;
        if (docs.Summaries.TryGetValue(id, out var direct))
            return direct;

        if (!docs.InheritsDoc.Contains(id))
            return null;

        return ResolveInheritedMethodSummary(method, docs);
    }

    private static string? ResolveInheritedPropertySummary(PropertyInfo prop, ParsedDocs docs)
    {
        var declaring = prop.DeclaringType;
        if (declaring is null) return null;

        for (var bt = declaring.BaseType; bt is not null; bt = bt.BaseType)
        {
            var baseProp = bt.GetProperty(prop.Name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (baseProp is null) continue;

            var baseId = GetPropertyXmlId(baseProp);
            if (baseId is not null && docs.Summaries.TryGetValue(baseId, out var s))
                return s;
        }

        foreach (var iface in declaring.GetInterfaces())
        {
            var ifaceProp = iface.GetProperty(prop.Name);
            if (ifaceProp is null) continue;

            var ifaceId = GetPropertyXmlId(ifaceProp);
            if (ifaceId is not null && docs.Summaries.TryGetValue(ifaceId, out var s))
                return s;
        }

        return null;
    }

    private static string? ResolveInheritedMethodSummary(MethodInfo method, ParsedDocs docs)
    {
        var declaring = method.DeclaringType;
        if (declaring is null) return null;

        var paramTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();

        for (var bt = declaring.BaseType; bt is not null; bt = bt.BaseType)
        {
            var baseMethod = bt.GetMethod(method.Name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly,
                binder: null, types: paramTypes, modifiers: null);
            if (baseMethod is null) continue;

            var baseId = GetMethodXmlId(baseMethod);
            if (baseId is not null && docs.Summaries.TryGetValue(baseId, out var s))
                return s;
        }

        foreach (var iface in declaring.GetInterfaces())
        {
            var ifaceMethod = iface.GetMethod(method.Name, paramTypes);
            if (ifaceMethod is null) continue;

            var ifaceId = GetMethodXmlId(ifaceMethod);
            if (ifaceId is not null && docs.Summaries.TryGetValue(ifaceId, out var s))
                return s;
        }

        return null;
    }

    private static ParsedDocs LoadDocs()
    {
        var asmLocation = typeof(IonComponent).Assembly.Location;
        var empty = new ParsedDocs(
            new Dictionary<string, string>(),
            new HashSet<string>(StringComparer.Ordinal));

        if (string.IsNullOrEmpty(asmLocation))
            return empty;

        var xmlPath = Path.ChangeExtension(asmLocation, ".xml");
        if (!File.Exists(xmlPath))
            return empty;

        try
        {
            var doc = XDocument.Load(xmlPath);
            var members = doc.Root?.Element("members")?.Elements("member") ?? [];
            var summaries = new Dictionary<string, string>(StringComparer.Ordinal);
            var inheritsDoc = new HashSet<string>(StringComparer.Ordinal);

            foreach (var member in members)
            {
                var name = member.Attribute("name")?.Value;
                if (string.IsNullOrEmpty(name)) continue;

                var summary = member.Element("summary");
                if (summary is not null)
                {
                    var text = NormalizeSummary(summary);
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        summaries[name] = text;
                        continue;
                    }
                }

                if (member.Element("inheritdoc") is not null)
                    inheritsDoc.Add(name);
            }
            return new ParsedDocs(summaries, inheritsDoc);
        }
        catch
        {
            return empty;
        }
    }

    private sealed record ParsedDocs(
        IReadOnlyDictionary<string, string> Summaries,
        IReadOnlySet<string> InheritsDoc);

    private static string NormalizeSummary(XElement summary)
    {
        var sb = new StringBuilder();
        AppendInline(summary, sb);
        return Regex.Replace(sb.ToString(), @"\s+", " ").Trim();
    }

    private static void AppendInline(XElement element, StringBuilder sb)
    {
        foreach (var node in element.Nodes())
        {
            switch (node)
            {
                case XText text:
                    sb.Append(text.Value);
                    break;
                case XElement child:
                    AppendChildElement(child, sb);
                    break;
            }
        }
    }

    private static void AppendChildElement(XElement child, StringBuilder sb)
    {
        var name = child.Name.LocalName;

        if (name is "see" or "seealso")
        {
            var cref = child.Attribute("cref")?.Value;
            var href = child.Attribute("href")?.Value;
            var langword = child.Attribute("langword")?.Value;
            if (!string.IsNullOrEmpty(cref))
                sb.Append(TrimCrefToShortName(cref));
            else if (!string.IsNullOrEmpty(langword))
                sb.Append(langword);
            else if (!string.IsNullOrEmpty(href))
                sb.Append(href);
            return;
        }

        if (name is "br" or "para")
        {
            sb.Append(' ');
            AppendInline(child, sb);
            sb.Append(' ');
            return;
        }

        if (name is "paramref" or "typeparamref")
        {
            var refName = child.Attribute("name")?.Value;
            if (!string.IsNullOrEmpty(refName))
                sb.Append(refName);
            return;
        }

        AppendInline(child, sb);
    }

    private static string TrimCrefToShortName(string cref)
    {
        var body = cref.Length > 2 && cref[1] == ':' ? cref[2..] : cref;
        var paren = body.IndexOf('(');
        if (paren >= 0)
            body = body[..paren];
        var lastDot = body.LastIndexOf('.');
        if (lastDot < 0)
            return body;
        var dotBeforeLast = body.LastIndexOf('.', lastDot - 1);
        return dotBeforeLast < 0 ? body : body[(dotBeforeLast + 1)..];
    }

    private static string? GetTypeXmlId(Type type)
    {
        var name = GetTypeNameForXmlId(type);
        return name is null ? null : "T:" + name;
    }

    private static string? GetPropertyXmlId(PropertyInfo prop)
    {
        var declaring = prop.DeclaringType;
        if (declaring is null)
            return null;
        var typeName = GetTypeNameForXmlId(declaring);
        return typeName is null ? null : $"P:{typeName}.{prop.Name}";
    }

    private static string? GetFieldXmlId(FieldInfo field)
    {
        var declaring = field.DeclaringType;
        if (declaring is null)
            return null;
        var typeName = GetTypeNameForXmlId(declaring);
        return typeName is null ? null : $"F:{typeName}.{field.Name}";
    }

    private static string? GetMethodXmlId(MethodInfo method)
    {
        var declaring = method.DeclaringType;
        if (declaring is null)
            return null;
        var typeName = GetTypeNameForXmlId(declaring);
        if (typeName is null)
            return null;

        var sb = new StringBuilder($"M:{typeName}.{method.Name}");
        if (method.IsGenericMethod)
            sb.Append("``").Append(method.GetGenericArguments().Length);

        var parameters = method.GetParameters();
        if (parameters.Length > 0)
        {
            sb.Append('(');
            for (var i = 0; i < parameters.Length; i++)
            {
                if (i > 0) sb.Append(',');
                sb.Append(FormatParameterTypeForXmlId(parameters[i].ParameterType, declaring));
            }
            sb.Append(')');
        }
        return sb.ToString();
    }

    private static string? GetTypeNameForXmlId(Type type)
    {
        if (type.IsGenericType && !type.IsGenericTypeDefinition)
            type = type.GetGenericTypeDefinition();

        var ns = type.Namespace;
        var simple = type.Name;
        return string.IsNullOrEmpty(ns) ? simple : $"{ns}.{simple}";
    }

    private static string FormatParameterTypeForXmlId(Type t, Type declaringType)
    {
        if (t.IsByRef)
            return FormatParameterTypeForXmlId(t.GetElementType()!, declaringType) + "@";

        if (t.IsArray)
        {
            var element = FormatParameterTypeForXmlId(t.GetElementType()!, declaringType);
            var rank = t.GetArrayRank();
            return element + (rank == 1 ? "[]" : "[" + new string(',', rank - 1) + "]");
        }

        if (t.IsGenericParameter)
            return t.DeclaringMethod is not null ? "``" + t.GenericParameterPosition : "`" + t.GenericParameterPosition;

        if (t.IsGenericType)
        {
            var def = t.GetGenericTypeDefinition();
            var ns = def.Namespace;
            var bareName = def.Name;
            var tick = bareName.IndexOf('`');
            if (tick >= 0) bareName = bareName[..tick];
            var inner = string.Join(",", t.GetGenericArguments().Select(a => FormatParameterTypeForXmlId(a, declaringType)));
            var prefix = string.IsNullOrEmpty(ns) ? bareName : $"{ns}.{bareName}";
            return $"{prefix}{{{inner}}}";
        }

        var typeNs = t.Namespace;
        return string.IsNullOrEmpty(typeNs) ? t.Name : $"{typeNs}.{t.Name}";
    }
}
