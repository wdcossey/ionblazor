namespace IonBlazor.Components;

public sealed partial class IonListOf<TItem> : IonContentComponent, IIonList, IIonModeComponent
    where TItem : notnull
{
    [Parameter, EditorRequired]
    public IEnumerable<TItem>? ItemsSource { get; set; }

    [Parameter, EditorRequired]
    public RenderFragment<TItem> ItemTemplate { get; set; } = null!;

    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    private Func<ValueTask<bool>> _closeSlidingItemsWrapper = null!;

    protected override string JsImportName => nameof(IonList);

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public bool? Inset { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Lines { get; set; } = IonListLines.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <inheritdoc/>
    public ValueTask<bool> CloseSlidingItemsAsync() => _closeSlidingItemsWrapper();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _closeSlidingItemsWrapper = () => JsComponent.InvokeAsync<bool>("closeSlidingItems", IonElement);
    }

    public override async ValueTask DisposeAsync()
    {
        _closeSlidingItemsWrapper = null!;
        await base.DisposeAsync();
    }
}