namespace IonBlazor.Services;

public sealed record ActionSheetControllerOptions
{
    public delegate void ButtonBuilder(ActionSheetButtonBuilder builder);

    [JsonIgnore]
    public ButtonBuilder? ButtonsBuilder { get; set; }

    [JsonPropertyName("header"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Header { get; set; }

    [JsonPropertyName("subHeader"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SubHeader { get; set; }

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
    public Action<ActionSheetControllerDismissEventArgs>? OnDidDismiss { get; set; }
}