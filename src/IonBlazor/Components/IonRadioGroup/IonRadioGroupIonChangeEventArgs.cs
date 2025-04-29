namespace IonBlazor.Components;

public sealed record IonRadioGroupIonChangeEventArgs
{
    public IonRadioGroup? Sender { get; init; }

    [JsonPropertyName("value")]
    public string? Value { get; init; }

    [JsonPropertyName("event")]
    public IonRadioGroupIonChangeEvent? Event { get; init; }
}

public sealed record IonRadioGroupIonChangeEvent
{
    public bool? IsTrusted { get; init; }
}