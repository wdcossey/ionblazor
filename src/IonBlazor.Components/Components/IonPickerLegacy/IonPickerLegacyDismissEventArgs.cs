namespace IonBlazor.Components;

public sealed record IonPickerLegacyDismissEventArgs
{
    public IonPickerLegacy? Sender { get; internal set; } = null!;

    public string? Role { get; internal set; }

    public IDictionary<string, PickedColumnOption>? Data { get; internal set; }
}