namespace IonBlazor.Components;

public sealed partial class IonListHeader : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// How the bottom border should be displayed on the list header.
    /// </summary>
    [Parameter]
    public string? Lines { get; set; } = IonListHeaderLines.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}