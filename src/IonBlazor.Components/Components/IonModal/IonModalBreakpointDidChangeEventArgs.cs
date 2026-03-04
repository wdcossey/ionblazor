namespace IonBlazor.Components;

public sealed record IonModalBreakpointDidChangeEventArgs
{
    public IonModal? Sender { get; internal init; }
    public double? Breakpoint { get; internal init; }
}
