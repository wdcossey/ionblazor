namespace IonicSharp.Components;

public partial class IonCardContent : IonComponent, IIonModeComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
    
    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}