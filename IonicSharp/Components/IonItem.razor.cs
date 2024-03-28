namespace IonicSharp.Components;

public partial class IonItem : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, a button tag will be rendered and the item will be tappable.
    /// </summary>
    [Parameter]
    public bool? Button { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, a detail arrow will appear on the item.
    /// Defaults to <b>false</b> unless the mode is <b>ios</b> and an <b>href</b> or <b>button</b> property is present.
    /// </summary>
    [Parameter]
    public bool? Detail { get; set; }

    /// <summary>
    /// The icon to use when <b>detail</b> is set to <b>true</b>.
    /// </summary>
    [Parameter]
    public string? DetailIcon { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the item.
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
    /// The fill for the item. If <see cref="IonItemFill.Solid"/> the item will have a background.
    /// If<see cref="IonItemFill.Outline"/> the item will be
    /// transparent with a border. Only available in <see cref="IonMode.MaterialDesign"/> mode.
    /// </summary>
    [Parameter]
    public string? Fill { get; set; } = IonItemFill.Default;

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set, an anchor tag will be
    /// rendered.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// How the bottom border should be displayed on the item.
    /// </summary>
    [Parameter]
    public string? Lines { get; set; } = IonItemLines.Default;

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
    /// The shape of the item. If <see cref="IonItemShape.Round"/> it will have increased border radius.
    /// </summary>
    [Parameter]
    public string? Shape { get; set; } = IonItemShape.Default;

    /// <summary>
    /// Specifies where to display the linked URL. Only applies when an href is provided.
    /// Special keywords: <b>"_blank"</b>, <b>"_self"</b>, <b>"_parent"</b>, <b>"_top"</b>.
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// The shape of the item. If <see cref="IonItemShape.Round"/> it will have increased border radius.
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonItemType.Default;
}

public static class IonItemFill
{
    public const string? Default = null;
    public const string Outline = "outline";
    public const string Solid = "solid";
}

public static class IonItemLines
{
    public const string? Default = null;
    public const string Full = "full";
    public const string Inset = "inset";
    public const string None = "none";
}

public static class IonItemShape
{
    public const string? Default = null;
    public const string Round = "round";
}

public static class IonItemType
{
    public const string? Default = null;
    public const string Button = "button";
    public const string Reset = "reset";
    public const string Submit = "submit";
}