namespace IonBlazor.Components;

public sealed record AlertInputRadio : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "radio";
}