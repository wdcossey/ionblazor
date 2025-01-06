namespace IonBlazor.Components;

public sealed partial class IonListHeader : IonContentComponent, IIonModeComponent, IIonColorComponent
{
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