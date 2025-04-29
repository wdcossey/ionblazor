namespace IonBlazor.Components;

public class PickerColumnOption : IPickerColumnOption
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Text { get; set; } = null!;

    [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; set; }

    [JsonPropertyName("disabled"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }

    [JsonPropertyName("duration"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; set; }

    [JsonPropertyName("transform"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Transform { get; set; }

    [JsonPropertyName("selected"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Selected { get; set; }

    /// <inheritdoc/>
    [JsonPropertyName("ariaLabel"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AriaLabel { get; set; }
}