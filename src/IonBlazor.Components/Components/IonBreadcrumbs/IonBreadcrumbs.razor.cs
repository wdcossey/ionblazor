namespace IonBlazor.Components;

public sealed partial class IonBreadcrumbs : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionCollapsedClickReference;

    protected override string JsImportName => nameof(IonBreadcrumbs);

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; }

    /// <summary>
    /// The number of breadcrumbs to show after the collapsed indicator.
    /// If itemsBeforeCollapse + itemsAfterCollapse is greater than maxItems,
    /// the breadcrumbs will not be collapsed.
    /// </summary>
    [Parameter]
    public uint? ItemsAfterCollapse { get; init; }

    /// <summary>
    /// The number of breadcrumbs to show before the collapsed indicator.
    /// If itemsBeforeCollapse + itemsAfterCollapse is greater than maxItems,
    /// the breadcrumbs will not be collapsed.
    /// </summary>
    [Parameter]
    public uint? ItemsBeforeCollapse { get; init; }

    /// <summary>
    /// The maximum number of breadcrumbs to show before collapsing.
    /// </summary>
    [Parameter]
    public uint? MaxItems { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Emitted when the collapsed indicator is clicked on.
    /// </summary>
    [Parameter]
    public EventCallback<IDictionary<string, string?>> IonCollapsedClick { get; set; }

    public IonBreadcrumbs()
    {
        _ionCollapsedClickReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?.AsArray().ToDictionary(k => k!["href"]?.GetValue<string>()!,
                v => v?["textContent"]?.GetValue<string>());

            await IonCollapsedClick.InvokeAsync(value);
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await JsComponent.InvokeVoidAsync("attachIonCollapsedClickListener", IonElement, _ionCollapsedClickReference);
    }

    public override async ValueTask DisposeAsync()
    {
        _ionCollapsedClickReference.Dispose();
        await base.DisposeAsync();
    }

    public async ValueTask SetMaxItemsAsync(uint? value)
    {
        await JsComponent.InvokeVoidAsync("maxItems", IonElement, value);
    }
}