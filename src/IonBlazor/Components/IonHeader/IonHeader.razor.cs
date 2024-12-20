namespace IonBlazor.Components;

public sealed partial class IonHeader : IonContentComponent, IIonModeComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// Describes the scroll effect that will be applied to the footer. Only applies in iOS mode.
    /// </summary>
    [Parameter]
    public string? Collapse { get; set; } = IonHeaderCollapse.Default;

    /// <summary>
    /// The mode from the parent (<see cref="IonApp"/>).
    /// </summary>
    [CascadingParameter(Name = "ion-app-mode")]
    internal string? CascadingMode { get; set; }

    /// <inheritdoc/>
    //[Parameter]
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If true, the header will be translucent. Only applies when the mode is "ios" and the device supports backdrop-filter.
    /// <br/><br/>
    /// Note: In order to scroll content behind the header, the fullscreen attribute needs to be set on the content.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }
}