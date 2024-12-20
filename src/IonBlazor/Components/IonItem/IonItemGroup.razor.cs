namespace IonBlazor.Components;

public sealed partial class IonItemGroup: IonContentComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;
}