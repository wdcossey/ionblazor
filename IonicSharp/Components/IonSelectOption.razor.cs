namespace IonicSharp.Components;

public partial class IonSelectOption: IonSelectOptionDefault
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
}

public abstract class IonSelectOptionDefault: IonSelectOptionBase<string>
{

}

public abstract class IonSelectOptionBase<TValue>: IonComponent, IIonContentComponent
{
    private Type? _valueType;

    [CascadingParameter(Name = nameof(TValue))]
    public Type? ValueType
    {
        get => _valueType;
        set => _valueType = value;
    }
    
    /// <inheritdoc />
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the select option.
    /// This property does not apply when <see cref="IonSelect.Interface"/>=<see cref="IonSelectInterface.ActionSheet"/>
    /// as <see cref="IonActionSheet{TButtonData}"/> does not allow for disabled buttons.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }
    
    /// <summary>
    /// The text value of the option.
    /// </summary>
    [Parameter, EditorRequired]
    public TValue? Value { get; set; }
}