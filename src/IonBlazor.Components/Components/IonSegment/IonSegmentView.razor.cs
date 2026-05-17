namespace IonBlazor.Components;

public partial class IonSegmentView : IonContentComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionSegmentViewScroll;

    /// <summary>
    /// If <b>true</b>, the segment view cannot be interacted with.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Emitted when the segment view is scrolled.
    /// </summary>
    [Parameter] public EventCallback<IonSegmentViewScrollEvent> IonSegmentViewScroll { get; set; }

    public IonSegmentView()
    {
        _ionSegmentViewScroll = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            JsonNode? detail = args?["detail"];
            var scrollRatio = detail?["scrollRatio"]?.GetValue<decimal>() ?? 0;
            var isManualScroll = detail?["isManualScroll"]?.GetValue<bool>() ?? false;

            IonSegmentViewScrollEvent eventArgs = new()
            {
                Sender = this,
                ScrollRatio = scrollRatio,
                IsManualScroll = isManualScroll
            };
            await IonSegmentViewScroll.InvokeAsync(eventArgs);
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement, IonEvent.Set("ionSegmentViewScroll", _ionSegmentViewScroll ));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionSegmentViewScroll.Dispose();
        await base.DisposeAsync();
    }

}