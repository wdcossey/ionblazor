namespace IonBlazor.Components;

public sealed record IonScrollStartEventArgs
{
    [JsonPropertyName("isScrolling")]
    public bool IsScrolling { get; init; }
}