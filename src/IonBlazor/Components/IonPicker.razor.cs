namespace IonBlazor.Components;

public partial class IonPicker : IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; }

    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }
}