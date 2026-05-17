namespace IonBlazor.Components;

public sealed record IonModalDragStartEventArgs
{
    public IonModal? Sender { get; internal init; }
}
