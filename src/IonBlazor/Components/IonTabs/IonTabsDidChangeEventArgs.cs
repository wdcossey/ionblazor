namespace IonBlazor.Components;

public sealed record IonTabsDidChangeEventArgs
{
    public string? Tab { get; internal init; }
}