using System.Text.Json.Serialization;
using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonContent : IonComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;

    private DotNetObjectReference<IonicEventCallback<__ionScrollEventArgs?>> _ionScrollReference;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionScrollEndReference;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionScrollStartReference;
    
    private readonly Lazy<ValueTask<IJSObjectReference>> _lazyIonComponentJs;
    private readonly Func<int,int,int,ValueTask> _scrollByPointJsWrapper;
    private readonly Func<int,ValueTask> _scrollToBottomJsWrapper;
    private readonly Func<int?,int?,int?,ValueTask> _scrollToPointJsWrapper;
    private readonly Func<int?,ValueTask> _scrollToTopJsWrapper;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If true and the content does not cause an overflow scroll, the scroll interaction will cause a bounce.
    /// If the content exceeds the bounds of ionContent, nothing will change.
    /// Note, this does not disable the system bounce on iOS. That is an OS level setting.
    /// </summary>
    [Parameter]
    public bool? ForceOverscroll { get; set; }

    /// <summary>
    /// If true, the content will scroll behind the headers and footers.
    /// This effect can easily be seen by setting the toolbar to transparent.
    /// </summary>
    [Parameter]
    public bool? Fullscreen { get; set; }

    /// <summary>
    /// Because of performance reasons, ionScroll events are disabled by default,
    /// in order to enable them and start listening from (ionScroll), set this property to true.
    /// </summary>
    [Parameter]
    public bool? ScrollEvents { get; set; }

    /// <summary>
    /// If you want to enable the content scrolling in the X axis, set this property to true.
    /// </summary>
    [Parameter]
    public bool? ScrollX { get; set; }

    /// <summary>
    /// If you want to disable the content scrolling in the Y axis, set this property to false.
    /// </summary>
    [Parameter]
    public bool? ScrollY { get; set; }

    /// <summary>
    /// Emitted while scrolling. This event is disabled by default.
    /// Set <b>scrollEvents</b> to <b>true</b> to enable.
    /// </summary>
    [Parameter]
    public EventCallback<IonScrollEventArgs?> IonScroll { get; set; }

    /// <summary>
    /// Emitted when the scroll has ended. This event is disabled by default.
    /// Set <b>scrollEvents</b> to <b>true</b> to enable.
    /// </summary>
    [Parameter]
    public EventCallback IonScrollEnd { get; set; }

    /// <summary>
    /// Emitted when the scroll has started. This event is disabled by default.
    /// Set <b>scrollEvents</b> to <b>true</b> to enable.
    /// </summary>
    [Parameter]
    public EventCallback IonScrollStart { get; set; }

    public IonContent()
    {
        _lazyIonComponentJs = new Lazy<ValueTask<IJSObjectReference>>(() => JsRuntime.ImportAsync("ionInput"));
        
        _scrollByPointJsWrapper = async (x, y, duration) => await (await _lazyIonComponentJs.Value).InvokeVoidAsync("scrollByPoint", _self, x, y, duration);
        _scrollToBottomJsWrapper = async duration => await (await _lazyIonComponentJs.Value).InvokeVoidAsync("scrollToBottom", _self, duration);
        _scrollToPointJsWrapper = async (x, y, duration) => await (await _lazyIonComponentJs.Value).InvokeVoidAsync("scrollToPoint", _self, x, y, duration);
        _scrollToTopJsWrapper = async duration => await (await _lazyIonComponentJs.Value).InvokeVoidAsync("scrollToTop", _self, duration);
        
        _ionScrollReference = IonicEventCallback<__ionScrollEventArgs?>.Create(async args =>
        {
            await IonScroll.InvokeAsync(args?.Detail);
        });

        _ionScrollEndReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
/*
{
  "tagName": "ION-CONTENT",
  "detail": {
    "isScrolling": false
  }
}
*/
            var isScrolling = args?["detail"]?["isScrolling"]?.GetValue<bool>();
            await IonScrollEnd.InvokeAsync();
        });

        _ionScrollStartReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
/*
{
  "tagName": "ION-CONTENT",
  "detail": {
    "isScrolling": true
  }
}
 */
            var isScrolling = args?["detail"]?["isScrolling"]?.GetValue<bool>();
            await IonScrollStart.InvokeAsync();
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
    
        await this.AttachIonListenersAsync(_self, new IonEvent[]
        {
            IonEvent.Set("ionScroll"     , _ionScrollReference      ),
            IonEvent.Set("ionScrollEnd"  , _ionScrollEndReference   ),
            IonEvent.Set("ionScrollStart", _ionScrollStartReference )
        });
    }

    /// <summary>
    /// Get the element where the actual scrolling takes place.
    /// This element can be used to subscribe to scroll events or manually modify scrollTop.
    /// However, it's recommended to use the API provided by ion-content: <br/>
    /// i.e. Using ionScroll, ionScrollStart, ionScrollEnd for scrolling events and scrollToPoint()
    /// to scroll the content into a certain point.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public async ValueTask<object?> GetScrollElementAsync()
    {
        throw new NotSupportedException();
        await JsRuntime.InvokeAsync<JsonObject>("getScrollElement", _self);
    }

    /// <summary>
    /// Scroll by a specified X/Y distance in the component.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="duration"></param>
    public async ValueTask ScrollByPointAsync(int x, int y, int duration) =>
        await _scrollByPointJsWrapper(x, y, duration);

    /// <summary>
    /// Scroll to the bottom of the component.
    /// </summary>
    /// <param name="duration"></param>
    public async ValueTask ScrollToBottomAsync(int duration) =>
        await _scrollToBottomJsWrapper(duration);

    /// <summary>
    /// Scroll to a specified X/Y location in the component.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="duration"></param>
    public async ValueTask ScrollToPointAsync(int? x = null, int? y = null, int? duration = null) =>
        await _scrollToPointJsWrapper(x, y, duration);
    
    /// <summary>
    /// Scroll to the top of the component.
    /// </summary>
    /// <param name="duration"></param>
    public async ValueTask ScrollToTopAsync(int? duration) =>
        await _scrollToTopJsWrapper(duration);

    internal sealed class __ionScrollEventArgs
    {
        [JsonPropertyName("detail")] public IonScrollEventArgs Detail { get; set; } = null!;
    }

    public sealed class IonScrollEventArgs : EventArgs
    {
        [JsonPropertyName("scrollTop")]
        public double ScrollTop { get; init; }

        [JsonPropertyName("scrollLeft")] 
        public double ScrollLeft { get; init; }

        [JsonPropertyName("type")] 
        public string? Type { get; init; }

        [JsonPropertyName("event")] 
        public IonContentScrollEvent? Event { get; init; }

        [JsonPropertyName("startX")] 
        public double StartX { get; init; }

        [JsonPropertyName("startY")] 
        public double StartY { get; init; }

        [JsonPropertyName("startTime")] 
        public double StartTime { get; init; }

        [JsonPropertyName("currentX")] 
        public double CurrentX { get; init; }

        [JsonPropertyName("currentY")] 
        public double CurrentY { get; init; }

        [JsonPropertyName("velocityX")] 
        public double VelocityX { get; init; }

        [JsonPropertyName("velocityY")] 
        public double VelocityY { get; init; }

        [JsonPropertyName("deltaX")] 
        public double DeltaX { get; init; }

        [JsonPropertyName("deltaY")] 
        public double DeltaY { get; init; }

        [JsonPropertyName("currentTime")] 
        public double CurrentTime { get; init; }

        [JsonPropertyName("isScrolling")] 
        public bool IsScrolling { get; init; }

        public sealed class IonContentScrollEvent
        {
            [JsonPropertyName("isTrusted")] public bool IsTrusted { get; init; }
        }
    }
}