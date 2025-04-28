namespace IonBlazor.Components;

public interface IActionSheetButton
{
    public delegate ValueTask HandlerDelegate(IActionSheetButton? button, int? index);

    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Text { get; }

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Role { get; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Icon { get; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? CssClass { get; }

    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IDictionary<string, string> HtmlAttributes { get; }

    [JsonIgnore]
    HandlerDelegate? Handler { get; }

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? Data { get; }
}

public abstract class ActionSheetButton : IActionSheetButton
{
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
    public IActionSheetButton.HandlerDelegate? Handler { get; set; }

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; set; }
}

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
        this.Handler is null
            ? null
            : (IActionSheetButton.HandlerDelegate)((button, index) =>
            {
                if (button is ActionSheetButton<TData> typedButton)
                {
                    return Handler.Invoke(typedButton, index);
                }
                return ValueTask.CompletedTask;
            });

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; set; }

    [JsonIgnore]
    object? IActionSheetButton.Data => this.Data;
}