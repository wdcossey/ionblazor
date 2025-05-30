﻿namespace IonBlazor.Components;

public sealed partial class IonModal : IonContentComponent, IIonModeComponent
{
    private readonly string _id = $"{Guid.NewGuid():N}";

    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionBreakpointDidChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionModalDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionModalDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionModalWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionModalWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willPresentReference;
    private readonly DotNetObjectReference<IonicEventCallbackResult<bool>> _canDismissReference;

    protected override string JsImportName => nameof(IonModal);

    private string Script => Breakpoints?.Length > 0 ?
                              $$"""
                              <script>
                                var modal = document.querySelector(`[is-modal="{{_id}}"]`);
                                if (modal) {
                                    modal.initialBreakpoint = {{InitialBreakpoint}};
                                    modal.breakpoints = [{{string.Join(",", Breakpoints)}}];
                                }
                  
                              </script>
                              """ : "";

    //private readonly DotNetObjectReference<IonicEventCallbackResult<JsonObject?, bool>>? _canDismissCallbackReference;
    //private Func<Task<bool>>? _canDismissCallback;

    /// <inheritdoc/>
    [Parameter, EditorRequired]
    public override RenderFragment? ChildContent { get; init; }

    /// <summary>
    /// If <b>true</b>, the modal will animate.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// A decimal value between 0 and 1 that indicates the point after which the backdrop will begin to fade
    /// in when using a sheet modal. Prior to this point, the backdrop will be hidden and the content underneath
    /// the sheet can be interacted with.
    /// This value is exclusive meaning the backdrop will become active after the value specified.
    /// </summary>
    [Parameter]
    public double? BackdropBreakpoint { get; set; }

    /// <summary>
    /// If <b>true</b>, the modal will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool? BackdropDismiss { get; set; }

    /// <summary>
    /// The breakpoints to use when creating a sheet modal. Each value in the array must be a decimal
    /// between 0 and 1 where 0 indicates the modal is fully closed and 1 indicates the modal is fully open.
    /// Values are relative to the height of the modal, not the height of the screen. One of the values in this array
    /// must be the value of the initialBreakpoint property. For example: [0, .25, .5, 1]
    /// </summary>
    [Parameter]
    public double[]? Breakpoints { get; set; } = null;

    public async ValueTask SetBreakpointsAsync(params double[]? breakpoints) =>
        await JsComponent.InvokeVoidAsync("breakpoints", IonElement, breakpoints);

    /*/// <summary>
    /// Determines whether or not a modal can dismiss when calling the dismiss method.
    /// <br/><br/>
    /// If the value is <b>true</b> or the value's function returns true, the modal will close when trying to dismiss.
    /// If the value is <b>false</b> or the value's function returns false,
    /// the modal will not close when trying to dismiss.
    /// </summary>
    [Parameter]
    public IIonModalCanDismiss? CanDismiss { get; set; }

    public async ValueTask SetCanDismissAsync(bool value)
    {
        _canDismissCallback = null!;
        await JsComponent!.InvokeVoidAsync("canDismiss", _self, value);
    }

    public ValueTask SetCanDismissAsync(Func<Task<bool>> callback)
    {
        _canDismissCallback = callback;
        return ValueTask.CompletedTask;
    }*/

    /// <summary>
    /// Animation to use when the modal is presented.
    /// </summary>
    [Parameter] public string? EnterAnimation { get; set; }

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
    public string? HandleBehavior { get; set; } = IonModalHandleBehavior.Default;

    /// <summary>
    /// Additional attributes to pass to the loader.<br/>
    /// The is not available in Blazor/Razor, use
    /// <a href="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/splat-attributes-and-arbitrary-parameters?view=aspnetcore-8.0" >attribute splatting</a>
    /// </summary>
    [Obsolete("Not available in Blazor/Razor, use attribute splatting", true)]
    [Parameter] public Dictionary<string, object> HtmlAttributes { get; set; } = null!;

    /// <summary>
    /// A decimal value between 0 and 1 that indicates the initial point the modal will open at when creating a
    /// sheet modal. This value must also be listed in the breakpoints array.
    /// </summary>
    [Parameter] public double? InitialBreakpoint { get; set; }

    public async ValueTask SetInitialBreakpointAsync(double? value) =>
        await JsComponent.InvokeVoidAsync("initialBreakpoint", IonElement, value);

    /// <summary>
    /// If <b>true</b>, the modal will open. If false, the modal will close. Use this if you need finer grained control
    /// over presentation, otherwise just use the modalController or the trigger property.
    /// Note: isOpen will not automatically be set back to false when the modal dismisses.
    /// You will need to do that in your code.
    /// </summary>
    [Parameter]
    public bool? IsOpen { get; set; }

    public async ValueTask<bool> SetIsOpenAsync(bool value)
    {
        var result = await JsComponent.InvokeAsync<bool>("isOpen", IonElement, value);
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
    public bool? KeepContentsMounted { get; set; }

    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.
    /// </summary>
    [Parameter]
    public bool? KeyboardClose { get; set; }

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
    public bool? ShowBackdrop { get; set; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the modal to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the modal has dismissed. Shorthand for ionModalDidDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonModalDismissEventArgs> DidDismiss { get; set; }

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
    public EventCallback<IonModalDismissEventArgs> IonModalDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the modal has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonModalDidPresent { get; set; }

    /// <summary>
    /// Emitted before the modal has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonModalDismissEventArgs> IonModalWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the modal has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonModalWillPresent { get; set; }

    /// <summary>
    /// Emitted before the modal has dismissed. Shorthand for ionModalWillDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonModalDismissEventArgs> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the modal has presented. Shorthand for ionModalWillPresent.
    /// </summary>
    [Parameter]
    public EventCallback WillPresent { get; set; }

    /// <summary>
    /// Emitted before the modal has presented. Shorthand for ionModalWillPresent.
    /// </summary>
    [Parameter]
    public EventCallback<IonModalCanDismissEventArgs> CanDismiss { get; set; }

    public IonModal()
    {
        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            //IsOpen = false;
            var dismissArgs = GetDismissArgs(args);
            await DidDismiss.InvokeAsync(dismissArgs);
        });

        _didPresentReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            //IsOpen = true;
            await DidPresent.InvokeAsync(this);
        });

        _ionBreakpointDidChangeReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            await IonBreakpointDidChange.InvokeAsync(this);
        });

        _ionModalDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonModalDismissEventArgs dismissArgs = GetDismissArgs(args);
            await IonModalDidDismiss.InvokeAsync(dismissArgs);
        });

        _ionModalDidPresentReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            await IonModalDidPresent.InvokeAsync(this);
        });

        _ionModalWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonModalDismissEventArgs dismissArgs = GetDismissArgs(args);
            await IonModalWillDismiss.InvokeAsync(dismissArgs);
        });

        _ionModalWillPresentReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            await IonModalWillPresent.InvokeAsync(this);
        });

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonModalDismissEventArgs dismissArgs = GetDismissArgs(args);
            await WillDismiss.InvokeAsync(dismissArgs);
        });

        _willPresentReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            await WillPresent.InvokeAsync(this);
        });

        _canDismissReference = IonicEventCallbackResult<bool>
            .Create(async () =>
            {
                IonModalCanDismissEventArgs args = new() { Sender = this };
                await CanDismiss.InvokeAsync(args);
                return args.CanDismiss;
            });
    }

    /// <summary>
    /// Dismiss the modal overlay after it has been presented.
    /// </summary>
    public async ValueTask<bool> DismissAsync(object? data = null, string? role = null) =>
        await JsComponent.InvokeAsync<bool>("dismiss", IonElement, data, role);

    /// <summary>
    /// Returns the current breakpoint of a sheet style modal
    /// </summary>
    public async ValueTask<int> GetCurrentBreakpointAsync() =>
        await JsComponent.InvokeAsync<int>("getCurrentBreakpoint", IonElement);

    /// <summary>
    /// Present the modal overlay after it has been created.
    /// </summary>
    public async ValueTask PresentAsync() =>
        await JsComponent.InvokeVoidAsync("present", IonElement);

    /// <summary>
    /// Move a sheet style modal to a specific breakpoint.
    /// The breakpoint value must be a value defined in your breakpoints array.
    /// </summary>
    /// <param name="value"></param>
    public async ValueTask SetCurrentBreakpointAsync(double value) =>
        await JsComponent.InvokeVoidAsync("setCurrentBreakpoint", IonElement, value);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement,
            IonEvent.Set("didDismiss", _didDismissReference),
            IonEvent.Set("didPresent", _didPresentReference),
            IonEvent.Set("ionBreakpointDidChange", _ionBreakpointDidChangeReference),
            IonEvent.Set("ionModalDidDismiss", _ionModalDidDismissReference),
            IonEvent.Set("ionModalDidPresent", _ionModalDidPresentReference),
            IonEvent.Set("ionModalWillDismiss", _ionModalWillDismissReference),
            IonEvent.Set("ionModalWillPresent", _ionModalWillPresentReference),
            IonEvent.Set("willDismiss", _willDismissReference),
            IonEvent.Set("willPresent", _willPresentReference)
        );

        await JsComponent.InvokeVoidAsync("canDismissCallback", IonElement, _canDismissReference);

        if (Breakpoints?.Length > 0)
        {
            //await JsComponent.InvokeVoidAsync("initialBreakpoint", _self, InitialBreakpoint);
            //await JsComponent.InvokeVoidAsync("breakpoints", _self, Breakpoints);
        }

        if (string.IsNullOrWhiteSpace(EnterAnimation) is false)
            await JsComponent.InvokeVoidAsync("enterAnimation", IonElement, EnterAnimation);
    }

    public override async ValueTask DisposeAsync()
    {
        _didDismissReference.Dispose();
        _didPresentReference.Dispose();
        _ionBreakpointDidChangeReference.Dispose();
        _ionModalDidDismissReference.Dispose();
        _ionModalDidPresentReference.Dispose();
        _ionModalWillDismissReference.Dispose();
        _ionModalWillPresentReference.Dispose();
        _willDismissReference.Dispose();
        _willPresentReference.Dispose();
        _canDismissReference.Dispose();
        await base.DisposeAsync();
    }

    private IonModalDismissEventArgs GetDismissArgs(JsonObject? args)
    {
        var role = args?["detail"]?["role"]?.GetValue<string>();
        var data = args?["detail"]?["data"] switch
        {
            JsonArray array => array.Deserialize<object?[]>(),
            JsonValue jsonValue => jsonValue.GetValue<object?>(),
            _ => null
        };

        return new IonModalDismissEventArgs() { Sender = this, Data = data, Role = role };
    }
}