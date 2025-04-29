namespace IonBlazor.Components;

public class ActionSheetButton<TData> : IActionSheetButton
    where TData: class, IActionSheetButtonData
{
    public delegate ValueTask HandlerDelegate(ActionSheetButton<TData> button, int? index);

    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string> HtmlAttributes { get; set; } = new Dictionary<string, string>();

    [JsonIgnore]
    public HandlerDelegate? Handler { get; set; }

    [JsonIgnore]
    IActionSheetButton.HandlerDelegate? IActionSheetButton.Handler =>
        Handler is not null
            ? (button, index) => button is ActionSheetButton<TData> data ? Handler(data, index) : ValueTask.CompletedTask
            : null;

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; set; }

    [JsonIgnore]
    object? IActionSheetButton.Data => this.Data;
}