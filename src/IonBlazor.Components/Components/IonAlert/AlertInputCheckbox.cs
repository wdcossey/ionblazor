namespace IonBlazor.Components;

public sealed record AlertInputCheckbox : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "checkbox";
}