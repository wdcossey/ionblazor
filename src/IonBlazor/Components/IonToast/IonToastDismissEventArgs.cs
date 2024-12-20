namespace IonBlazor.Components;

public sealed record IonToastDismissEventArgs
{
    public IonToast? Sender { get; internal init; }
    public string? Role { get; internal init; }
}