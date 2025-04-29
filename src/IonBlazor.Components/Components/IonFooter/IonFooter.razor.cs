namespace IonBlazor.Components;

public sealed partial class IonFooter : IonContentComponent, IIonModeComponent
{
    /// <summary>
    /// Describes the scroll effect that will be applied to the footer. Only applies in iOS mode.
    /// </summary>
    [Parameter]
    public string? Collapse { get; set; } = IonFooterCollapse.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If true, the footer will be translucent. Only applies when the mode is "ios" and the device supports backdrop-filter.
    /// <br/><br/>
    /// Note: In order to scroll content behind the footer, the fullscreen attribute needs to be set on the content.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }
}