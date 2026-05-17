namespace IonBlazor.Components.Abstractions;

public interface IPickerColumn
{
    [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string Name { get; set; }

    [JsonPropertyName("align"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Align { get; set; }

    [JsonPropertyName("selectedIndex"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? SelectedIndex { get; set; }

    [JsonPropertyName("prevSelected"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? PrevSelected { get; set; }

    [JsonPropertyName("prefix"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Prefix { get; set; }

    [JsonPropertyName("suffix"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Suffix { get; set; }

    [JsonPropertyName("options"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IPickerColumnOption[] Options { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? CssClass { get; set; }

    [JsonPropertyName("columnWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? ColumnWidth { get; set; }

    [JsonPropertyName("prefixWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? PrefixWidth { get; set; }

    [JsonPropertyName("suffixWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? SuffixWidth { get; set; }

    [JsonPropertyName("optionsWidth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? OptionsWidth { get; set; }
}