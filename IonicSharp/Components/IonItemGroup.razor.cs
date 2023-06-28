namespace IonicSharp.Components;

public partial class IonItemGroup: IonComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }
}