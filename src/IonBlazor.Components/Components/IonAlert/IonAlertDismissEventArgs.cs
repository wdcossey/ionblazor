namespace IonBlazor.Components;

public sealed record IonAlertDismissEventArgs
{
    public IonAlert? Sender { get; internal init; }

    public string? Role { get; internal init; }

    public IAlertValues? Values { get; internal init; }
}