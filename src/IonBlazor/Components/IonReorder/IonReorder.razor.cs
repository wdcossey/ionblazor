namespace IonBlazor.Components;

public sealed partial class IonReorder: IonContentComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }
}