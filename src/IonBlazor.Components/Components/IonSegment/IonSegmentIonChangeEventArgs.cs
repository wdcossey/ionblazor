namespace IonBlazor.Components;

public sealed record IonSegmentIonChangeEventArgs
{
    public IonSegment? Sender { get; internal init; }

    public string? Value { get; internal init; }

    //public bool? IsTrusted { get; internal set; }
}