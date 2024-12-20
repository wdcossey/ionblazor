namespace IonBlazor.Components;

public class ActionSheetButton<TData>
    where TData: class, IActionSheetButtonData
{
    public delegate ValueTask HandlerDelegate<TButtonData>(ActionSheetButton<TButtonData>? button, int? index)
        where TButtonData : class, IActionSheetButtonData;

    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string> HtmlAttributes { get; set; } = null!;

    [JsonIgnore]
    public HandlerDelegate<TData>? Handler { get; set; }

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; set; }
}