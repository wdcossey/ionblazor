namespace IonBlazor.Components;

public sealed record IonSegmentViewScrollEvent
{
    public IonSegmentView? Sender { get; internal init; }
    public decimal ScrollRatio { get; internal init; }
    public bool IsManualScroll { get; internal init; }
}