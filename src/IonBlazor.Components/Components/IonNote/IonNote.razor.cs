namespace IonBlazor.Components;

public sealed partial class IonNote : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter] public string? Color { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
}