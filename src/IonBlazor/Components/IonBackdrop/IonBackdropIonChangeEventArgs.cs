namespace IonBlazor.Components;

public class IonBackdropIonChangeEventArgs : EventArgs
{
    public IonBackdrop Sender { get; internal init; } = null!;
}