namespace IonBlazor.Components;

public record IonPickerLegacyEventArgs<TColumn, TColumnOption, TButton>
    where TColumn: class, IPickerColumn<TColumnOption>
    where TColumnOption: class, IPickerColumnOption
    where TButton: class, IPickerButton
{
    public IonPickerLegacy<TColumn, TColumnOption, TButton>? Sender { get; internal init; }
}