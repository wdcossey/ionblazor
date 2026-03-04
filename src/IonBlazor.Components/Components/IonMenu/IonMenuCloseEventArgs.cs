namespace IonBlazor.Components;

public sealed record IonMenuCloseEventArgs
{
    public IonMenu? Sender { get; internal init; }
    public string? Role { get; internal init; }
}
