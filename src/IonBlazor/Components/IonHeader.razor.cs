namespace IonBlazor.Components;

public partial class IonHeader : IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    //private string? _cascadingMode;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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

    /*[CascadingParameter(Name = "ion-app-mode")]
    private string? CascadingMode
    {
        get => _cascadingMode;
        set
        {
            _cascadingMode = value;

            if (Mode != IonMode.Default)
                return;

            Mode = value;
        }
    }*/

    /// <summary>
    /// If true, the header will be translucent. Only applies when the mode is "ios" and the device supports backdrop-filter.
    /// <br/><br/>
    /// Note: In order to scroll content behind the header, the fullscreen attribute needs to be set on the content.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }
}

public static class IonHeaderCollapse
{
    public const string? Default = null;
    public const string Condense = "condense";
    public const string Fade = "fade";
}