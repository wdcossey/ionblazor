namespace IonBlazor.Components;

public sealed record IonAlertIonAlertDidPresentEventArgs
{
    public IonAlert? Sender { get; internal init; }
}