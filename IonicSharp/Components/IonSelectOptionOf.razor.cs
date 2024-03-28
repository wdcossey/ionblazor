namespace IonicSharp.Components;

public partial class IonSelectOptionOf<TValue>: IonSelectOptionBase<TValue>
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /*/// <summary>
    /// The text value of the option.
    /// </summary>
    [Parameter, EditorRequired]
    public TValue? Value { get; set; }*/
}