using System.Runtime.Serialization;

namespace IonicSharp.Components;

public partial class IonLoading: IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _didPresentReference ;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionLoadingDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionLoadingDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionLoadingWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionLoadingWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willPresentReference;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <inheritdoc/>
    [Parameter, EditorRequired]
    public RenderFragment? ChildContent { get; set; }
    
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
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public string? EnterAnimation { get; set; }
    
    /// <summary>
    /// Additional attributes to pass to the loader.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public IDictionary<string, string?> HtmlAttributes { get; set; }
    
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
    public EventCallback DidPresent { get; set; }

    /// <summary>
    /// Emitted after the loading has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> IonLoadingDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the loading has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonLoadingDidPresent { get; set; }

    /// <summary>
    /// Emitted before the loading has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> IonLoadingWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the loading has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonLoadingWillPresent { get; set; }

    /// <summary>
    /// Emitted before the loading indicator has dismissed. Shorthand for <see cref="IonLoadingWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonLoadingDismissEventArgs> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the loading indicator has presented. Shorthand for <see cref="IonLoadingWillPresent"/>.
    /// </summary>
    [Parameter]
    public EventCallback WillPresent { get; set; }
    
    public IonLoading()
    {
        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var dismissArgs = GetDismissArgs(args);
            await DidDismiss.InvokeAsync(dismissArgs);
        });
        
        _didPresentReference = IonicEventCallback.Create(async () => await DidPresent.InvokeAsync());
        
        _ionLoadingDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var dismissArgs = GetDismissArgs(args);
            await IonLoadingDidDismiss.InvokeAsync(dismissArgs);
        });
        
        _ionLoadingDidPresentReference = IonicEventCallback.Create(async () => await IonLoadingDidPresent.InvokeAsync());
        
        _ionLoadingWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var dismissArgs = GetDismissArgs(args);
            await IonLoadingWillDismiss.InvokeAsync(dismissArgs);
        });
        
        _ionLoadingWillPresentReference = IonicEventCallback.Create(async () => await IonLoadingWillPresent.InvokeAsync());
        
        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var dismissArgs = GetDismissArgs(args);
            await WillDismiss.InvokeAsync(dismissArgs);
        });
        _willPresentReference = IonicEventCallback.Create(async () => await WillPresent.InvokeAsync());
        
        /*
         * {
  "tagName": "ION-LOADING",
  "detail": {
    "data": null,
    "role": "winner"
  }
}
         */
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, new[]
        {
            IonEvent.Set("didDismiss"           , _didDismissReference ),
            IonEvent.Set("didPresent"           , _didPresentReference  ),
            IonEvent.Set("ionLoadingDidDismiss" , _ionLoadingDidDismissReference),
            IonEvent.Set("ionLoadingDidPresent" , _ionLoadingDidPresentReference ),
            IonEvent.Set("ionLoadingWillDismiss", _ionLoadingWillDismissReference ),
            IonEvent.Set("ionLoadingWillPresent", _ionLoadingWillPresentReference ),
            IonEvent.Set("willDismiss"          , _willDismissReference ),
            IonEvent.Set("willPresent"          , _willPresentReference ),
        });
    }
    
    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync<TData>(TData? data = null, string? role = null) where TData : class => 
        JsComponent.InvokeAsync<bool>("dismiss", _self, data, role);
    
    /// <summary>
    /// Dismiss the loading overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync(string? role = null) => 
        JsComponent.InvokeAsync<bool>("dismiss", _self, role);
    
    /// <summary>
    /// Returns a promise that resolves when the loading did dismiss.
    /// </summary>
    /// <returns></returns>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask OnDidDismissAsync()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Returns a promise that resolves when the loading will dismiss.
    /// </summary>
    /// <returns></returns>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask OnWillDismissAsync()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Present the loading overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public ValueTask PresentAsync() => JsComponent.InvokeVoidAsync("present", _self);
    
    /// <summary>
    /// Sets the <see cref="Message"/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask SetMessageAsync(string? message)
    {
        await JsComponent.InvokeVoidAsync("setMessage", _self, message);
        //this.Message = message;
    }
    
    /// <summary>
    /// Sets the <see cref="Message"/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentWithMessageAsync(string? message)
    {
        await JsComponent.InvokeVoidAsync("presentWithMessage", _self, message);
    }

    private IonLoadingDismissEventArgs GetDismissArgs(JsonObject? args)
    {
        var role = args?["detail"]?["role"]?.GetValue<string>();
        var data = args?["detail"]?["data"];
        return new IonLoadingDismissEventArgs() { Sender = this, Data = data, Role = role };
    }
}

public class IonLoadingDismissEventArgs : EventArgs
{
    public IonLoading Sender { get; internal set; } = null!;
    
    public string? Role { get; internal set; }
    
    public object? Data { get; internal set; }
}

public class IonLoadingSpinner
{
    public const string? Default = null;
    public const string Bubbles = "bubbles";
    public const string Circles = "circles";
    public const string Circular = "circular";
    public const string Crescent = "crescent";
    public const string Dots = "dots";
    public const string Lines = "lines";
    public const string LinesSharp = "lines-sharp";
    public const string LinesSharpSmall = "lines-sharp-small";
    public const string LinesSmall = "lines-small";
    public const string Null = "null";
}