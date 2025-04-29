namespace IonBlazor.Components;

public partial class IonSegmentContent : IonContentComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }
}