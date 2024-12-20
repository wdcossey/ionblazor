namespace IonBlazor.Components;

public sealed partial class IonPicker : IonContentComponent, IIonModeComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; }
}