namespace IonBlazor.Components;

public record AlertButton : IAlertButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string> HtmlAttributes { get; set; } = null!;

    [JsonIgnore]
    public Func<AlertButtonEventArgs, ValueTask>? Handler { get; set; } = null!;
}