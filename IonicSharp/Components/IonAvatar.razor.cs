namespace IonicSharp.Components;

public partial class IonAvatar : IonComponent, IIonContentComponent
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}