namespace IonBlazor.Components;

public sealed record IonInputChangeEventArgs : IIonInputEventArgs
{
    public IonInput? Sender { get; internal init; }

    public string? Value { get; internal init; }

    public IonInputEvent Event { get; internal init; }
}