namespace IonBlazor.Components;

public sealed partial class IonRow : IonContentComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }
}