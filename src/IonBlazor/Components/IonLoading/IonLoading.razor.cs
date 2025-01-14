namespace IonBlazor.Components;

public sealed partial class IonLoading: IonContentComponent, IIonLoading, IIonModeComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _didPresentReference ;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionLoadingDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionLoadingDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionLoadingWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionLoadingWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willPresentReference;

    protected override string JsImportName => nameof(IonLoading);

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If <b>true</b>, the loading indicator will animate.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// If <b>true</b>, the loading indicator will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool? BackdropDismiss { get; set; }

    /// <summary>
    /// Additional classes to apply for custom CSS.
    /// If multiple classes are provided they should be separated by spaces.
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Number of milliseconds to wait before dismissing the loading indicator.
    /// </summary>
    [Parameter]
    public int? Duration { get; set; }

    /// <summary>
    /// Animation to use when the loading indicator is presented.
    /// </summary>
    [Obsolete("Not available in Blazor/Razor", true)]
    public string? EnterAnimation { get; set; }

    /// <summary>
    /// Additional attributes to pass to the loader.<br/>
    /// The is not available in Blazor/Razor, use
    /// <a href="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/splat-attributes-and-arbitrary-parameters?view=aspnetcore-8.0" >attribute splatting</a>
    /// </summary>
    [Obsolete("Not available in Blazor/Razor, use attribute splatting", true)]
    public IDictionary<string, string?> HtmlAttributes { get; set; } = null!;

    /// <summary>
    /// If <b>true</b>, the loading indicator will open.
    /// If <b>false</b>, the loading indicator will close.
    /// Use this if you need finer grained control over presentation, otherwise just use the loadingController or the
    /// <b>trigger</b> property.
    /// Note: <see cref="IsOpen"/> will not automatically be set back to <b>false</b> when the loading indicator
    /// dismisses. You will need to do that in your code.
    /// </summary>
    [Parameter]
    public bool? IsOpen { get; set; }

    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.
    /// </summary>
    [Parameter]
    public bool? KeyboardClose { get; set; }

    /// <summary>
    /// Optional text content to display in the loading indicator.
    /// <br/><br/>
    /// This property accepts custom HTML as a string. Content is parsed as plaintext by default.
    /// <b>innerHTMLTemplatesEnabled</b> must be set to <b>true</b> in the Ionic config before custom HTML can be used.
    /// </summary>
    [Parameter]
    public string? Message { get; set; }

    /// <summary>
    /// If <b>true</b>, a backdrop will be displayed behind the loading indicator.
    /// </summary>
    [Parameter]
    public bool? ShowBackdrop { get; set; }

    /// <summary>
    /// The name of the spinner to display.
    /// </summary>
    [Parameter]
    public string? Spinner { get; set; } = IonLoadingSpinner.Default;

    /// <summary>
    /// If <b>true</b>, the loading indicator will be translucent.
    /// Only applies when the mode is <see cref="IonMode.iOS"/> and the device supports backdrop-filter.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the loading indicator to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the loading indicator has dismissed. Shorthand for <see cref="IonLoadingDidDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the loading indicator has presented. Shorthand for <see cref="IonLoadingDidPresent"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingPresentEventArgs> DidPresent { get; set; }

    /// <summary>
    /// Emitted after the loading has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> IonLoadingDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the loading has presented.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingPresentEventArgs> IonLoadingDidPresent { get; set; }

    /// <summary>
    /// Emitted before the loading has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> IonLoadingWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the loading has presented.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingPresentEventArgs> IonLoadingWillPresent { get; set; }

    /// <summary>
    /// Emitted before the loading indicator has dismissed. Shorthand for <see cref="IonLoadingWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the loading indicator has presented. Shorthand for <see cref="IonLoadingWillPresent"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingPresentEventArgs> WillPresent { get; set; }

    public IonLoading()
    {
        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var dismissArgs = GetDismissArgs(args);
            await DidDismiss.InvokeAsync(dismissArgs);
        });

        _didPresentReference = IonicEventCallback.Create(async () =>
        {
            IonLoadingPresentEventArgs presentArgs = GetPresentArgs();
            await DidPresent.InvokeAsync(presentArgs);
        });

        _ionLoadingDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var dismissArgs = GetDismissArgs(args);
            await IonLoadingDidDismiss.InvokeAsync(dismissArgs);
        });

        _ionLoadingDidPresentReference = IonicEventCallback.Create(async () =>
        {
            IonLoadingPresentEventArgs presentArgs = GetPresentArgs();
            await IonLoadingDidPresent.InvokeAsync(presentArgs);
        });

        _ionLoadingWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonLoadingDismissEventArgs dismissArgs = GetDismissArgs(args);
            await IonLoadingWillDismiss.InvokeAsync(dismissArgs);
        });

        _ionLoadingWillPresentReference = IonicEventCallback.Create(async () =>
        {
            IonLoadingPresentEventArgs presentArgs = GetPresentArgs();
            await IonLoadingWillPresent.InvokeAsync(presentArgs);
        });

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonLoadingDismissEventArgs dismissArgs = GetDismissArgs(args);
            await WillDismiss.InvokeAsync(dismissArgs);
        });

        _willPresentReference = IonicEventCallback.Create(async () =>
        {
            IonLoadingPresentEventArgs presentArgs = GetPresentArgs();
            await WillPresent.InvokeAsync(presentArgs);
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("didDismiss", _didDismissReference),
            IonEvent.Set("didPresent", _didPresentReference),
            IonEvent.Set("ionLoadingDidDismiss", _ionLoadingDidDismissReference),
            IonEvent.Set("ionLoadingDidPresent", _ionLoadingDidPresentReference),
            IonEvent.Set("ionLoadingWillDismiss", _ionLoadingWillDismissReference),
            IonEvent.Set("ionLoadingWillPresent", _ionLoadingWillPresentReference),
            IonEvent.Set("willDismiss", _willDismissReference),
            IonEvent.Set("willPresent", _willPresentReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _didDismissReference.Dispose();
        _didPresentReference.Dispose();
        _ionLoadingDidDismissReference.Dispose();
        _ionLoadingDidPresentReference.Dispose();
        _ionLoadingWillDismissReference.Dispose();
        _ionLoadingWillPresentReference.Dispose();
        _willDismissReference.Dispose();
        _willPresentReference.Dispose();
        await base.DisposeAsync();
    }

    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync<TData>(TData? data = null, string? role = null) where TData : class =>
        JsComponent.InvokeAsync<bool>("dismiss", IonElement, data, role);

    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync(string? role = null) =>
        JsComponent.InvokeAsync<bool>("dismiss", IonElement, role);

    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public ValueTask PresentAsync() =>
        JsComponent.InvokeVoidAsync("present", IonElement);

    /// <summary>
    /// Sets the <see cref="Message"/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask SetMessageAsync(string? message) =>
        await JsComponent.InvokeVoidAsync("setMessage", IonElement, message);

    /// <summary>
    /// Sets the <see cref="Message"/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentWithMessageAsync(string? message) =>
        await JsComponent.InvokeVoidAsync("presentWithMessage", IonElement, message);

    private IonLoadingDismissEventArgs GetDismissArgs(JsonObject? args)
    {
        var role = args?["detail"]?["role"]?.GetValue<string>();
        var data = args?["detail"]?["data"];
        var htmlAttributes = args?["htmlAttributes"]?.Deserialize<Dictionary<string, string>>();

        return new IonLoadingDismissEventArgs()
        {
            Sender = this,
            Data = data,
            Role = role,
            HtmlAttributes = htmlAttributes
        };
    }

    private IonLoadingPresentEventArgs GetPresentArgs()
    {
        return new IonLoadingPresentEventArgs
        {
            Sender = this
        };
    }
}