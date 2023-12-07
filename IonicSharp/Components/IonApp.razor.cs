namespace IonicSharp.Components;

public partial class IonApp: IonComponent, IIonContentComponent, IIonModeComponent
{
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Mode { get; set; }
}