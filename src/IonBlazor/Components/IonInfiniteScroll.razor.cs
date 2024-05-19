namespace IonBlazor.Components;

public partial class IonInfiniteScroll : IonComponent, IIonContentComponent
{
    private ElementReference _self;

    private DotNetObjectReference<IonicEventCallback> _ionInfiniteReference;

    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, the infinite scroll will be hidden and scroll event listeners will be removed.
    /// <p/>
    /// Set this to true to disable the infinite scroll from actively trying to receive new data while scrolling.
    /// This is useful when it is known that there is no more data that can be added, and the infinite scroll is no
    /// longer needed.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// The position of the infinite scroll element. The value can be either
    /// <see cref="IonInfiniteScrollPosition.Top"/> or <see cref="IonInfiniteScrollPosition.Bottom"/>.
    /// </summary>
    [Parameter]
    public string? Position { get; set; } = IonInfiniteScrollPosition.Default;

    /// <summary>
    /// The threshold distance from the bottom of the content to call the infinite output event when scrolled.
    /// The threshold value can be either a percent, or in pixels. For example, use the value of <b>10%</b> for the
    /// <b>infinite</b> output event to get called when the user has scrolled 10% from the bottom of the page. Use the value
    /// <b>100px</b> when the scroll is within 100 pixels from the bottom of the page.
    /// </summary>
    [Parameter]
    public string? Threshold { get; set; }

    /// <summary>
    /// Emitted when the scroll reaches the threshold distance. From within your infinite handler, you must call the
    /// infinite scroll's complete() method when your async operation has completed.
    /// </summary>
    [Parameter]
    public EventCallback<IonInfiniteEventArgs> IonInfinite { get; set; }

    public IonInfiniteScroll()
    {
        _ionInfiniteReference = IonicEventCallback.Create(async () => await IonInfinite.InvokeAsync(new IonInfiniteEventArgs() { Sender = this }));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionInfinite", _ionInfiniteReference));
    }

    /// <summary>
    /// Call <see cref="CompleteAsync"/> within the ionInfinite output event handler when your async operation has
    /// completed. For example, the loading state is while the app is performing an asynchronous operation, such as
    /// receiving more data from an AJAX request to add more items to a data list. Once the data has been received and
    /// UI updated, you then call this method to signify that the loading has completed. This method will change the
    /// infinite scroll's state from loading to enabled.
    /// </summary>
    /// <returns></returns>
    public async ValueTask CompleteAsync()
    {
        await JsRuntime.InvokeVoidAsync("IonBlazor.IonInfiniteScroll.complete", _self);
    }

    public class IonInfiniteEventArgs : EventArgs
    {
        public IonInfiniteScroll Sender { get; internal set; } = null!;
    }
}

public static class IonInfiniteScrollPosition
{
    public const string? Default = null;
    public const string Bottom = "bottom";
    public const string Top = "top";
}
