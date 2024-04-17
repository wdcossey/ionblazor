namespace IonicSharp.Components;

public partial class IonGrid : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the grid will have a fixed width based on the screen size.
    /// </summary>
    [Parameter]
    public bool? Fixed { get; set; }
}