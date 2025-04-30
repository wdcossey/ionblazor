using System.Collections.ObjectModel;

namespace IonicTest.Components;

public sealed partial class PlaygroundConsole : ComponentBase
{
    private readonly ObservableCollection<ConsoleItem> _itemsSource = [];

    //[Parameter] public RenderFragment<ConsoleItem> ItemTemplate { get; set; } = null!;

    public void Add(string message, string? type = null, string? icon = null)
    {
        _itemsSource.Add(new ConsoleItem
        {
            Message = message,
            Type = type,
            Icon = icon
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _itemsSource.CollectionChanged += (sender, args) =>
        {
            StateHasChanged();
        };
    }
}


internal sealed record ConsoleItem
{
    public required string Message { get; init; }
    public string? Type { get; init; }
    public string? Icon { get; init; }
}