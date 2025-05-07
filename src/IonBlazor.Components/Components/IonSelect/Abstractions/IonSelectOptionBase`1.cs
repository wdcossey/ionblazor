namespace IonBlazor.Components.Abstractions;

public abstract class IonSelectOptionBase<TValue>: IonContentComponent
{
    [CascadingParameter(Name = nameof(TValue))]
    public Type? ValueType { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the select option.
    /// This property does not apply when <see cref="IonSelect{TValue}.Interface"/>=<see cref="IonSelectInterface.ActionSheet"/>
    /// as <see cref="IonActionSheet{TButtonData}"/> does not allow for disabled buttons.
    /// </summary>
    [Parameter] public bool? Disabled { get; set; }

    /// <summary>
    /// The text value of the option.
    /// </summary>
    [Parameter, EditorRequired] public TValue? Value { get; set; }
}