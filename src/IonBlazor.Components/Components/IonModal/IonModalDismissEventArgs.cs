namespace IonBlazor.Components;

public sealed record IonModalDismissEventArgs
{
    [JsonIgnore]
    public IonModal? Sender { get; internal init; }

    [JsonPropertyName("role")]
    public string? Role { get; internal init; }

    [JsonPropertyName("data")]
    public object? Data { get; internal init; }
}