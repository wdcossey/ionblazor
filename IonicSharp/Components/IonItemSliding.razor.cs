namespace IonicSharp.Components;

public partial class IonItemSliding : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionDragReference;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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

        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionDrag", _ionDragReference));
    }
}

public class IonDragEventArgs : EventArgs
{
    public double? Amount { get; internal set; }
    public double? Ratio { get; internal set; }
}