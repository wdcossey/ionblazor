namespace IonBlazor.Components;

public sealed record IonContentScrollEvent
{
    [JsonPropertyName("isTrusted")] public bool IsTrusted { get; init; }
}