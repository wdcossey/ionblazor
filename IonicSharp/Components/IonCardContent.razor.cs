namespace IonicSharp.Components;

public partial class IonCardContent : IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
    
    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}