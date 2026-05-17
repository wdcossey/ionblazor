namespace IonBlazor.Components.Abstractions;

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
    IActionSheetButtonData? Data { get; }
}