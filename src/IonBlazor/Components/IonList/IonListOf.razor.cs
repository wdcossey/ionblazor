namespace IonBlazor.Components;

public sealed partial class IonListOf<TItem> : IonList
    where TItem : notnull
{
    [Parameter, EditorRequired] public IEnumerable<TItem> ItemsSource { get; set; } = null!;

    [Parameter, EditorRequired] public RenderFragment<TItem> ItemTemplate { get; set; } = null!;
}