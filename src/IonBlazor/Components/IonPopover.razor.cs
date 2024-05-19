namespace IonBlazor.Components;

public partial class IonPopover : IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionPopoverDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionPopoverDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionPopoverWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionPopoverWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willPresentReference;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Describes how to align the popover content with the <b>reference</b> point.
    /// Defaults to <see cref="IonPopoverAlignment.Center"/> for <see cref="IonMode.iOS"/> mode, and
    /// <see cref="IonPopoverAlignment.Start"/> for <see cref="IonMode.MaterialDesign"/> mode.
    /// </summary>
    [Parameter] public string? Alignment { get; set; } = IonPopoverAlignment.Default;
    
    /// <summary>
    /// If <b>true</b>, the popover will animate.
    /// </summary>
    [Parameter] public bool? Animated { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the popover will display an arrow that points at the <b>reference</b> when running in
    /// <see cref="IonMode.iOS"/> mode. Does not apply in <see cref="IonMode.MaterialDesign"/> mode.
    /// </summary>
    [Parameter] public bool? Arrow { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the popover will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter] public bool? BackdropDismiss { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the popover will be automatically dismissed when the content has been clicked.
    /// </summary>
    [Parameter] public bool? DismissOnSelect { get; set; }
    
    ///// <summary>
    ///// The event to pass to the popover animation.
    ///// </summary>
    //[Parameter] public object? Event { get; set; }

    /// <summary>
    /// If <b>true</b>, the popover will open. If <b>false</b>, the popover will close.
    /// Use this if you need finer grained control over presentation, otherwise just use the popoverController or the
    /// <b>trigger</b> property. Note: <see cref="IsOpen"/> will not automatically be set back to <b>false</b> when the
    /// popover dismisses. You will need to do that in your code.
    /// </summary>
    [Parameter] public bool? IsOpen { get; set; }

    public void SetIsOpen(bool value)
    {
        IsOpen = value;
        StateHasChanged();
    }
    
    /// <summary>
    /// If <b>true</b>, the component passed into <see cref="IonPopover"/> will automatically be mounted when the
    /// popover is created. The component will remain mounted even when the popover is dismissed. However, the
    /// component will be destroyed when the popover is destroyed. This property is not reactive and should only be
    /// used when initially creating a popover.
    /// <p/>
    ///Note: This feature only applies to inline popovers in JavaScript frameworks such as Angular, React, and Vue.
    /// </summary>
    [Parameter] public bool? KeepContentsMounted { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.
    /// </summary>
    [Parameter] public bool? KeyboardClose { get; set; }

    //LeaveAnimation
    
    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Describes what to position the popover relative to.
    /// If <see cref="IonPopoverReference.Trigger"/>, the popover will be positioned relative to the trigger
    /// button.
    /// If passing in an event, this is determined via event.target.
    /// If <see cref="IonPopoverReference.Event"/>, the popover will be positioned relative to the x/y coordinates
    /// of the trigger action.
    /// If passing in an event, this is determined via event.clientX and event.clientY.
    /// </summary>
    [Parameter] public string? Reference { get; set; } = IonPopoverReference.Default;

    /// <summary>
    /// If <b>true</b>, a backdrop will be displayed behind the popover. This property controls whether or not the
    /// backdrop darkens the screen when the popover is presented. It does not control whether or not the backdrop
    /// is active or present in the DOM.
    /// </summary>
    [Parameter] public bool? ShowBackdrop { get; set; }

    /// <summary>
    /// Describes which side of the reference point to position the popover on.
    /// The <see cref="IonPopoverSide.Start"/> and <see cref="IonPopoverSide.End"/> values are RTL-aware,
    /// and the <see cref="IonPopoverSide.Left"/> and <see cref="IonPopoverSide.Right"/> values are not.
    /// </summary>
    [Parameter] public string? Side { get; set; } = IonPopoverSide.Default;

    /// <summary>
    /// Describes how to calculate the popover width.
    /// If <see cref="IonPopoverSize.Cover"/>, the popover width will match the width of the trigger.
    /// If <see cref="IonPopoverSize.Auto"/>, the popover width will be determined by the content in the popover.
    /// </summary>
    [Parameter] public string? Size { get; set; } = IonPopoverSize.Default;

    /// <summary>
    /// If <b>true</b>, the popover will be translucent.
    /// Only applies when the mode is <see cref="IonMode.iOS"/> and the device supports
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/backdrop-filter#Browser_compatibility">backdrop-filter</a>.
    /// </summary>
    [Parameter] public bool? Translucent { get; set; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the popover to open.
    /// Use the <see cref="TriggerAction"/> property to customize the interaction that results in the popover opening.
    /// </summary>
    [Parameter] public string? Trigger { get; set; }

    /// <summary>
    /// Describes what kind of interaction with the trigger that should cause the popover to open.
    /// Does not apply when the <see cref="Trigger"/> property is <b>null</b>.
    /// If <see cref="IonPopoverTriggerAction.Click"/>, the popover will be presented when the trigger is left clicked.
    /// If <see cref="IonPopoverTriggerAction.Hover"/>, the popover will be presented when a pointer hovers over the
    /// trigger.
    /// If <see cref="IonPopoverTriggerAction.ContextMenu"/>, the popover will be presented when the trigger is right
    /// clicked on desktop and long pressed on mobile.
    /// This will also prevent your device's normal context menu from appearing.
    /// </summary>
    [Parameter] public string? TriggerAction { get; set; } = IonPopoverTriggerAction.Default;
    
    [Parameter] public EventCallback DidDismiss { get; set; }
    [Parameter] public EventCallback DidPresent { get; set; }
    [Parameter] public EventCallback IonPopoverDidDismiss { get; set; }
    [Parameter] public EventCallback IonPopoverDidPresent { get; set; }
    [Parameter] public EventCallback IonPopoverWillDismiss { get; set; }
    [Parameter] public EventCallback IonPopoverWillPresent { get; set; }
    [Parameter] public EventCallback WillDismiss { get; set; }
    [Parameter] public EventCallback WillPresent { get; set; }

    public IonPopover()
    {
        _didDismissReference = IonicEventCallback.Create(async () => await DidDismiss.InvokeAsync(this));
        
        _didPresentReference = IonicEventCallback.Create(async () => await DidPresent.InvokeAsync(this));

        _ionPopoverDidDismissReference = IonicEventCallback.Create(async () => await IonPopoverDidDismiss.InvokeAsync(this));

        _ionPopoverDidPresentReference = IonicEventCallback.Create(async () => await IonPopoverDidPresent.InvokeAsync(this));
        
        _ionPopoverWillDismissReference = IonicEventCallback.Create(async () => await IonPopoverWillDismiss.InvokeAsync(this));
        
        _ionPopoverWillPresentReference = IonicEventCallback.Create(async () => await IonPopoverWillPresent.InvokeAsync(this));
        
        _willDismissReference = IonicEventCallback.Create(async () => await WillDismiss.InvokeAsync(this));
        
        _willPresentReference = IonicEventCallback.Create(async () => await WillPresent.InvokeAsync(this));
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await this.AttachIonListenersAsync(_self, new []
        {
            IonEvent.Set("didDismiss"           , _didDismissReference            ),
            IonEvent.Set("didPresent"           , _didPresentReference            ),
            IonEvent.Set("ionPopoverDidDismiss" , _ionPopoverDidDismissReference  ),
            IonEvent.Set("ionPopoverDidPresent" , _ionPopoverDidPresentReference  ),
            IonEvent.Set("ionPopoverWillDismiss", _ionPopoverWillDismissReference ),
            IonEvent.Set("ionPopoverWillPresent", _ionPopoverWillPresentReference ),
            IonEvent.Set("willDismiss"          , _willDismissReference           ),
            IonEvent.Set("willPresent"          , _willPresentReference           )
        });
    }
    
    /*
Methods

dismiss
Description	Dismiss the popover overlay after it has been presented.
Signature	dismiss(data?: any, role?: string, dismissParentPopover?: boolean) => Promise<boolean>

onDidDismiss
Description	Returns a promise that resolves when the popover did dismiss.
Signature	onDidDismiss<T = any>() => Promise<OverlayEventDetail<T>>

onWillDismiss
Description	Returns a promise that resolves when the popover will dismiss.
Signature	onWillDismiss<T = any>() => Promise<OverlayEventDetail<T>>

present
Description	Present the popover overlay after it has been created. Developers can pass a mouse, touch, or pointer event to position the popover relative to where that event was dispatched.
Signature	present(event?: MouseEvent ｜ TouchEvent ｜ PointerEvent ｜ CustomEvent) => Promise<void>
     */
}

public static class IonPopoverAlignment
{
    public const string? Default = null;
    public const string Center = "center";
    public const string End = "end";
    public const string Start = "start";
}

public static class IonPopoverReference
{
    public const string? Default = null;
    public const string Event = "event";
    public const string Trigger = "trigger";
}

public static class IonPopoverSide
{
    public const string? Default = null;
    public const string Bottom = "bottom";
    public const string End = "end";
    public const string Left = "left";
    public const string Right = "right";
    public const string Start = "start";
    public const string Top = "top";
}

public static class IonPopoverSize
{
    public const string? Default = null;
    public const string Auto = "auto";
    public const string Cover = "cover";
}

public static class IonPopoverTriggerAction
{
    public const string? Default = null;
    public const string Click = "click";
    public const string ContextMenu = "context-menu";
    public const string Hover = "hover";
}