namespace IonBlazor.Components;

public sealed record IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>
    where TColumn: class, IPickerColumn<TColumnOption>
    where TColumnOption: class, IPickerColumnOption
    where TButton: class, IPickerButton
{
    public IonPickerLegacy<TColumn, TColumnOption, TButton> Sender { get; internal set; } = null!;

    public string? Role { get; internal set; }

    public IDictionary<string, PickedColumnOption>? Data { get; internal set; }
}