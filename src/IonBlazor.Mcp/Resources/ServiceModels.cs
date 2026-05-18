namespace IonBlazor.Mcp.Resources;

public sealed record ServiceSummary(
    string Name,
    string? OptionsTypeName,
    int MethodCount,
    string? Description = null);

public sealed record ServiceMetadata(
    string Name,
    string FullName,
    IReadOnlyList<ServiceMethod> Methods,
    ServiceOptions? Options,
    string? Description = null);

public sealed record ServiceMethod(
    string Name,
    string ReturnType,
    IReadOnlyList<ServiceMethodParameter> Parameters,
    string? Description = null);

public sealed record ServiceMethodParameter(string Name, string TypeName);

public sealed record ServiceOptions(
    string Name,
    string FullName,
    IReadOnlyList<ServiceOptionProperty> Properties,
    string? Description = null);

public sealed record ServiceOptionProperty(string Name, string TypeName, string? Description = null);
