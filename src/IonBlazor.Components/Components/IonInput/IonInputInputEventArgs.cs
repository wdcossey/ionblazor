namespace IonBlazor.Components;

public sealed record IonInputInputEventArgs : IIonInputEventArgs
{
    public IonInput? Sender { get; internal init; }

    public string? Value { get; set; }

    public IonInputEvent Event { get; internal init; }
}