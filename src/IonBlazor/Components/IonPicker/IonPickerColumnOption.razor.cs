namespace IonBlazor.Components;

public sealed partial class IonPickerColumnOption : IonContentComponent, IIonColorComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

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