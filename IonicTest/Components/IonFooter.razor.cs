namespace IonicTest.Components;

public partial class IonFooter: IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Describes the scroll effect that will be applied to the footer. Only applies in iOS mode.
    /// </summary>
    [Parameter] public string? Collapse { get; set; } = IonFooterCollapse.Default;
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// If true, the footer will be translucent. Only applies when the mode is "ios" and the device supports backdrop-filter.
    /// <br/><br/>
    /// Note: In order to scroll content behind the footer, the fullscreen attribute needs to be set on the content.
    /// </summary>
    [Parameter] public bool Translucent { get; set; }
    
}

public static class IonFooterCollapse
{
    public const string? Default = null;
    public const string Fade = "fade";
}