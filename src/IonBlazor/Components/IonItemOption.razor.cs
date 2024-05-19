namespace IonBlazor.Components;

public partial class IonItemOption : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the item option.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it, so the user will be prompted
    /// to save it as a local file. If the attribute has a value, it is used as the pre-filled file name in the
    /// Save prompt (the user can still change the file name if they want).
    /// </summary>
    [Parameter]
    public string? Download { get; set; }

    /// <summary>
    /// If true, the option will expand to take up the available width and cover any other options.
    /// </summary>
    [Parameter]
    public bool? Expandable { get; set; }

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set,
    /// an anchor tag will be rendered.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object. The value is a space-separated list
    /// of link types.<br/>
    /// https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types
    /// </summary>
    [Parameter]
    public string? Rel { get; set; }

    /// <summary>
    /// Specifies where to display the linked URL. Only applies when an href is provided.
    /// Special keywords: <b>"_blank"</b>, <b>"_self"</b>, <b>"_parent"</b>, <b>"_top"</b>.
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonItemOptionType.Default;
}

public static class IonItemOptionType
{
    public const string? Default = null;
    public const string Button = "button";
    public const string Reset = "reset";
    public const string Submit = "submit";
}