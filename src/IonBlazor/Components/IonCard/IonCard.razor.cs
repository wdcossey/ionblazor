﻿namespace IonBlazor.Components;

public sealed partial class IonCard : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    /// <summary>
    /// If <b>true</b>, a button tag will be rendered and the card will be tappable.
    /// </summary>
    [Parameter]
    public bool? Button { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If true, the user cannot interact with the <see cref="IonCard"/>.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; init; }

    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file. <br/>
    /// If the attribute has a value, it is used as the pre-filled file name in the Save prompt
    /// (the user can still change the file name if they want).
    /// </summary>
    [Parameter]
    public string? Download { get; init; }

    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to. If this property is set, an anchor tag will be rendered.
    /// </summary>
    [Parameter]
    public string? Href { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Specifies the relationship of the target object to the link object. The value is a space-separated list of
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Link_types">link types</a>.
    /// </summary>
    [Parameter]
    public string? Rel { get; init; }

    //[Parameter] public string? RouterAnimation { get; set; }
    //[Parameter] public string? RouterDirection { get; set; }

    /// <summary>
    /// Specifies where to display the linked URL. <br/>
    /// Only applies when an href is provided. <br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".
    /// </summary>
    [Parameter]
    public string? Target { get; init; }

    /// <summary>
    /// The type of the button. Only used when an <b>@onclick</b> or <see cref="IonButton"/> property is present.
    /// </summary>
    [Parameter]
    public string? Type { get; init; } = IonCardButtonType.Default;
}