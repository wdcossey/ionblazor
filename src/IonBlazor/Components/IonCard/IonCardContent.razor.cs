namespace IonBlazor.Components;

public sealed partial class IonCardContent : IonContentComponent, IIonModeComponent
{
    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}