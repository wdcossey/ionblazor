namespace IonBlazor.Components;

public sealed record IonInputOtpInputEventArgs : IIonInputEventArgs
{
    public IonInputOtp? Sender { get; internal init; }

    public string? Value { get; internal init; }

    public IonInputEvent Event { get; internal init; }
}