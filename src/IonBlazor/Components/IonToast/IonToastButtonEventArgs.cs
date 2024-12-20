namespace IonBlazor.Components;

public sealed record IonToastButtonEventArgs
{
    public IonToast? Sender { get; internal init; }
    public int? Index { get; internal init; }
    public IIonToastButton? Button { get; internal init; }
}