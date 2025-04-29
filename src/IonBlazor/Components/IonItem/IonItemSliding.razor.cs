namespace IonBlazor.Components;

public sealed partial class IonItemSliding : IonContentComponent
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
    /// <returns></returns>
    public Task CloseAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Close all of the sliding items in the list. Items can also be closed from the List.
    /// </summary>
    /// <returns></returns>
    public Task CloseOpenedAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get the amount the item is open in pixels.
    /// </summary>
    /// <returns></returns>
    public Task GetOpenAmountAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get the ratio of the open amount of the item compared to the width of the options. If the number returned is
    /// positive, then the options on the right side are open. If the number returned is negative, then the options
    /// on the left side are open. If the absolute value of the number is greater than 1, the item is open more than
    /// the width of the options.
    /// </summary>
    /// <returns></returns>
    public Task GetSlidingRatioAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Open the sliding item.
    /// </summary>
    /// <returns></returns>
    public Task OpenAsync()
    {
        throw new NotImplementedException();
    }

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