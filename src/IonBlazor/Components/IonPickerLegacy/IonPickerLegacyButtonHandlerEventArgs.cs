namespace IonBlazor.Components;

public sealed record IonPickerLegacyButtonHandlerEventArgs<TColumn, TColumnOption, TButton> : IonPickerLegacyEventArgs<TColumn, TColumnOption, TButton>
    where TColumn: class, IPickerColumn<TColumnOption>
    where TColumnOption: class, IPickerColumnOption
    where TButton: class, IPickerButton
{
    public int? Index { get; internal init; }
    public TButton? Button { get; internal init; }
    public Dictionary<string, PickedColumnOption>? Value { get; internal init; }
}