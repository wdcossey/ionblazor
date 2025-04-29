namespace IonBlazor.Components;

public sealed partial class IonTabButton : IonContentComponent, IIonModeComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the tab button.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file.
    /// If the attribute has a value, it is used as the pre-filled file name in the Save prompt
    /// (the user can still change the file name if they want).
    /// </summary>
    [Parameter]
    public string? Download { get; set; }

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to.
    /// If this property is set, an anchor tag will be rendered.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Set the layout of the text and icon in the tab bar.
    /// It defaults to <see cref="IonTabButtonLayout.IconTop"/>.
    /// </summary>
    [Parameter]
    public string? Layout { get; set; } = IonTabButtonLayout.IconTop;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object.
    /// The value is a space-separated list of link types.
    /// </summary>
    [Parameter]
    public string? Rel { get; set; }

    /// <summary>
    /// The selected tab component
    /// </summary>
    [Parameter]
    public bool? Selected { get; set; }

    /// <summary>
    /// A tab id must be provided for each <see cref="IonTab"/>.
    /// It's used internally to reference the selected tab or by the router to switch between them.
    /// </summary>
    [Parameter]
    public string? Tab { get; set; }

    /// <summary>
    /// Specifies where to display the linked URL. Only applies when an href is provided.
    /// Special keywords: <b>_blank</b>, <b>_self</b>, <b>_parent</b>, <b>_top</b>.
    /// </summary>
    [Parameter]
    public string? Target { get; set; }
}