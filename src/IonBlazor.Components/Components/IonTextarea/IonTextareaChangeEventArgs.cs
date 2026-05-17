namespace IonBlazor.Components;

public sealed record IonTextareaChangeEventArgs : IIonInputEventArgs
{
    public IonTextarea? Sender { get; internal init; }

    public string? Value { get; internal init; }

    public IonInputEvent Event { get; internal init; }
}