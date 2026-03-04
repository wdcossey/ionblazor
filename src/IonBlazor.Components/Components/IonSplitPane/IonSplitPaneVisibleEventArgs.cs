namespace IonBlazor.Components;

public sealed record IonSplitPaneVisibleEventArgs
{
    public IonSplitPane? Sender { get; internal init; }
    public bool? Visible { get; internal init; }
}