namespace IonicSharp.Components;

public partial class IonHeader : IonComponent, IIonModeComponent, IIonContentComponent
{
    //private string? _cascadingMode;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Describes the scroll effect that will be applied to the footer. Only applies in iOS mode.
    /// </summary>
    [Parameter]
    public string? Collapse { get; set; } = IonHeaderCollapse.Default;

    /// <inheritdoc/>
    //[Parameter]
    [CascadingParameter(Name = nameof(Mode))]
    public string? Mode { get; set; } = IonMode.Default;

    /*[CascadingParameter(Name = "ion-mode")]
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