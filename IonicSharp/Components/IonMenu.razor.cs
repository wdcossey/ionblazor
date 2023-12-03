namespace IonicSharp.Components;

public partial class IonMenu: IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _ionDidCloseReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionDidOpenReference ;
    private readonly DotNetObjectReference<IonicEventCallback> _ionWillCloseReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionWillOpenReference;
    
    private readonly Lazy<ValueTask<IJSObjectReference>> _lazyIonComponentJs;
    private readonly Func<bool?,ValueTask<bool>> _closeJsWrapper;
    private readonly Func<ValueTask<bool>> _isActiveJsWrapper;
    private readonly Func<ValueTask<bool>> _isOpenJsWrapper;
    private readonly Func<bool?, ValueTask<bool>> _openJsWrapper;
    private readonly Func<bool,bool?,ValueTask<bool>> _setOpenJsWrapper;
    private readonly Func<bool?,ValueTask<bool>> _toggleJsWrapper;

    /// <inheritdoc/>
    [Parameter, EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The <b>id</b> of the main content. When using a router this is typically <b>ion-router-outlet</b>.
    /// When not using a router, this is typically your main view's <see cref="IonContent"/>.
    /// This is not the id of the <see cref="IonContent"/> inside of your <see cref="IonMenu"/>.
    /// </summary>
    [Parameter]
    public string? ContentId { get; set; }

    /// <summary>
    /// If <b>true</b>, the menu is disabled.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// The edge threshold for dragging the menu open.
    /// If a drag/swipe happens over this value, the menu is not triggered.
    /// </summary>
    [Parameter]
    public int? MaxEdgeStart { get; set; }

    /// <summary>
    /// An id for the menu.
    /// </summary>
    [Parameter]
    public string? MenuId { get; set; }

    /// <summary>
    /// Which side of the view the menu should be placed.
    /// </summary>
    [Parameter]
    public string? Side { get; set; } = IonMenuSide.Default;

    /// <summary>
    /// If <b>true</b>, swiping the menu is enabled.
    /// </summary>
    [Parameter]
    public bool? SwipeGesture { get; set; }

    /// <summary>
    /// The display type of the menu. Available options: "overlay", "reveal", "push".
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonMenuType.Default;
    
    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// Emitted when the menu is closed.
    /// </summary>
    [Parameter]
    public EventCallback IonDidClose { get; set; }
    
    /// <summary>
    /// Emitted when the menu is open.
    /// </summary>
    [Parameter]
    public EventCallback IonDidOpen { get; set; }
    
    /// <summary>
    /// Emitted when the menu is about to be closed.
    /// </summary>
    [Parameter]
    public EventCallback IonWillClose { get; set; }
    
    /// <summary>
    /// Emitted when the menu is about to be opened.
    /// </summary>
    [Parameter]
    public EventCallback IonWillOpen { get; set; }
    
    public IonMenu()
    {
        _lazyIonComponentJs = new Lazy<ValueTask<IJSObjectReference>>(() => JsRuntime.ImportAsync("ionMenu"));

        _closeJsWrapper = async animated => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("close", _self, animated);
        _isActiveJsWrapper = async () => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("isActive", _self);
        _isOpenJsWrapper = async () => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("isOpen", _self);
        _openJsWrapper = async animated => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("open", _self, animated);
        _setOpenJsWrapper = async (shouldOpen, animated) => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("setOpen", _self, shouldOpen, animated);
        _toggleJsWrapper = async animated => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("toggle", _self, animated);
        
        _ionDidCloseReference = IonicEventCallback.Create(async () => await IonDidClose.InvokeAsync());
        _ionDidOpenReference = IonicEventCallback.Create(async () => await IonDidOpen.InvokeAsync());
        _ionWillCloseReference = IonicEventCallback.Create(async () => await IonWillClose.InvokeAsync());
        _ionWillOpenReference = IonicEventCallback.Create(async () => await IonWillOpen.InvokeAsync());
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, new[]
        {
            IonEvent.Set("ionDidClose" , _ionDidCloseReference ),
            IonEvent.Set("ionDidOpen"  , _ionDidOpenReference  ),
            IonEvent.Set("ionWillClose", _ionWillCloseReference),
            IonEvent.Set("ionWillOpen" , _ionWillOpenReference ),
        });
    }
    
    /// <summary>
    /// Closes the menu. If the menu is already closed or it can't be closed, it returns false.
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> CloseAsync(bool? animated = null) => await _closeJsWrapper.Invoke(animated);

    /// <summary>
    /// Returns true is the menu is active.
    /// <br/><br/>
    /// A menu is active when it can be opened or closed, meaning it's enabled and it's not part of a ion-split-pane.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> IsActiveAsync() => await _isActiveJsWrapper.Invoke();
    
    /// <summary>
    /// Returns true is the menu is open.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> IsOpenAsync() => await _isOpenJsWrapper.Invoke();
    
    /// <summary>
    /// Opens the menu. If the menu is already open or it can't be opened, it returns false.
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> OpenAsync(bool? animated = null) => await _openJsWrapper.Invoke(animated);
    
    /// <summary>
    /// Opens or closes the button. If the operation can't be completed successfully, it returns false.
    /// </summary>
    /// <param name="shouldOpen"></param>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> SetOpenAsync(bool shouldOpen, bool? animated = null) => await _setOpenJsWrapper.Invoke(shouldOpen, animated);
    
    /// <summary>
    /// Toggles the menu. If the menu is already open, it will try to close, otherwise it will try to open it.
    /// If the operation can't be completed successfully, it returns false.
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> ToggleAsync(bool? animated = null) => await _toggleJsWrapper.Invoke(animated);
    
}

public static class IonMenuType
{
    public const string? Default = null;
    public const string Overlay = "overlay";
    public const string Reveal = "reveal";
    public const string Push = "push";
}


public static class IonMenuSide
{
    public const string? Default = null;
    public const string End = "end";
    public const string Start = "start";
}
