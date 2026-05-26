namespace IonBlazor.Mcp.Resources;

/// <summary>
/// Distinguishes the two service patterns in the registry.
/// </summary>
public enum ServiceKind
{
    /// <summary>
    /// Modern pattern: non-<c>ComponentBase</c> class registered via <c>AddIonBlazor()</c> and
    /// consumed with <c>@inject</c>. Methods are instance methods.
    /// </summary>
    Injected,

    /// <summary>
    /// Legacy pattern: <c>ComponentBase</c> subclass rendered as a tag at the app root, with
    /// static methods that read a static <c>IJSRuntime</c> field captured during the first render.
    /// Being phased out in favour of <see cref="Injected"/>.
    /// </summary>
    Legacy,
}

public sealed record ServiceSummary(
    string Name,
    ServiceKind Kind,
    string? OptionsTypeName,
    int MethodCount,
    string? Description = null);

public sealed record ServiceMetadata(
    string Name,
    string FullName,
    ServiceKind Kind,
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
    IReadOnlyList<ServiceOptionBuilder> Builders,
    string? Description = null);

public sealed record ServiceOptionProperty(
    string Name,
    string TypeName,
    string? DefaultValue = null,
    string? Description = null);

public sealed record ServiceOptionBuilder(
    string PropertyName,
    string DelegateSignature,
    string BuilderTypeName,
    IReadOnlyList<ServiceMethod> BuilderMethods,
    string? Description = null);
