namespace IonBlazor.Components;

public record AlertInputTextArea : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "textarea";
}