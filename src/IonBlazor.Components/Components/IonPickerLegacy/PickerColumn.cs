namespace IonBlazor.Components;

public class PickerColumn : IPickerColumn<PickerColumnOption>
{
    [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; } = null!;
    [JsonPropertyName("align"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Align { get; set; }

    [JsonPropertyName("selectedIndex"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SelectedIndex { get; set; }

    [JsonPropertyName("prevSelected"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PrevSelected { get; set; }

    [JsonPropertyName("prefix"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Prefix { get; set; }

    [JsonPropertyName("suffix"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Suffix { get; set; }

    [JsonPropertyName("options"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PickerColumnOption[] Options { get; set; } = null!;

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("columnWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ColumnWidth { get; set; }

    [JsonPropertyName("prefixWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PrefixWidth { get; set; }

    [JsonPropertyName("suffixWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SuffixWidth { get; set; }

    [JsonPropertyName("optionsWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OptionsWidth { get; set; }
}