namespace IonBlazor.Components;

public partial class IonPickerColumnOption : IonComponent, IIonColorComponent, IIonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// The parent <see cref="IonPicker"/>.
    /// </summary>
    [CascadingParameter(Name = "ion-picker-column")]
    public IonPickerColumn? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the picker.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; } = false;

    /// <summary>
    /// The selected option in the picker.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }
}