namespace IonBlazor.Components;

public sealed partial class IonListOf<TItem> : IonList
    where TItem : notnull
{
    [Parameter, EditorRequired]
    public IEnumerable<TItem>? ItemsSource { get; set; }

    [Parameter, EditorRequired]
    public RenderFragment<TItem> ItemTemplate { get; set; } = null!;

    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }
}