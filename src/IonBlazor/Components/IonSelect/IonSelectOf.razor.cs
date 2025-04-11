using Microsoft.AspNetCore.Components;

namespace IonBlazor.Components;

public partial class IonSelectOf<TItem> : IonSelect<TItem>
    where TItem : notnull
{
    [Parameter, EditorRequired] public IEnumerable<TItem> ItemsSource { get; set; } = null!;

    [Parameter, EditorRequired] public RenderFragment<TItem> ItemTemplate { get; set; } = null!;

    protected override async Task IonChangeCallback(JsonObject? args)
    {
        JsonNode? value = args?["detail"]?["value"];
        var indexes = value switch
        {
            null => [],
            JsonArray => value.Deserialize<string[]>(),
            _ => [value.GetValue<string>()]
        };

        var values = ItemsSource
            .Select((s, i) => (Index: (i).ToString(), Value: s))
            .Where(w => indexes?.Contains(w.Index) is true)
            .Select(s => s.Value)
            .ToList();

        await IonChange.InvokeAsync(new IonSelectChangeEventArgs<TItem>
        {
            Sender = this,
            Value = new IonSelectValue<TItem>(values ?? [])
        });
    }
}