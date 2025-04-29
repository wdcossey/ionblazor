namespace IonBlazor.Components;

public sealed record IonAccordionGroupIonChangeEventArgs
{
    public IonAccordionGroup? Sender { get; internal init; } = null!;
    public string[]? Value { get; internal init; }
}