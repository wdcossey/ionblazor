namespace IonBlazor.Components;

public sealed record IonAlertWillPresentEventArgs
{
    public IonAlert? Sender { get; internal init; }
}