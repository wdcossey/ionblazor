namespace IonBlazor.Components;

public sealed partial class IonBreadcrumbs : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionCollapsedClickReference;

    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// The number of breadcrumbs to show after the collapsed indicator.
    /// If itemsBeforeCollapse + itemsAfterCollapse is greater than maxItems,
    /// the breadcrumbs will not be collapsed.
    /// </summary>
    [Parameter]
    public int? ItemsAfterCollapse { get; set; }

    /// <summary>
    /// The number of breadcrumbs to show before the collapsed indicator.
    /// If itemsBeforeCollapse + itemsAfterCollapse is greater than maxItems,
    /// the breadcrumbs will not be collapsed.
    /// </summary>
    [Parameter]
    public int? ItemsBeforeCollapse { get; set; }

    /// <summary>
    /// The maximum number of breadcrumbs to show before collapsing.
    /// </summary>
    [Parameter]
    public int? MaxItems { get; set; }

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

        await JsComponent.InvokeVoidAsync("attachIonCollapsedClickListener", _self, _ionCollapsedClickReference);
    }

    public override async ValueTask DisposeAsync()
    {
        _ionCollapsedClickReference.Dispose();
        await base.DisposeAsync();
    }
}