namespace IonBlazor.Components.Abstractions;

public interface IPickerButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Text { get; }
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Role { get; }
    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? CssClass { get; }

    [JsonIgnore]
    Func<Dictionary<string, PickedColumnOption>?, ValueTask>? Handler { get; }
}