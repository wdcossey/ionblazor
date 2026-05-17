namespace IonBlazor.Components;

public sealed record IonInputOtpCompleteEventArgs : IIonInputArgs
{
    public IonInputOtp? Sender { get; internal init; }

    public string? Value { get; internal init; }
}