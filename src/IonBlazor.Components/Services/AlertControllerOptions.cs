namespace IonBlazor.Services;

public sealed record AlertControllerOptions
{
    public delegate void ButtonBuilder(AlertButtonBuilder builder);

    public delegate void InputBuilder(AlertInputBuilder builder);

    [JsonIgnore]
    public ButtonBuilder? ButtonsBuilder { get; set; }

    [JsonIgnore]
    public InputBuilder? InputsBuilder { get; set; }

    [JsonPropertyName("header"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Header { get; set; }

    [JsonPropertyName("subHeader"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SubHeader { get; set; }

    [JsonPropertyName("message"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("backdropDismiss"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? BackdropDismiss { get; set; }

    [JsonPropertyName("translucent"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Translucent { get; set; }

    [JsonPropertyName("animated"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Animated { get; set; }

    [JsonPropertyName("mode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mode { get; set; }

    [JsonPropertyName("keyboardClose"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? KeyboardClose { get; set; }

    [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string>? HtmlAttributes { get; set; }

    [JsonIgnore]
    public Action<IonAlertDismissEventArgs>? OnDidDismiss { get; set; }
}