namespace IonBlazor.Components;

public sealed record AlertInputTextArea : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "textarea";
}