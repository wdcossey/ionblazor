namespace IonBlazor.Components;

public class IonBackdropTapEventArgs : EventArgs
{
    public IonBackdrop Sender { get; internal init; } = null!;
}