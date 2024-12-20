namespace IonBlazor.Components;

public sealed record IonSearchbarChangeEventArgs
{
    public string? Value { get; internal init; }
    public bool? IsTrusted { get; internal init; }
}