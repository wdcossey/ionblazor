namespace IonBlazor.Components;

public record AlertInputNumber : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "number";
}