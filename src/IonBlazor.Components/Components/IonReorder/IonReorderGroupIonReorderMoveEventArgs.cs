namespace IonBlazor.Components;

public sealed record IonReorderGroupIonReorderMoveEventArgs
{
    public IonReorderGroup? Sender { get; internal init; }
    public int? From { get; internal init; }
    public int? To { get; internal init; }
}
