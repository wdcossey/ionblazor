using System.ComponentModel;
using System.Text;
using IonBlazor.Mcp.Resources;
using ModelContextProtocol.Server;

namespace IonBlazor.Mcp.Tools;

[McpServerToolType]
public static class ServiceTools
{
    private const int InlineDescriptionMaxLength = 140;

    [McpServerTool, Description(
        "Lists every IonBlazor static service controller (IonActionSheetController, IonAlertController, " +
        "IonLoadingController, IonToastController) along with the associated *Options type and the number of " +
        "public static methods. Use this for queries like 'how do I show a toast programmatically' or " +
        "'which IonBlazor overlay services exist'. Important: each controller is a ComponentBase subclass " +
        "that must be rendered once at the app root (e.g. App.razor or MainLayout.razor) before its static " +
        "methods can be invoked — call GetServiceMetadata for the exact setup snippet.")]
    public static string ListServices()
    {
        var services = ServiceRegistry.ListAll();

        var sb = new StringBuilder();
        sb.Append("# IonBlazor Services (").Append(services.Count).AppendLine(")");
        sb.AppendLine();
        sb.AppendLine("> Each controller below is a `ComponentBase` subclass, not a DI service. The static");
        sb.AppendLine("> methods only work after the controller's tag has been rendered once at the app root");
        sb.AppendLine("> (e.g. `<IonAlertController />` in `App.razor` or `MainLayout.razor`). That render");
        sb.AppendLine("> captures `IJSRuntime` into a static field used by every subsequent static call.");
        sb.AppendLine();
        sb.AppendLine("| Name | Options | Methods | Description |");
        sb.AppendLine("| --- | --- | --- | --- |");

        foreach (var s in services)
        {
            sb.Append("| ").Append(s.Name)
              .Append(" | ").Append(s.OptionsTypeName is null ? "-" : $"`{s.OptionsTypeName}`")
              .Append(" | ").Append(s.MethodCount)
              .Append(" | ").Append(FormatCellDescription(s.Description))
              .AppendLine(" |");
        }

        return sb.ToString();
    }

    [McpServerTool, Description(
        "Returns full metadata for a single IonBlazor service controller: type-level summary, the required " +
        "host-component setup snippet, all public static methods (signatures, parameters, return types, " +
        "summaries), and the associated *Options record — every property with its type, default value and " +
        "summary, plus any builder properties expanded to their delegate signature and the fluent builder's " +
        "public methods (e.g. ButtonsBuilder/InputsBuilder). Use this to learn the exact API for " +
        "IonAlertController.PresentAsync, IonToastController.PresentAsync, IonLoadingController.CreateAsync, " +
        "etc. Service name must be exact PascalCase, e.g. 'IonToastController'.")]
    public static string GetServiceMetadata(
        [Description("Exact service name, e.g. 'IonActionSheetController', 'IonAlertController', 'IonLoadingController', 'IonToastController'.")]
        string serviceName)
    {
        var meta = ServiceRegistry.GetMetadata(serviceName);
        if (meta is null)
            return $"Service '{serviceName}' not found. Use ListServices to see the full inventory.";

        var sb = new StringBuilder();
        sb.Append("# ").AppendLine(meta.Name);
        sb.AppendLine();
        if (!string.IsNullOrEmpty(meta.Description))
        {
            sb.AppendLine(meta.Description);
            sb.AppendLine();
        }
        sb.Append("- **Full name:** `").Append(meta.FullName).AppendLine("`");
        if (meta.Options is not null)
            sb.Append("- **Options type:** `").Append(meta.Options.Name).AppendLine("`");
        sb.AppendLine();

        AppendSetup(sb, meta);
        AppendMethods(sb, meta);
        AppendOptions(sb, meta);

        return sb.ToString();
    }

    private static void AppendSetup(StringBuilder sb, ServiceMetadata meta)
    {
        sb.AppendLine("## Setup");
        sb.AppendLine();
        sb.Append("`").Append(meta.Name).AppendLine("` is a `ComponentBase` subclass, not a DI service. It injects");
        sb.AppendLine("`IJSRuntime` and caches it into a static field during its first render — every static");
        sb.AppendLine("method below reads that field, so the controller's tag MUST be rendered once at the app");
        sb.AppendLine("root before any static call. Without it, `PresentAsync` will throw `NullReferenceException`.");
        sb.AppendLine();
        sb.AppendLine("Place the tag once in `App.razor` (or `MainLayout.razor`):");
        sb.AppendLine();
        sb.AppendLine("```razor");
        sb.Append('<').Append(meta.Name).AppendLine(" />");
        sb.AppendLine("```");
        sb.AppendLine();
        sb.AppendLine("Then call the static methods from anywhere in the app — no DI registration needed.");
        sb.AppendLine();
    }

    private static void AppendMethods(StringBuilder sb, ServiceMetadata meta)
    {
        sb.AppendLine("## Static methods");
        sb.AppendLine();
        if (meta.Methods.Count == 0)
        {
            sb.AppendLine("(none)");
            sb.AppendLine();
            return;
        }

        sb.AppendLine("| Signature | Returns | Description |");
        sb.AppendLine("| --- | --- | --- |");
        foreach (var m in meta.Methods)
        {
            var paramList = string.Join(", ", m.Parameters.Select(p => $"{p.TypeName} {p.Name}"));
            sb.Append("| `").Append(m.Name).Append('(').Append(paramList).Append(')').Append('`')
              .Append(" | `").Append(m.ReturnType).Append('`')
              .Append(" | ").Append(FormatCellDescription(m.Description))
              .AppendLine(" |");
        }
        sb.AppendLine();
    }

    private static void AppendOptions(StringBuilder sb, ServiceMetadata meta)
    {
        if (meta.Options is null)
            return;

        var opts = meta.Options;
        sb.Append("## Options — `").Append(opts.Name).AppendLine("`");
        sb.AppendLine();
        if (!string.IsNullOrEmpty(opts.Description))
        {
            sb.AppendLine(opts.Description);
            sb.AppendLine();
        }

        if (opts.Properties.Count == 0)
        {
            sb.AppendLine("(no public read/write data properties)");
            sb.AppendLine();
        }
        else
        {
            sb.AppendLine("| Property | Type | Default | Description |");
            sb.AppendLine("| --- | --- | --- | --- |");
            foreach (var p in opts.Properties)
            {
                sb.Append("| ").Append(p.Name)
                  .Append(" | `").Append(p.TypeName).Append('`')
                  .Append(" | ").Append(p.DefaultValue is null ? "-" : $"`{p.DefaultValue}`")
                  .Append(" | ").Append(FormatCellDescription(p.Description))
                  .AppendLine(" |");
            }
            sb.AppendLine();
        }

        AppendOptionBuilders(sb, opts);
    }

    private static void AppendOptionBuilders(StringBuilder sb, ServiceOptions opts)
    {
        if (opts.Builders.Count == 0)
            return;

        sb.AppendLine("### Builders");
        sb.AppendLine();
        sb.AppendLine("Builder properties take a delegate that configures a fluent builder. Assign them like any");
        sb.AppendLine("other option property; the delegate receives the builder instance to call the methods below.");
        sb.AppendLine();

        foreach (var b in opts.Builders)
        {
            sb.Append("#### `").Append(b.PropertyName).Append("` — `").Append(b.DelegateSignature).AppendLine("`");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(b.Description))
            {
                sb.AppendLine(b.Description);
                sb.AppendLine();
            }
            sb.Append("Configures `").Append(b.BuilderTypeName).AppendLine("`:");
            sb.AppendLine();

            if (b.BuilderMethods.Count == 0)
            {
                sb.AppendLine("(no public methods)");
                sb.AppendLine();
                continue;
            }

            sb.AppendLine("| Method | Returns | Description |");
            sb.AppendLine("| --- | --- | --- |");
            foreach (var m in b.BuilderMethods)
            {
                var paramList = string.Join(", ", m.Parameters.Select(p => $"{p.TypeName} {p.Name}"));
                sb.Append("| `").Append(m.Name).Append('(').Append(paramList).Append(")`")
                  .Append(" | `").Append(m.ReturnType).Append('`')
                  .Append(" | ").Append(FormatCellDescription(m.Description))
                  .AppendLine(" |");
            }
            sb.AppendLine();
        }
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
