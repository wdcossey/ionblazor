namespace IonBlazor.Components;

public sealed partial class IonItemSliding : IonJsContentComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionDragReference;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the sliding item.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// Emitted when the sliding position changes.
    /// </summary>
    [Parameter]
    public EventCallback<IonDragEventArgs> IonDrag { get; set; }

    internal override string JsImportName => nameof(IonItemSliding);

    public IonItemSliding()
    {
        _ionDragReference = IonicEventCallback<JsonObject?>
            .Create(async args =>
            {
                await IonDrag.InvokeAsync(new IonDragEventArgs
                {
                    Sender = this,
                    Amount = args?["detail"]?["amount"]?.GetValue<double>(),
                    Ratio = args?["detail"]?["ratio"]?.GetValue<double>(),
                });
            });
    }

    /// <summary>
    /// Close the sliding item. Items can also be closed from the List.
    /// </summary>
    public async ValueTask CloseAsync() =>
        await JsComponent.InvokeVoidAsync("close", IonElement);

    /// <summary>
    /// Close all of the sliding items in the list. Items can also be closed from the List.
    /// </summary>
    public async ValueTask CloseOpenedAsync() =>
        await JsComponent.InvokeVoidAsync("closeOpened", IonElement);

    /// <summary>
    /// Get the amount the item is open in pixels.
    /// </summary>
    public async ValueTask<double> GetOpenAmountAsync() =>
        await JsComponent.InvokeAsync<double>("getOpenAmount", IonElement);

    /// <summary>
    /// Get the ratio of the open amount of the item compared to the width of the options. If the number returned is
    /// positive, then the options on the right side are open. If the number returned is negative, then the options
    /// on the left side are open. If the absolute value of the number is greater than 1, the item is open more than
    /// the width of the options.
    /// </summary>
    public async ValueTask<double> GetSlidingRatioAsync() =>
        await JsComponent.InvokeAsync<double>("getSlidingRatio", IonElement);

    /// <summary>
    /// Open the sliding item.
    /// </summary>
    /// <param name="side">Which side of the sliding item to open. If unspecified, the first side with options will open.</param>
    public async ValueTask OpenAsync(string? side = null) =>
        await JsComponent.InvokeVoidAsync("open", IonElement, side);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement, IonEvent.Set("ionDrag", _ionDragReference));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionDragReference.Dispose();
        await base.DisposeAsync();
    }
}

public sealed record IonDragEventArgs
{
    public IonItemSliding? Sender { get; internal init; }
    public double? Amount { get; internal init; }
    public double? Ratio { get; internal init; }
}