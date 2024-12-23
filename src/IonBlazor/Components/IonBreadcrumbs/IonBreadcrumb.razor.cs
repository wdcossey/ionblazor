namespace IonBlazor.Components;

public sealed partial class IonBreadcrumb : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private DotNetObjectReference<IonicEventCallback> _ionFocusReference;

    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// If <b>true</b>, the breadcrumb will take on a different look to show that it is the currently active breadcrumb.
    /// Defaults to <b>true</b> for the last breadcrumb if it is not set on any.
    /// </summary>
    [Parameter] public bool? Active { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the breadcrumb.
    /// </summary>
    [Parameter] public bool? Disabled { get; init; }

    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it, so the user will be prompted
    /// to save it as a local file. If the attribute has a value, it is used as the pre-filled file name in the
    /// Save prompt (the user can still change the file name if they want).
    /// </summary>
    [Parameter] public string? Download { get; init; }

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set, an anchor tag will
    /// be rendered.
    /// </summary>
    [Parameter] public string? Href { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object. The value is a space-separated list of
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types">link types</a>.
    /// </summary>
    [Parameter] public string? Rel { get; init; }

    /// <summary>
    /// If <b>true</b>, show a separator between this breadcrumb and the next. Defaults to <b>true</b> for all
    /// breadcrumbs except the last.
    /// </summary>
    [Parameter] public bool? Separator { get; init; }

    /// <summary>
    /// Specifies where to display the linked URL. <br/>
    /// Only applies when an href is provided. <br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".
    /// </summary>
    [Parameter] public string? Target { get; init; }

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
        _ionBlurReference = IonicEventCallback.Create(async () => await OnBlur.InvokeAsync());
        _ionFocusReference = IonicEventCallback.Create(async () => await OnFocus.InvokeAsync());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            _self,
            IonEvent.Set("ionBlur", _ionBlurReference),
            IonEvent.Set("ionFocus", _ionFocusReference));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionFocusReference.Dispose();
        await base.DisposeAsync();
    }
}