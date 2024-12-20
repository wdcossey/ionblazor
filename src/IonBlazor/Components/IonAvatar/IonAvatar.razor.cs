namespace IonBlazor.Components;

public sealed partial class IonAvatar : IonContentComponent
{
    private ElementReference _self;

    /// <inheritdoc />
    public override ElementReference IonElement => _self;
}