namespace IonBlazor.Components;

public class IonToastButton: IIonToastButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    [JsonPropertyName("side"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Side { get; set; } = ToastButtonSide.Default;

    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; } = ToastButtonRole.Default;

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonIgnore]
    public Func<IonToastButtonEventArgs, ValueTask>? Handler { get; set; } = null!;
}