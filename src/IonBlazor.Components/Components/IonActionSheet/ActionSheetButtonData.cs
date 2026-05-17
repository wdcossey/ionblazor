namespace IonBlazor.Components;

public class ActionSheetButtonData : IActionSheetButtonData
{
    [JsonPropertyName("action"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Action { get; set; }
}