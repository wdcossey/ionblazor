namespace IonBlazor.Components;

public sealed record IonPickerLegacyButtonHandlerEventArgs : IonPickerLegacyEventArgs
{
    public int? Index { get; internal init; }
    public IPickerButton? Button { get; internal init; }
    public Dictionary<string, PickedColumnOption>? Value { get; internal init; }
}