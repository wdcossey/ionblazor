namespace IonBlazor.Components;

public sealed record IonBackdropTapEventArgs
{
    public IonBackdrop? Sender { get; internal init; }
}