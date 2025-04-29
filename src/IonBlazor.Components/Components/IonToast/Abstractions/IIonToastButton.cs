namespace IonBlazor.Components.Abstractions;

public interface IIonToastButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Text { get; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Icon { get; }

    [JsonPropertyName("side"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Side { get; }

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Role { get; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? CssClass { get; }

    [JsonIgnore]
    Func<IonToastButtonEventArgs, ValueTask>? Handler { get; }
}