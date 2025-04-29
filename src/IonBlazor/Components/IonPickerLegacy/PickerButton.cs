namespace IonBlazor.Components;

public class PickerButton : IPickerButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonIgnore]
    public Func<Dictionary<string, PickedColumnOption>?, ValueTask>? Handler { get; set; }
}