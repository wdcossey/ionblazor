namespace IonicSharp.Components;

public partial class IonAvatar : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
}