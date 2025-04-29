namespace IonBlazor.Components;

public sealed record IonAlertDidPresentEventArgs
{
    public IonAlert? Sender { get; internal init; }
}