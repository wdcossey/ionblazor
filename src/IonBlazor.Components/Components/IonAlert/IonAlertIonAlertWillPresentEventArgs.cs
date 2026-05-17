namespace IonBlazor.Components;

public sealed record IonAlertIonAlertWillPresentEventArgs
{
    public IonAlert? Sender { get; internal init; }
}