namespace IonBlazor.Components.Abstractions;

public interface IPickerColumnOption
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string Text { get; set; }

    [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? Value { get; set; }

    [JsonPropertyName("disabled"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    bool? Disabled { get; set; }

    [JsonPropertyName("duration"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? Duration { get; set; }

    [JsonPropertyName("transform"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Transform { get; set; }

    [JsonPropertyName("selected"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    bool? Selected { get; set; }

    /// <summary>
    /// The optional text to assign as the aria-label on the picker column option.
    /// </summary>
    [JsonPropertyName("ariaLabel"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? AriaLabel { get; set; }
}