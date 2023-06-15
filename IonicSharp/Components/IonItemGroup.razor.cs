namespace IonicSharp.Components;

public partial class IonItemGroup: IonComponent
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}