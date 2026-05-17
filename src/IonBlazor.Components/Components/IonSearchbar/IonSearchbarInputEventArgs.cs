namespace IonBlazor.Components;

public sealed record IonSearchbarInputEventArgs
{
    public string? Value { get; internal init; }
    public bool? IsTrusted { get; internal init; }
}