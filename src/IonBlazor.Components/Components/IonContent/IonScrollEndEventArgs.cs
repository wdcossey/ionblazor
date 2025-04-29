namespace IonBlazor.Components;

public sealed record IonScrollEndEventArgs
{
    [JsonPropertyName("isScrolling")]
    public bool IsScrolling { get; init; }
}