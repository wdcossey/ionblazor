namespace IonBlazor.Mcp.Resources;

public sealed record ComponentSummary(
    string Name,
    string BaseClass,
    bool HasChildContent,
    bool HasJsInterop,
    string? JsImportName,
    IReadOnlyList<string> Interfaces,
    IReadOnlyList<string> BindProperties,
    string? Description = null);

public sealed record ComponentMetadata(
    string Name,
    string FullName,
    string BaseClass,
    bool HasChildContent,
    bool HasJsInterop,
    string? JsImportName,
    IReadOnlyList<string> Interfaces,
    IReadOnlyList<ComponentParameter> Parameters,
    IReadOnlyList<ComponentCascadingParameter> CascadingParameters,
    IReadOnlyList<ComponentEvent> Events,
    IReadOnlyList<ComponentBind> Binds,
    IReadOnlyList<ComponentMethod> JsMethods,
    string? Description = null);

public sealed record ComponentParameter(string Name, string TypeName, string? Description = null);

public sealed record ComponentCascadingParameter(string Name, string TypeName, string? CascadingName, string? Description = null);

public sealed record ComponentEvent(string Name, string? PayloadType, string? Description = null);

public sealed record ComponentBind(string PropertyName, string ChangedCallbackName, string? InputCallbackName, string? Description = null);

public sealed record ComponentMethod(string Name, string ReturnType, IReadOnlyList<ComponentMethodParameter> Parameters, string? Description = null);

public sealed record ComponentMethodParameter(string Name, string TypeName);
