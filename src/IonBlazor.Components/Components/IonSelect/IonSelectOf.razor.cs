using Microsoft.AspNetCore.Components;

namespace IonBlazor.Components;

public partial class IonSelectOf<TItem> : IonSelect<TItem>
    where TItem : notnull
{
    /// <summary>
    /// A property name or function used to compare object values
    /// </summary>
    [Parameter]
    public EventCallback<IonSelectCompareEventArgs<TItem>> CompareWith { get; init; }

    [Parameter, EditorRequired]
    public IEnumerable<TItem> ItemsSource { get; set; } = null!;

    [Parameter, EditorRequired]
    public RenderFragment<KeyValuePair<int, TItem>> ItemTemplate { get; set; } = null!;

    protected override async Task IonChangeCallback(JsonObject? args)
    {
        var value = args?["detail"]?["value"];
        var indexes = value switch
        {
            null => [],
            JsonArray jsonArray => jsonArray.Deserialize<string[]>()?.Select(int.Parse).ToArray() ?? [],
            _ => [int.Parse(value.GetValue<string>())]
        };

        var compareArgs = new IonSelectCompareEventArgs<TItem>
        {
            Sender = this
        };

        await CompareWith.InvokeAsync(compareArgs);

        IList<TItem> values = ItemsSource
            .Select((s, i) => new KeyValuePair<int, TItem>(i, s))
            .Where(w => compareArgs.Compare(indexes, w))
            .Select(s => s.Value)
            .ToList();

        await IonChange.InvokeAsync(new IonSelectChangeEventArgs<TItem>
        {
            Sender = this,
            Value = new IonSelectValue<TItem>(values)
        });
    }



}