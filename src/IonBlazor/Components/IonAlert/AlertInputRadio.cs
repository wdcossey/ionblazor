namespace IonBlazor.Components;

public record AlertInputRadio : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "radio";
}