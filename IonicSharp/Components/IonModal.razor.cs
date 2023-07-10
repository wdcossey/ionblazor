namespace IonicSharp.Components;

public partial class IonModal : IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionBreakpointDidChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionModalDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionModalDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionModalWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionModalWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _willPresentReference;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, the modal will animate.
    /// </summary>
    [Parameter]
    public bool Animated { get; set; } = true;

    /// <summary>
    /// A decimal value between 0 and 1 that indicates the point after which the backdrop will begin to fade
    /// in when using a sheet modal. Prior to this point, the backdrop will be hidden and the content underneath
    /// the sheet can be interacted with.
    /// This value is exclusive meaning the backdrop will become active after the value specified.
    /// </summary>
    [Parameter]
    public double BackdropBreakpoint { get; set; } = 0.0d;

    /// <summary>
    /// If <b>true</b>, the modal will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool BackdropDismiss { get; set; } = true;

    ///// <summary>
    ///// The breakpoints to use when creating a sheet modal. Each value in the array must be a decimal
    ///// between 0 and 1 where 0 indicates the modal is fully closed and 1 indicates the modal is fully open.
    ///// Values are relative to the height of the modal, not the height of the screen. One of the values in this array
    ///// must be the value of the initialBreakpoint property. For example: [0, .25, .5, 1]
    ///// </summary>
    //[Parameter] public double[]? Breakpoints { get; set; } = null;

    /// <summary>
    /// Determines whether or not a modal can dismiss when calling the dismiss method.
    /// <br/><br/>
    /// If the value is <b>true</b> or the value's function returns true, the modal will close when trying to dismiss.
    /// If the value is <b>false</b> or the value's function returns false,
    /// the modal will not close when trying to dismiss.
    /// </summary>
    [Parameter]
    public bool CanDismiss { get; set; } = true;

    public async ValueTask SetCanDismissAsync(bool value)
    {
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonModal.canDismiss", _self, value);
    }

    ///// <summary>
    ///// Animation to use when the modal is presented.
    ///// </summary>
    //[Parameter] public object EnterAnimation { get; set; }

    [Parameter] public bool? Handle { get; set; }

    /// <summary>
    /// The interaction behavior for the sheet modal when the handle is pressed.
    /// <br/><br/>
    /// Defaults to "none", which means the modal will not change size or position when the handle is pressed.
    /// Set to "cycle" to let the modal cycle between available breakpoints when pressed.
    /// <br/><br/>
    /// Handle behavior is unavailable when the handle property is set to false or when the breakpoints property
    /// is not set (using a fullscreen or card modal).
    /// </summary>
    [Parameter]
    public string? HandleBehavior { get; set; } = IonModalHandleBehavior.None;

    ///// <summary>
    ///// Additional attributes to pass to the modal.
    ///// </summary>
    //[Parameter] public Dictionary<string, object> HtmlAttributes { get; set; }

    /// <summary>
    /// A decimal value between 0 and 1 that indicates the initial point the modal will open at when creating a
    /// sheet modal. This value must also be listed in the breakpoints array.
    /// </summary>
    [Parameter]
    public double? InitialBreakpoint { get; set; }

    /// <summary>
    /// If <b>true</b>, the modal will open. If false, the modal will close. Use this if you need finer grained control
    /// over presentation, otherwise just use the modalController or the trigger property.
    /// Note: isOpen will not automatically be set back to false when the modal dismisses.
    /// You will need to do that in your code.
    /// </summary>
    [Parameter]
    public bool IsOpen { get; set; } = false;

    public async ValueTask<bool> SetIsOpenAsync(bool value)
    {
        var result = await JsRuntime.InvokeAsync<bool>("IonicSharp.IonModal.isOpen", _self, value);
        //IsOpen = result;
        return result;
    }

    /// <summary>
    /// If <b>true</b>, the component passed into ion-modal will automatically be mounted when the modal is created.
    /// The component will remain mounted even when the modal is dismissed. However, the component will be destroyed
    /// when the modal is destroyed. This property is not reactive and should only be used when initially creating a
    /// modal.
    /// <br/><br/>
    /// Note: This feature only applies to inline modals in JavaScript frameworks such as Angular, React, and Vue.
    /// </summary>
    [Parameter]
    public bool? KeepContentsMounted { get; set; } = false;

    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.
    /// </summary>
    [Parameter]
    public bool KeyboardClose { get; set; } = true;

    ///// <summary>
    ///// Animation to use when the modal is dismissed.
    ///// </summary>
    //[Parameter] public object? LeaveAnimation { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    ///// <summary>
    ///// The element that presented the modal. This is used for card presentation effects and for stacking multiple
    ///// modals on top of each other. Only applies in iOS mode.
    ///// </summary>
    //[Parameter] public object? PresentingElement { get; set; }

    /// <summary>
    /// If <b>true</b>, a backdrop will be displayed behind the modal. This property controls whether or not the backdrop
    /// darkens the screen when the modal is presented. It does not control whether or not the backdrop is
    /// active or present in the DOM.
    /// </summary>
    [Parameter]
    public bool ShowBackdrop { get; set; } = true;

    /// <summary>
    /// An ID corresponding to the trigger element that causes the modal to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the modal has dismissed. Shorthand for ionModalDidDismiss.
    /// </summary>
    [Parameter]
    public EventCallback DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the modal has presented. Shorthand for ionModalDidPresent.
    /// </summary>
    [Parameter]
    public EventCallback DidPresent { get; set; }

    /// <summary>
    /// Emitted after the modal breakpoint has changed.
    /// </summary>
    [Parameter]
    public EventCallback IonBreakpointDidChange { get; set; }

    /// <summary>
    /// Emitted after the modal has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback IonModalDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the modal has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonModalDidPresent { get; set; }

    /// <summary>
    /// Emitted before the modal has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback IonModalWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the modal has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonModalWillPresent { get; set; }

    /// <summary>
    /// Emitted before the modal has dismissed. Shorthand for ionModalWillDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonModalWillDismissEventArgs> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the modal has presented. Shorthand for ionModalWillPresent.
    /// </summary>
    [Parameter]
    public EventCallback WillPresent { get; set; }

    public IonModal()
    {
        _didDismissReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            /*
{
  "tagName": "ION-MODAL",
  "detail": {
    "role": "backdrop"
  }
}
             */

            //IsOpen = false;
            await DidDismiss.InvokeAsync(this);
        }));

        _didPresentReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            //IsOpen = true;
            await DidPresent.InvokeAsync(this);
        }));

        _ionBreakpointDidChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            await IonBreakpointDidChange.InvokeAsync(this);
        }));

        _ionModalDidDismissReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            /*
{
  "tagName": "ION-MODAL",
  "detail": {
    "role": "backdrop"
  }
}
             */
            await IonModalDidDismiss.InvokeAsync(this);
        }));

        _ionModalDidPresentReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            await IonModalDidPresent.InvokeAsync(this);
        }));

        _ionModalWillDismissReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            /*
{
  "tagName": "ION-MODAL",
  "detail": {
    "role": "backdrop"
  }
}
             */
            await IonModalWillDismiss.InvokeAsync(this);
        }));

        _ionModalWillPresentReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            await IonModalWillPresent.InvokeAsync(this);
        }));

        _willDismissReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            /*
{
  "tagName": "ION-MODAL",
  "detail": {
    "role": "backdrop"
  }
}
             */

            var role = args?["detail"]?["role"]?.GetValue<string>();
            var data = args?["detail"]?["data"]?.GetValue<object>();
            //var to = args?["detail"]?["to"]?.GetValue<int>();
            await WillDismiss.InvokeAsync(
                new IonModalWillDismissEventArgs() { Sender = this, Data = data, Role = role });
        }));

        _willPresentReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            await WillPresent.InvokeAsync(this);
        }));

    }

    /// <summary>
    /// Dismiss the modal overlay after it has been presented.
    /// </summary>
    public async ValueTask<bool> DismissAsync(object? data = null, string? role = null)
    {
        return await JsRuntime.InvokeAsync<bool>("IonicSharp.IonModal.dismiss", _self, data, role);
    }

    /// <summary>
    /// Returns the current breakpoint of a sheet style modal
    /// </summary>
    public async ValueTask<int> GetCurrentBreakpointAsync(object? data = null, string? role = null)
    {
        //TODO: JS not implemented!
        throw new NotImplementedException();
        return await JsRuntime.InvokeAsync<int>("IonicSharp.IonModal.getCurrentBreakpoint", _self);
    }

    /*
Methods
dismiss
Description	
Signature	dismiss(data?: any, role?: string) => Promise<boolean>
getCurrentBreakpoint
Description	
Signature	getCurrentBreakpoint() => Promise<number ｜ undefined>
onDidDismiss
Description	Returns a promise that resolves when the modal did dismiss.
Signature	onDidDismiss<T = any>() => Promise<OverlayEventDetail<T>>
onWillDismiss
Description	Returns a promise that resolves when the modal will dismiss.
Signature	onWillDismiss<T = any>() => Promise<OverlayEventDetail<T>>
present
Description	Present the modal overlay after it has been created.
Signature	present() => Promise<void>
setCurrentBreakpoint
Description	Move a sheet style modal to a specific breakpoint. The breakpoint value must be a value defined in your breakpoints array.
Signature	setCurrentBreakpoint(breakpoint: number) => Promise<void>
     */

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await JsRuntime.InvokeVoidAsync("IonicSharp.attachListeners", new[]
        {
            new { Event = "didDismiss"            , Ref = _didDismissReference             },
            new { Event = "didPresent"            , Ref = _didPresentReference             },
            new { Event = "ionBreakpointDidChange", Ref = _ionBreakpointDidChangeReference },
            new { Event = "ionModalDidDismiss"    , Ref = _ionModalDidDismissReference     },
            new { Event = "ionModalDidPresent"    , Ref = _ionModalDidPresentReference     },
            new { Event = "ionModalWillDismiss"   , Ref = _ionModalWillDismissReference    },
            new { Event = "ionModalWillPresent"   , Ref = _ionModalWillPresentReference    },
            new { Event = "willDismiss"           , Ref = _willDismissReference            },
            new { Event = "willPresent"           , Ref = _willPresentReference            }
        }, _self);
    }
}

public static class IonModalHandleBehavior
{
    public const string Cycle = "cycle";
    public const string None = "none";
}

public class IonModalWillDismissEventArgs : EventArgs
{
    public IonModal Sender { get; internal set; } = null!;
    
    public string? Role { get; internal set; }
    
    public object? Data { get; internal set; }
}