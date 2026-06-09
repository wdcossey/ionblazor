namespace IonBlazor.Mcp.Tools;

/// <summary>
/// Shared, descriptive error-message builders for the single-entity getter tools
/// (<c>get_component_metadata</c>, <c>get_service_metadata</c>, <c>get_value_set</c>).
/// Returning these strings — rather than letting an unhandled exception bubble up as the
/// opaque "An error occurred invoking '&lt;tool&gt;'" — makes failures self-explanatory to an
/// LLM caller: every message names the tool, the accepted parameter(s), what was actually
/// received, and how to discover valid names.
/// </summary>
internal static class ToolErrors
{
    /// <summary>
    /// Built when a getter is called with no recognized name parameter supplied. Reports the
    /// accepted parameter (plus the generic <c>name</c> alias), the values actually received for
    /// the recognized keys, and the matching <c>list_*</c> tool to discover valid names.
    /// </summary>
    public static string MissingName(
        string toolName,
        string primaryParameter,
        string listTool,
        IReadOnlyList<(string Key, string? Value)> received)
    {
        var receivedText = string.Join(", ", received.Select(r => $"{r.Key}={Format(r.Value)}"));
        return $"Error [{toolName}]: no name supplied. Accepted parameter: '{primaryParameter}' " +
               $"(alias: 'name'). Received: {receivedText}. " +
               $"Pass the entity name as '{primaryParameter}' or 'name', or call {listTool} to list valid names.";
    }

    /// <summary>
    /// Built when a getter is called with a recognized name parameter that names an entity which
    /// does not exist. Reports the accepted parameter(s), the requested value, and a hint to call
    /// the matching <c>list_*</c> tool.
    /// </summary>
    public static string NotFound(
        string toolName,
        string entityLabel,
        string primaryParameter,
        string requested,
        string listTool)
    {
        return $"Error [{toolName}]: {entityLabel} '{requested}' not found. " +
               $"Accepted parameter: '{primaryParameter}' (alias: 'name'). " +
               $"Call {listTool} to list valid names.";
    }

    private static string Format(string? value) =>
        value is null ? "(null)" : $"\"{value}\"";
}