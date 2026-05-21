using System.ComponentModel;
using System.Text;
using IonBlazor.Mcp.Resources;
using ModelContextProtocol.Server;

namespace IonBlazor.Mcp.Tools;

[McpServerToolType]
public static class ValueSetTools
{
    private const int InlineDescriptionMaxLength = 140;

    [McpServerTool, Description(
        "Lists every IonBlazor value-set class — static classes in IonBlazor.Components that hold the " +
        "conventional `const string` values for `string`-typed component parameters (e.g. IonMode, IonColor, " +
        "IonInputType, IonRippleEffectType, IonRangeLabelPlacement). Parameters accept any string at runtime, " +
        "so these classes are advisory rather than enums — the design intentionally keeps the surface " +
        "forward-compatible with future Ionic releases. Use this for queries like 'what values can the " +
        "color attribute take' or 'list every Ionic mode option'.")]
    public static string ListValueSets()
    {
        var sets = ValueSetRegistry.ListAll();

        var sb = new StringBuilder();
        sb.Append("# IonBlazor Value Sets (").Append(sets.Count).AppendLine(")");
        sb.AppendLine();
        sb.AppendLine("> Each entry is a `public static class` of `public const string?` values. They are not");
        sb.AppendLine("> enums — IonBlazor's `string` parameters accept arbitrary values so the surface stays");
        sb.AppendLine("> compatible across Ionic versions. Use `GetValueSet` for the full constant list.");
        sb.AppendLine();
        sb.AppendLine("| Name | Values | Description |");
        sb.AppendLine("| --- | --- | --- |");

        foreach (var s in sets)
        {
            sb.Append("| ").Append(s.Name)
              .Append(" | ").Append(s.ValueCount)
              .Append(" | ").Append(FormatCellDescription(s.Description))
              .AppendLine(" |");
        }

        return sb.ToString();
    }

    [McpServerTool, Description(
        "Returns every constant in a single IonBlazor value-set class — constant name, the literal string " +
        "value it maps to, and the XML doc summary where present. Use this for the exact options behind a " +
        "parameter like IonInput.Type (IonInputType), IonApp.Mode (IonMode), or IonRippleEffect.Type " +
        "(IonRippleEffectType). Value-set name must be exact PascalCase, e.g. 'IonInputType'.")]
    public static string GetValueSet(
        [Description("Exact value-set class name, e.g. 'IonMode', 'IonColor', 'IonInputType', 'IonRippleEffectType'.")]
        string name)
    {
        var meta = ValueSetRegistry.GetMetadata(name);
        if (meta is null)
            return $"Value set '{name}' not found. Use ListValueSets to see the full inventory.";

        var sb = new StringBuilder();
        sb.Append("# ").AppendLine(meta.Name);
        sb.AppendLine();
        if (!string.IsNullOrEmpty(meta.Description))
        {
            sb.AppendLine(meta.Description);
            sb.AppendLine();
        }
        sb.Append("- **Full name:** `").Append(meta.FullName).AppendLine("`");
        sb.Append("- **Constants:** ").Append(meta.Values.Count).AppendLine();
        sb.AppendLine();

        if (meta.Values.Count == 0)
        {
            sb.AppendLine("(no constants)");
            return sb.ToString();
        }

        sb.AppendLine("| Constant | Value | Description |");
        sb.AppendLine("| --- | --- | --- |");
        foreach (var v in meta.Values)
        {
            sb.Append("| `").Append(meta.Name).Append('.').Append(v.ConstantName).Append('`')
              .Append(" | ").Append(v.Value is null ? "`null`" : $"`\"{v.Value}\"`")
              .Append(" | ").Append(FormatCellDescription(v.Description))
              .AppendLine(" |");
        }

        return sb.ToString();
    }

    private static string FormatCellDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return "-";

        var single = description.Replace('\r', ' ').Replace('\n', ' ').Replace("|", "\\|");
        if (single.Length > InlineDescriptionMaxLength)
            single = single[..(InlineDescriptionMaxLength - 1)] + "…";
        return single;
    }
}
