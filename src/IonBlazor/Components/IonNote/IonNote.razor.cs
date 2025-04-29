namespace IonBlazor.Components;

public sealed partial class IonNote : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter] public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
}