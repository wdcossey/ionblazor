namespace IonBlazor.Components;

public sealed partial class IonCardSubtitle : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}