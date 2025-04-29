namespace IonBlazor.Components;

public record AlertInputCheckbox : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "checkbox";
}