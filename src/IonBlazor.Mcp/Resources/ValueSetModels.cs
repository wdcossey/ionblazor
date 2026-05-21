namespace IonBlazor.Mcp.Resources;

public sealed record ValueSetSummary(
    string Name,
    string FullName,
    int ValueCount,
    string? Description = null);

public sealed record ValueSetMetadata(
    string Name,
    string FullName,
    IReadOnlyList<ValueSetEntry> Values,
    string? Description = null);

public sealed record ValueSetEntry(
    string ConstantName,
    string? Value,
    string? Description = null);
