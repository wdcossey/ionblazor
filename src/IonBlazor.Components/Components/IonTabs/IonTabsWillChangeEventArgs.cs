namespace IonBlazor.Components;

public sealed record IonTabsWillChangeEventArgs
{
    public string? Tab { get; internal init; }
}