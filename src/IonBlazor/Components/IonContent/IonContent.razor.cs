namespace IonBlazor.Components;

public sealed partial class IonContent : IonContentComponent, IIonColorComponent
{
    private DotNetObjectReference<IonicEventCallback<__ionScrollEventArgs?>> _ionScrollReference;
    private DotNetObjectReference<IonicEventCallback<__ionScrollEndEventArgs?>> _ionScrollEndReference;
    private DotNetObjectReference<IonicEventCallback<__ionScrollStartEventArgs?>> _ionScrollStartReference;

    protected override string JsImportName => nameof(IonContent);

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
    public EventCallback<IonContentScrollEventArgs?> IonScroll { get; set; }

    /// <summary>
    /// Emitted when the scroll has ended. This event is disabled by default.
    /// Set <b>scrollEvents</b> to <b>true</b> to enable.
    /// </summary>
    [Parameter]
    public EventCallback<IonScrollEndEventArgs?> IonScrollEnd { get; set; }

    /// <summary>
    /// Emitted when the scroll has started. This event is disabled by default.
    /// Set <b>scrollEvents</b> to <b>true</b> to enable.
    /// </summary>
    [Parameter]
    public EventCallback<IonScrollStartEventArgs?> IonScrollStart { get; set; }

    public IonContent()
    {
        _ionScrollReference = IonicEventCallback<__ionScrollEventArgs?>
            .Create(async args => await IonScroll.InvokeAsync(args?.Detail));

        _ionScrollEndReference = IonicEventCallback<__ionScrollEndEventArgs?>
            .Create(async args => await IonScrollEnd.InvokeAsync(args?.Detail));

        _ionScrollStartReference = IonicEventCallback<__ionScrollStartEventArgs?>
            .Create(async args => await IonScrollStart.InvokeAsync(args?.Detail));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionScroll", _ionScrollReference),
            IonEvent.Set("ionScrollEnd", _ionScrollEndReference),
            IonEvent.Set("ionScrollStart", _ionScrollStartReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionScrollReference.Dispose();
        _ionScrollEndReference.Dispose();
        _ionScrollStartReference.Dispose();
        await base.DisposeAsync();
    }

    /// <summary>
    /// Get the element where the actual scrolling takes place.
    /// This element can be used to subscribe to scroll events or manually modify scrollTop.
    /// However, it's recommended to use the API provided by ion-content: <br/>
    /// i.e. Using ionScroll, ionScrollStart, ionScrollEnd for scrolling events and scrollToPoint()
    /// to scroll the content into a certain point.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public async ValueTask GetScrollElementAsync() =>
        await JsComponent.InvokeAsync<JsonElement?>("getScrollElement", IonElement);

    /// <summary>
    /// Scroll by a specified X/Y distance in the component.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="duration"></param>
    public async ValueTask ScrollByPointAsync(int x, int y, int duration) =>
        await JsComponent.InvokeVoidAsync("scrollByPoint", IonElement, x, y, duration);

    /// <summary>
    /// Scroll to the bottom of the component.
    /// </summary>
    /// <param name="duration"></param>
    public async ValueTask ScrollToBottomAsync(int duration) =>
        await JsComponent.InvokeVoidAsync("scrollToBottom", IonElement, duration);

    /// <summary>
    /// Scroll to a specified X/Y location in the component.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="duration"></param>
    public async ValueTask ScrollToPointAsync(int? x = null, int? y = null, int? duration = null) =>
        await JsComponent.InvokeVoidAsync("scrollToPoint", IonElement, x, y, duration);

    /// <summary>
    /// Scroll to the top of the component.
    /// </summary>
    /// <param name="duration"></param>
    public async ValueTask ScrollToTopAsync(int? duration) =>
        await JsComponent.InvokeVoidAsync("scrollToTop", IonElement, duration);

    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    internal sealed record __ionScrollEventArgs
    {
        [JsonPropertyName("detail")] public IonContentScrollEventArgs Detail { get; init; } = null!;
    }

    internal sealed record __ionScrollEndEventArgs
    {
        [JsonPropertyName("detail")] public IonScrollEndEventArgs Detail { get; init; } = null!;
    }

    internal sealed record __ionScrollStartEventArgs
    {
        [JsonPropertyName("detail")] public IonScrollStartEventArgs Detail { get; init; } = null!;
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
}