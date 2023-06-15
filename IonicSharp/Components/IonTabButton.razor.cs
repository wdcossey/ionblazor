﻿namespace IonicSharp.Components;

public partial class IonTabButton: IonComponent
{
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If true, the user cannot interact with the tab button.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file.
    /// If the attribute has a value, it is used as the pre-filled file name in the Save prompt
    /// (the user can still change the file name if they want).
    /// </summary>
    [Parameter] public string? Download { get; set; }
    
    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to.
    /// If this property is set, an anchor tag will be rendered.
    /// </summary>
    [Parameter] public string? Href { get; set; }
    
    /// <summary>
    /// Set the layout of the text and icon in the tab bar.
    /// It defaults to <see cref="IonTabButtonLayout.IconTop"/>.
    /// </summary>
    [Parameter] public string? Layout { get; set; } = IonTabButtonLayout.IconTop;

    /// <summary>
    /// The mode determines which platform styles to use.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object.
    /// The value is a space-separated list of link types.
    /// </summary>
    [Parameter] public string? Rel { get; set; }
    
    /// <summary>
    /// The selected tab component
    /// </summary>
    [Parameter] public bool Selected { get; set; }
    
    /// <summary>
    /// A tab id must be provided for each ion-tab.
    /// It's used internally to reference the selected tab or by the router to switch between them.
    /// </summary>
    [Parameter] public string? Tab { get; set; }
    
    /// <summary>
    /// Specifies where to display the linked URL. Only applies when an href is provided.
    /// Special keywords: "_blank", "_self", "_parent", "_top".
    /// </summary>
    [Parameter] public string? Target { get; set; }
}

public static class IonTabButtonLayout
{
    public const string IconBottom = "icon-bottom";
    public const string IconEnd = "icon-end";
    public const string IconHide = "icon-hide";
    public const string IconStart = "icon-start";
    public const string IconTop = "icon-top";
    public const string LabelHide = "label-hide";
}
