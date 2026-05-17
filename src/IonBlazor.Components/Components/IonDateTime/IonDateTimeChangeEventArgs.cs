namespace IonBlazor.Components;

public sealed record IonDateTimeChangeEventArgs
{
    public IonDateTime? Sender { get; internal init; }

    public string? Value { get; internal set; }
}