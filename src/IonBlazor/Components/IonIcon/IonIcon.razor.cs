namespace IonBlazor.Components;

public sealed partial class IonIcon : IonComponent, IIonColorComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    [Parameter]
    public string Name { get; set; } = null!;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; } = IonColor.Default;

    [Parameter]
    public string? Size { get; set; }
}