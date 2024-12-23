namespace IonBlazor.Components;

public sealed partial class IonReorder: IonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }
}