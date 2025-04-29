namespace IonBlazor.Components;

public sealed record IonModalCanDismissEventArgs
{
    [JsonIgnore]
    public IonModal? Sender { get; internal init; }

    [JsonPropertyName("canDismiss")]
    public bool CanDismiss { get; set; } = true;
}