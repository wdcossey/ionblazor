namespace IonBlazor.Components;

public class PickedColumnOption : IPickedColumnOption
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Text { get; init; } = null!;

    [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonElement? Value { get; init; }

    [JsonPropertyName("columnIndex")]
    public int? ColumnIndex { get; init; }
}