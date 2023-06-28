namespace IonicSharp.Components;

public partial class IonThumbnail : IonComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}