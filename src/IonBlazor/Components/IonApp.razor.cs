namespace IonBlazor.Components;

public partial class IonApp: IonComponent, IIonContentComponent, IIonModeComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
    
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Mode { get; set; }
}