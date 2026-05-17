namespace IonBlazor.Components;

public sealed record IonContentScrollEventArgs
{
    [JsonPropertyName("scrollTop")]
    public double ScrollTop { get; init; }

    [JsonPropertyName("scrollLeft")]
    public double ScrollLeft { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("event")]
    public IonContentScrollEvent? Event { get; init; }

    [JsonPropertyName("startX")]
    public double StartX { get; init; }

    [JsonPropertyName("startY")]
    public double StartY { get; init; }

    [JsonPropertyName("startTime")]
    public double StartTime { get; init; }

    [JsonPropertyName("currentX")]
    public double CurrentX { get; init; }

    [JsonPropertyName("currentY")]
    public double CurrentY { get; init; }

    [JsonPropertyName("velocityX")]
    public double VelocityX { get; init; }

    [JsonPropertyName("velocityY")]
    public double VelocityY { get; init; }

    [JsonPropertyName("deltaX")]
    public double DeltaX { get; init; }

    [JsonPropertyName("deltaY")]
    public double DeltaY { get; init; }

    [JsonPropertyName("currentTime")]
    public double CurrentTime { get; init; }

    [JsonPropertyName("isScrolling")]
    public bool IsScrolling { get; init; }
}