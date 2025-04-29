namespace IonBlazor.Components;

public sealed partial class IonFabButton : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    private DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private DotNetObjectReference<IonicEventCallback> _ionFocusReference;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the <see cref="IonFabButton"/> will be show a close icon.<br/>
    /// Default: <b>false</b>
    /// </summary>
    [Parameter]
    public bool Activated { get; set; }

    /// <summary>
    /// The icon name to use for the close icon.
    /// This will appear when the fab button is pressed.
    /// Only applies if it is the main button inside of a fab containing a fab list.<br/>
    /// Default: <b>close</b>
    /// </summary>
    [Parameter]
    public string? CloseIcon { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If true, the user cannot interact with the fab button.<br/>
    /// Default: <b>false</b>
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file.
    /// If the attribute has a value, it is used as the pre-filled file name in the
    /// Save prompt (the user can still change the file name if they want).<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter]
    public string? Download { get; set; }

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to.
    /// If this property is set, an anchor tag will be rendered.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object.
    /// The value is a space-separated list of link types.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter]
    public string? Rel { get; set; }

    ///// <summary>
    ///// When using a router, it specifies the transition animation when navigating to another page using href.<br/>
    ///// Default: <b>null</b>
    ///// </summary>
    //[Parameter] public string? RouterAnimation { get; set; }

    /// <summary>
    /// When using a router, it specifies the transition direction when navigating to another page using href.<br/>
    /// Default: <see cref="IonFabButtonRouterDirection.Forward"/>
    /// </summary>
    [Parameter]
    public string? RouterDirection { get; set; } = IonFabButtonRouterDirection.Forward;

    /// <summary>
    /// If true, the fab button will show when in a fab-list.<br/>
    /// Default: <b>false</b>
    /// </summary>
    [Parameter]
    public bool Show { get; set; }

    /// <summary>
    /// The size of the button. Set this to small in order to have a mini fab button.<br/>
    /// Default: <see cref="IonFabButtonSize.Default"/>
    /// </summary>
    [Parameter]
    public string? Size { get; set; } = IonFabButtonSize.Default;

    /// <summary>
    /// Specifies where to display the linked URL. Only applies when an href is provided.<br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// If true, the fab button will be translucent.
    /// Only applies when the mode is "ios" and the device supports <b>backdrop-filter</b>.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter]
    public string? Translucent { get; set; }

    /// <summary>
    /// The type of the button.<br/>
    /// Default: <see cref="IonButtonType.Button"/>
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonButtonType.Button;

    [Parameter]
    public EventCallback IonBlur { get; set; }

    [Parameter]
    public EventCallback IonFocus { get; set; }


    public IonFabButton()
    {
        _ionBlurReference = IonicEventCallback.Create(async () => await IonBlur.InvokeAsync());
        _ionFocusReference = IonicEventCallback.Create(async () => await IonFocus.InvokeAsync());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            _self,
            IonEvent.Set("ionBlur", _ionBlurReference),
            IonEvent.Set("ionFocus", _ionFocusReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionFocusReference.Dispose();
        await base.DisposeAsync();
    }
}