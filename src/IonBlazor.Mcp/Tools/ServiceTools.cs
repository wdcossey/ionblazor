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
        "'which IonBlazor overlay services exist'.")]
    public static string ListServices()
    {
        var services = ServiceRegistry.ListAll();

        var sb = new StringBuilder();
        sb.Append("# IonBlazor Services (").Append(services.Count).AppendLine(")");
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
        "Returns full metadata for a single IonBlazor service controller: type-level summary, all public " +
        "static methods (signatures, parameters, return types, summaries), and the associated *Options " +
        "record's properties (name, type, summary). Use this to learn the exact API for " +
        "IonAlertController.PresentAsync, IonToastController.PresentAsync, IonLoadingController.CreateAsync, etc. " +
        "Service name must be exact PascalCase, e.g. 'IonToastController'.")]
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

        AppendMethods(sb, meta);
        AppendOptions(sb, meta);

        return sb.ToString();
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
            sb.AppendLine("(no public read/write properties)");
            sb.AppendLine();
            return;
        }

        sb.AppendLine("| Property | Type | Description |");
        sb.AppendLine("| --- | --- | --- |");
        foreach (var p in opts.Properties)
        {
            sb.Append("| ").Append(p.Name)
              .Append(" | `").Append(p.TypeName).Append('`')
              .Append(" | ").Append(FormatCellDescription(p.Description))
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
