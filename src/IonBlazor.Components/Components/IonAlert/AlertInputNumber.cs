namespace IonBlazor.Components;

public sealed record AlertInputNumber : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "number";
}