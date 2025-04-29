namespace IonBlazor.Components;

public sealed partial class IonTitle: IonContentComponent, IIonColorComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Color { get; set; }

    /// <summary>
    /// The size of the toolbar title.
    /// </summary>
    [Parameter] public string? Size { get; set; } = IonTitleSize.Default;
}