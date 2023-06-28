namespace IonicSharp.Components;

public partial class IonReorder: IonComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}