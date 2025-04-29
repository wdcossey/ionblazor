namespace IonBlazor.Components;

public sealed record IonRefresherIonStartEventArgs
{
    public IonRefresher? Sender { get; internal init; }
}