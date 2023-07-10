namespace IonicSharp.Components;

public partial class IonRow : IonComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}