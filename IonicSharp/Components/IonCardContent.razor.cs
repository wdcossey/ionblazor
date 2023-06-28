namespace IonicSharp.Components;

public partial class IonCardContent : IonComponent
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}