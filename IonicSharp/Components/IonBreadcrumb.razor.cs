namespace IonicSharp.Components;

public partial class IonBreadcrumb : IonComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionBlurReference = null;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionFocusReference = null;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, the breadcrumb will take on a different look to show that it is the currently active breadcrumb.
    /// Defaults to <b>true</b> for the last breadcrumb if it is not set on any.
    /// </summary>
    [Parameter] public bool? Active { get; set; }
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the breadcrumb.
    /// </summary>
    [Parameter] public bool? Disabled { get; set; }
    
    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it, so the user will be prompted
    /// to save it as a local file. If the attribute has a value, it is used as the pre-filled file name in the
    /// Save prompt (the user can still change the file name if they want).
    /// </summary>
    [Parameter] public string? Download { get; set; }
    
    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set, an anchor tag will
    /// be rendered.
    /// </summary>
    [Parameter] public string? Href { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// Specifies the relationship of the target object to the link object. The value is a space-separated list of
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types">link types</a>.
    /// </summary>
    [Parameter] public string? Rel { get; set; }
    
    /// <summary>
    /// If <b>true</b>, show a separator between this breadcrumb and the next. Defaults to <b>true</b> for all
    /// breadcrumbs except the last.
    /// </summary>
    [Parameter] public bool? Separator { get; set; }
    
    /// <summary>
    /// Specifies where to display the linked URL. <br/>
    /// Only applies when an href is provided. <br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".
    /// </summary>
    [Parameter] public string? Target { get; set; }

    /// <summary>
    /// Emitted when the breadcrumb loses focus.
    /// </summary>
    [Parameter] public EventCallback OnBlur { get; set; }
    
    /// <summary>
    /// Emitted when the breadcrumb has focus.
    /// </summary>
    [Parameter] public EventCallback OnFocus { get; set; }
    
    public IonBreadcrumb()
    {
        _ionBlurReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(async args =>
        {
            await OnBlur.InvokeAsync();
        }));
        
        _ionFocusReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(async args =>
        {
            await OnFocus.InvokeAsync();
        }));
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new []
        {
            new { Event = "ionBlur", Ref = _ionBlurReference},
            new { Event = "ionFocus", Ref = _ionFocusReference}
        }, _self);
        
    }
}