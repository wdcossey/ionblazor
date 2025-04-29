namespace IonBlazor.Components;

public sealed partial class IonMenu: IonContentComponent, IIonModeComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionDidCloseReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionDidOpenReference ;
    private readonly DotNetObjectReference<IonicEventCallback> _ionWillCloseReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionWillOpenReference;

    protected override string JsImportName => nameof(IonMenu);

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

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionDidClose", _ionDidCloseReference),
            IonEvent.Set("ionDidOpen", _ionDidOpenReference),
            IonEvent.Set("ionWillClose", _ionWillCloseReference),
            IonEvent.Set("ionWillOpen", _ionWillOpenReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionDidCloseReference.Dispose();
        _ionDidOpenReference.Dispose();
        _ionWillCloseReference.Dispose();
        _ionWillOpenReference.Dispose();
        await base.DisposeAsync();
    }

    /// <summary>
    /// Closes the menu. If the menu is already closed or it can't be closed, it returns false.
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> CloseAsync(bool? animated = null) =>
        await JsComponent.InvokeAsync<bool>("close", IonElement, animated);

    /// <summary>
    /// Returns true is the menu is active.
    /// <br/><br/>
    /// A menu is active when it can be opened or closed, meaning it's enabled and it's not part of a ion-split-pane.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> IsActiveAsync() =>
        await JsComponent.InvokeAsync<bool>("isActive", IonElement);

    /// <summary>
    /// Returns true is the menu is open.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> IsOpenAsync() =>
        await JsComponent.InvokeAsync<bool>("isOpen", IonElement);

    /// <summary>
    /// Opens the menu. If the menu is already open or it can't be opened, it returns false.
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> OpenAsync(bool? animated = null) =>
        await JsComponent.InvokeAsync<bool>("open", IonElement, animated);

    /// <summary>
    /// Opens or closes the button. If the operation can't be completed successfully, it returns false.
    /// </summary>
    /// <param name="shouldOpen"></param>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> SetOpenAsync(bool shouldOpen, bool? animated = null) =>
        await JsComponent.InvokeAsync<bool>("setOpen", IonElement, shouldOpen, animated);

    /// <summary>
    /// Toggles the menu. If the menu is already open, it will try to close, otherwise it will try to open it.
    /// If the operation can't be completed successfully, it returns false.
    /// </summary>
    /// <param name="animated"></param>
    /// <returns></returns>
    public async ValueTask<bool> ToggleAsync(bool? animated = null) =>
        await JsComponent.InvokeAsync<bool>("toggle", IonElement, animated);

}