namespace IonBlazor.Components;

public sealed record IonPickerColumnIonChangeEventArgs
{
    public IonPickerColumn? Sender { get; internal init; }

    public string? Value { get; internal init; }
}