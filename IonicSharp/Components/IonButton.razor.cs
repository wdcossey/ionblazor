using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonButton : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private DotNetObjectReference<IonicEventCallback> _ionFocusReference;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The type of button.
    /// </summary>
    [Parameter]
    public string? ButtonType { get; set; } = "button";

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If true, the user cannot interact with the <see cref="IonButton"/>.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file. <br/>
    /// If the attribute has a value, it is used as the pre-filled file name in the Save prompt
    /// (the user can still change the file name if they want).
    /// </summary>
    [Parameter]
    public string? Download { get; set; }

    /// <summary>
    /// Set to <see cref="IonButtonExpand.Block"/> for a full-width button
    /// or to <see cref="IonButtonExpand.Full"/> for a full-width button with square corners and no left or right borders.
    /// </summary>
    [Parameter]
    public string? Expand { get; set; }

    /// <summary>
    /// Set to <see cref="IonButtonFill.Clear"/> for a transparent button that resembles a flat button,
    /// to <see cref="IonButtonFill.Outline"/> for a transparent button with a border,
    /// or to <see cref="IonButtonFill.Solid"/> for a button with a filled background.
    /// The default fill is <see cref="IonButtonFill.Solid"/> except inside of a toolbar,
    /// where the default is <see cref="IonButtonFill.Clear"/>.
    /// </summary>
    [Parameter]
    public string? Fill { get; set; }

    /// <summary>
    /// The HTML form element or form element id. Used to submit a form when the button is not a child of the form.
    /// </summary>
    [Parameter]
    public string? Form { get; set; }

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set, an anchor tag will be rendered.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object. The value is a space-separated list of
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types">link types</a>.
    /// </summary>
    [Parameter]
    public string? Rel { get; set; }

    //[Parameter] public string? RouterAnimation { get; set; }
    //[Parameter] public string? RouterDirection { get; set; }

    /// <summary>
    /// Set to <see cref="IonButtonShape.Round"/> for a button with more rounded corners.
    /// </summary>
    [Parameter]
    public string? Shape { get; set; } = IonButtonShape.Default;

    /// <summary>
    /// Set to <see cref="IonButtonSize.Small"/> for a button with less height and padding,
    /// to <see cref="IonButtonSize.Default"/> for a button with the default height and padding,
    /// or to <see cref="IonButtonSize.Large"/> for a button with more height and padding. <br/>
    /// By default the size is unset, unless the button is inside of an item,
    /// where the size is <see cref="IonButtonSize.Small"/> by default. <br/>
    /// Set the size to <see cref="IonButtonSize.Default"/> inside of an item to make it a standard size button.
    /// </summary>
    [Parameter]
    public string? Size { get; set; }

    /// <summary>
    /// If true, activates a button with a heavier font weight.
    /// </summary>
    [Parameter]
    public bool? Strong { get; set; }

    /// <summary>
    /// Specifies where to display the linked URL. <br/>
    /// Only applies when an href is provided. <br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter]
    public string Type { get; set; } = IonButtonType.Button;

    /// <summary>
    /// Emitted when the button loses focus.
    /// </summary>
    [Parameter]
    public EventCallback OnBlur { get; set; }

    /// <summary>
    /// Emitted when the button has focus.
    /// </summary>
    [Parameter]
    public EventCallback OnFocus { get; set; }

    public IonButton()
    {
        _ionBlurReference = IonicEventCallback.Create(async () =>
        {
            await OnBlur.InvokeAsync();
        });

        _ionFocusReference = IonicEventCallback.Create(async () =>
        {
            await OnFocus.InvokeAsync();
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await this.AttachIonListenersAsync(_self, new IonEvent[]
        {
            IonEvent.Set("ionBlur" , _ionBlurReference ),
            IonEvent.Set("ionFocus", _ionFocusReference)
        });

    }
}

public static class IonButtonExpand {
    public const string? Default = null;
    public const string Block = "block";
    public const string Full = "full";
}

public static class IonButtonFill 
{
    public const string Clear = "clear";
    public const string Default = "default";
    public const string Outline = "outline";
    public const string Solid = "solid";
}

public static class IonButtonShape 
{
    public const string? Default = null;
    public const string Round = "round";
}

public static class IonButtonSize 
{
    public const string Default = "default";
    public const string Large = "large";
    public const string Small = "small";
}

public static class IonButtonType 
{
    public const string Button = "button";
    public const string Reset = "reset";
    public const string Submit = "submit";
}