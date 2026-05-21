using System.ComponentModel;
using System.Text;
using IonBlazor.Mcp.Resources;
using ModelContextProtocol.Server;

namespace IonBlazor.Mcp.Tools;

[McpServerToolType]
public static class ComponentTools
{
    private const int InlineDescriptionMaxLength = 140;

    [McpServerTool, Description(
        "Lists every IonBlazor component along with its base class, JS-interop status, " +
        "child-content support, the marker interfaces it implements " +
        "(IIonModeComponent, IIonColorComponent, etc.), and its two-way @bind-able properties.")]
    public static string ListComponents()
    {
        var components = ComponentRegistry.ListAll();

        var sb = new StringBuilder();
        sb.Append("# IonBlazor Components (").Append(components.Count).AppendLine(")");
        sb.AppendLine();
        sb.AppendLine("| Name | Base | JS | ChildContent | Interfaces | @bind |");
        sb.AppendLine("| --- | --- | --- | --- | --- | --- |");

        foreach (var c in components)
        {
            sb.Append("| ").Append(c.Name)
              .Append(" | ").Append(c.BaseClass)
              .Append(" | ").Append(c.HasJsInterop ? "Yes" : "-")
              .Append(" | ").Append(c.HasChildContent ? "Yes" : "-")
              .Append(" | ").Append(c.Interfaces.Count == 0 ? "-" : string.Join(", ", c.Interfaces))
              .Append(" | ").Append(c.BindProperties.Count == 0 ? "-" : string.Join(", ", c.BindProperties))
              .AppendLine(" |");
        }

        return sb.ToString();
    }

    [McpServerTool, Description(
        "Filters the IonBlazor component inventory by structural traits. " +
        "All filters are optional and combined with AND. " +
        "Use this for queries like 'which components support @bind-Checked' " +
        "(supportsBindFor='Checked'), 'all overlays with JS interop' (hasJsInterop=true, hasChildContent=false), " +
        "or 'all color-themed components' (requiresInterface='IIonColorComponent'). " +
        "Returns a markdown table including the type-level summary where available.")]
    public static string FindComponents(
        [Description("Filter by base class. One of: 'IonComponent', 'IonContentComponent', 'IonJsComponent', 'IonJsContentComponent'.")]
        string? baseClass = null,
        [Description("Filter to components that implement this Ion interface, e.g. 'IIonModeComponent', 'IIonColorComponent'.")]
        string? requiresInterface = null,
        [Description("Filter by JS interop presence (true = derived from IonJsComponent).")]
        bool? hasJsInterop = null,
        [Description("Filter by ChildContent support (true = derived from IonContentComponent or IonJsContentComponent).")]
        bool? hasChildContent = null,
        [Description("Filter to components that expose a @bind-able property of this name, e.g. 'Value', 'Checked'.")]
        string? supportsBindFor = null)
    {
        var matches = ComponentRegistry.ListAll()
            .Where(c => baseClass is null || string.Equals(c.BaseClass, baseClass, StringComparison.Ordinal))
            .Where(c => requiresInterface is null || c.Interfaces.Contains(requiresInterface, StringComparer.Ordinal))
            .Where(c => hasJsInterop is null || c.HasJsInterop == hasJsInterop)
            .Where(c => hasChildContent is null || c.HasChildContent == hasChildContent)
            .Where(c => supportsBindFor is null || c.BindProperties.Contains(supportsBindFor, StringComparer.Ordinal))
            .ToList();

        var sb = new StringBuilder();
        sb.Append("# IonBlazor Components matching filters (").Append(matches.Count).AppendLine(")");
        sb.AppendLine();

        if (matches.Count == 0)
        {
            sb.AppendLine("(no components matched the supplied filters)");
            return sb.ToString();
        }

        sb.AppendLine("| Name | Base | JS | ChildContent | Interfaces | @bind | Description |");
        sb.AppendLine("| --- | --- | --- | --- | --- | --- | --- |");
        foreach (var c in matches)
        {
            sb.Append("| ").Append(c.Name)
              .Append(" | ").Append(c.BaseClass)
              .Append(" | ").Append(c.HasJsInterop ? "Yes" : "-")
              .Append(" | ").Append(c.HasChildContent ? "Yes" : "-")
              .Append(" | ").Append(c.Interfaces.Count == 0 ? "-" : string.Join(", ", c.Interfaces))
              .Append(" | ").Append(c.BindProperties.Count == 0 ? "-" : string.Join(", ", c.BindProperties))
              .Append(" | ").Append(FormatCellDescription(c.Description))
              .AppendLine(" |");
        }
        return sb.ToString();
    }

    [McpServerTool, Description(
        "Returns full metadata for a single IonBlazor component: type-level summary, base class, " +
        "JsImportName, interfaces, [Parameter] properties (with types and summaries), " +
        "[CascadingParameter] properties, EventCallback events, two-way @bind support " +
        "(value + parallel changed/input callbacks), and JS interop methods. " +
        "Component name must be exact PascalCase (e.g. 'IonInput'). " +
        "Generic components use angle-bracket form (e.g. 'IonSelect<TValue>').")]
    public static string GetComponentMetadata(
        [Description("Exact component name, e.g. 'IonButton', 'IonInput', 'IonSelect<TValue>'.")]
        string componentName)
    {
        var meta = ComponentRegistry.GetMetadata(componentName);
        if (meta is null)
            return $"Component '{componentName}' not found. Use ListComponents to see the full inventory.";

        var sb = new StringBuilder();
        sb.Append("# ").AppendLine(meta.Name);
        sb.AppendLine();
        if (!string.IsNullOrEmpty(meta.Description))
        {
            sb.AppendLine(meta.Description);
            sb.AppendLine();
        }
        sb.Append("- **Full name:** `").Append(meta.FullName).AppendLine("`");
        sb.Append("- **Base class:** `").Append(meta.BaseClass).AppendLine("`");
        sb.Append("- **Has child content:** ").AppendLine(meta.HasChildContent ? "Yes" : "No");
        sb.Append("- **Has JS interop:** ").AppendLine(meta.HasJsInterop ? "Yes" : "No");
        if (meta.JsImportName is not null)
            sb.Append("- **JS import name:** `").Append(meta.JsImportName).AppendLine("`");
        sb.Append("- **Interfaces:** ")
          .AppendLine(meta.Interfaces.Count == 0 ? "(none)" : string.Join(", ", meta.Interfaces.Select(i => $"`{i}`")));
        sb.AppendLine();

        sb.AppendLine("> **Slots & arbitrary attributes:** Ionic named slots are not component parameters. Every");
        sb.AppendLine("> IonBlazor component captures unmatched attributes via `[Parameter(CaptureUnmatchedValues");
        sb.AppendLine("> = true)] AdditionalAttributes`, so a named slot is a de-facto HTML attribute — place");
        sb.AppendLine("> content into one with `slot=\"start\"` / `slot=\"end\"` on the child element, exactly as you");
        sb.AppendLine("> would pass `id` or `class`. The valid slot names per component are Ionic web-component");
        sb.AppendLine("> metadata; see the Ionic docs for the wrapped element.");
        sb.AppendLine();

        AppendBinds(sb, meta);
        AppendParameters(sb, meta);
        AppendCascadingParameters(sb, meta);
        AppendEvents(sb, meta);
        AppendJsMethods(sb, meta);
        AppendValueSets(sb, meta);

        return sb.ToString();
    }

    private static void AppendBinds(StringBuilder sb, ComponentMetadata meta)
    {
        if (meta.Binds.Count == 0)
            return;

        sb.AppendLine("## @bind support");
        sb.AppendLine();
        sb.AppendLine("| Property | Commit (`@bind-X`) | Live (`@bind-X:event`) | Description |");
        sb.AppendLine("| --- | --- | --- | --- |");
        foreach (var b in meta.Binds)
        {
            sb.Append("| ").Append(b.PropertyName)
              .Append(" | ").Append(b.ChangedCallbackName)
              .Append(" | ").Append(b.InputCallbackName ?? "-")
              .Append(" | ").Append(FormatCellDescription(b.Description))
              .AppendLine(" |");
        }
        sb.AppendLine();
    }

    private static void AppendParameters(StringBuilder sb, ComponentMetadata meta)
    {
        sb.AppendLine("## Parameters");
        sb.AppendLine();
        if (meta.Parameters.Count == 0)
        {
            sb.AppendLine("(none)");
            sb.AppendLine();
            return;
        }
        sb.AppendLine("| Name | Type | Value set | Description |");
        sb.AppendLine("| --- | --- | --- | --- |");
        foreach (var p in meta.Parameters)
            sb.Append("| ").Append(p.Name)
              .Append(" | `").Append(p.TypeName).Append('`')
              .Append(" | ").Append(p.ValueSetName is null ? "-" : $"`{p.ValueSetName}`")
              .Append(" | ").Append(FormatCellDescription(p.Description))
              .AppendLine(" |");
        sb.AppendLine();
    }

    private static void AppendValueSets(StringBuilder sb, ComponentMetadata meta)
    {
        if (meta.ValueSets.Count == 0)
            return;

        sb.AppendLine("## Value sets");
        sb.AppendLine();
        sb.AppendLine("These static classes hold the conventional `const string` values for the matching `string` parameters above.");
        sb.AppendLine("They are advisory — the parameters accept any `string`, which keeps the surface forward-compatible with");
        sb.AppendLine("future Ionic updates.");
        sb.AppendLine();

        foreach (var vs in meta.ValueSets)
        {
            sb.Append("### `").Append(vs.Name).AppendLine("`");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(vs.Description))
            {
                sb.AppendLine(vs.Description);
                sb.AppendLine();
            }
            sb.AppendLine("| Constant | Value | Description |");
            sb.AppendLine("| --- | --- | --- |");
            foreach (var v in vs.Values)
            {
                sb.Append("| `").Append(vs.Name).Append('.').Append(v.ConstantName).Append('`')
                  .Append(" | ").Append(v.Value is null ? "`null`" : $"`\"{v.Value}\"`")
                  .Append(" | ").Append(FormatCellDescription(v.Description))
                  .AppendLine(" |");
            }
            sb.AppendLine();
        }
    }

    private static void AppendCascadingParameters(StringBuilder sb, ComponentMetadata meta)
    {
        if (meta.CascadingParameters.Count == 0)
            return;

        sb.AppendLine("## Cascading parameters");
        sb.AppendLine();
        sb.AppendLine("| Name | Type | Cascading name | Description |");
        sb.AppendLine("| --- | --- | --- | --- |");
        foreach (var c in meta.CascadingParameters)
        {
            sb.Append("| ").Append(c.Name)
              .Append(" | `").Append(c.TypeName).Append('`')
              .Append(" | ").Append(c.CascadingName is null ? "-" : $"`{c.CascadingName}`")
              .Append(" | ").Append(FormatCellDescription(c.Description))
              .AppendLine(" |");
        }
        sb.AppendLine();
    }

    private static void AppendEvents(StringBuilder sb, ComponentMetadata meta)
    {
        if (meta.Events.Count == 0)
            return;

        sb.AppendLine("## Events");
        sb.AppendLine();
        sb.AppendLine("| Name | Payload | Description |");
        sb.AppendLine("| --- | --- | --- |");
        foreach (var e in meta.Events)
        {
            sb.Append("| ").Append(e.Name)
              .Append(" | ").Append(e.PayloadType is null ? "(no payload)" : $"`{e.PayloadType}`")
              .Append(" | ").Append(FormatCellDescription(e.Description))
              .AppendLine(" |");
        }
        sb.AppendLine();
    }

    private static void AppendJsMethods(StringBuilder sb, ComponentMetadata meta)
    {
        if (meta.JsMethods.Count == 0)
            return;

        sb.AppendLine("## JS methods");
        sb.AppendLine();
        sb.AppendLine("| Signature | Returns | Description |");
        sb.AppendLine("| --- | --- | --- |");
        foreach (var m in meta.JsMethods)
        {
            var paramList = string.Join(", ", m.Parameters.Select(p => $"{p.TypeName} {p.Name}"));
            sb.Append("| `").Append(m.Name).Append('(').Append(paramList).Append(')').Append('`')
              .Append(" | `").Append(m.ReturnType).Append('`')
              .Append(" | ").Append(FormatCellDescription(m.Description))
              .AppendLine(" |");
        }
        sb.AppendLine();
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
