using System.Collections.Immutable;

namespace IonBlazor.Components;

public sealed partial class IonBreadcrumbs : IonJsContentComponent, IIonModeComponent, IIonColorComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionCollapsedClickReference;

    internal override string JsImportName => nameof(IonBreadcrumbs);

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
    public string? Mode { get; set; } = IonMode.Undefined;

    /// <summary>
    /// Emitted when the collapsed indicator is clicked on.
    /// </summary>
    [Parameter]
    public EventCallback<IonBreadcrumbsCollapsedClickEventArgs> IonCollapsedClick { get; set; }

    public IonBreadcrumbs()
    {
        _ionCollapsedClickReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            await IonCollapsedClick.InvokeAsync(new IonBreadcrumbsCollapsedClickEventArgs
            {
                Sender = this,
                CollapsedBreadcrumbs = args?["detail"]?
                    .AsArray()
                    .OfType<JsonObject>()
                    .Select(obj => new CollapsedBreadcrumb
                    {
                        Active = obj["active"]?.GetValue<bool>(),
                        Collapsed = obj["collapsed"]?.GetValue<bool>(),
                        Disabled = obj["disabled"]?.GetValue<bool>(),
                        Download = obj["download"]?.GetValue<string>(),
                        Href = obj["href"]?.GetValue<string>(),
                        Last = obj["last"]?.GetValue<bool>(),
                        TextContent = obj["textContent"]?.GetValue<string>(),
                    }).ToImmutableList()
            });
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