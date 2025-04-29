namespace IonBlazor.Services;

public sealed record ToastControllerOptions
{
    public delegate void ButtonBuilder(ToastButtonBuilder builder);

    [JsonIgnore]
    public ButtonBuilder? ButtonsBuilder { get; set; }

    [JsonPropertyName("header"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Header { get; set; }

    [JsonPropertyName("message"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; } = 2 * 1000;

    [JsonPropertyName("position"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Position { get; set; }

    [JsonPropertyName("translucent"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Translucent { get; set; }

    [JsonPropertyName("animated"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Animated { get; set; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string>? HtmlAttributes { get; set; }

    [JsonPropertyName("color"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Color { get; set; }

    [JsonPropertyName("positionAnchor"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PositionAnchor { get; set; }

    [JsonPropertyName("mode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mode { get; set; }

    [JsonPropertyName("keyboardClose"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? KeyboardClose { get; set; }

    [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    [JsonIgnore]
    public Action<IonToastDismissEventArgs>? OnDidDismiss { get; set; }

}