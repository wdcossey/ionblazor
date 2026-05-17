namespace IonBlazor.Components.Abstractions;

public interface IPickedColumnOption
{
    [JsonPropertyName("text")]
    string Text { get; }

    [JsonPropertyName("value")]
    JsonElement? Value { get; }

    [JsonPropertyName("columnIndex")]
    int? ColumnIndex { get; }

}