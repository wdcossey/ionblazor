namespace IonBlazor.Components;

public sealed record IonReorderGroupIonItemReorderEventArgs
{
    public IonReorderGroup? Sender { get; internal init; }
    public int? From { get; internal init; }
    public int? To { get; internal init; }
}