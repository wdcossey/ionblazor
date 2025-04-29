namespace IonBlazor.Components;

public sealed partial class IonCardContent : IonContentComponent, IIonModeComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}