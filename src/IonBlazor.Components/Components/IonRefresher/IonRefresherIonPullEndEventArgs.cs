namespace IonBlazor.Components;

public sealed record IonRefresherIonPullEndEventArgs()
{
    public IonRefresher? Sender { get; internal init; }

    public RefresherPullEndEventDetailReason Reason { get; internal init; }
}