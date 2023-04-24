namespace IonicTest.Components;

public partial class IonFabButton : IonControl
{
    private ElementReference _self;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the <see cref="IonFabButton"/> will be show a close icon.<br/>
    /// Default: <b>false</b>
    /// </summary>
    [Parameter] public bool Activated { get; set; }
    
    /// <summary>
    /// The icon name to use for the close icon.
    /// This will appear when the fab button is pressed.
    /// Only applies if it is the main button inside of a fab containing a fab list.<br/>
    /// Default: <b>close</b>
    /// </summary>
    [Parameter] public string? CloseIcon { get; set; }
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonicColor.Primary"/>, <see cref="IonicColor.Secondary"/>,
    /// <see cref="IonicColor.Tertiary"/>, <see cref="IonicColor.Success"/>,
    /// <see cref="IonicColor.Warning"/>, <see cref="IonicColor.Danger"/>,
    /// <see cref="IonicColor.Light"/>, <see cref="IonicColor.Medium"/>,
    /// and <see cref="IonicColor.Dark"/>. <br/>
    /// For more information on colors, see theming.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If true, the user cannot interact with the fab button.<br/>
    /// Default: <b>false</b>
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// This attribute instructs browsers to download a URL instead of navigating to it,
    /// so the user will be prompted to save it as a local file.
    /// If the attribute has a value, it is used as the pre-filled file name in the
    /// Save prompt (the user can still change the file name if they want).<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Download { get; set; }
    
    /// <summary>
    /// Contains a URL or a URL fragment that the hyperlink points to.
    /// If this property is set, an anchor tag will be rendered.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Href { get; set; }

    /// <summary>
    /// The mode determines which platform styles to use.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public IonicStyleMode? Mode { get; set; }

    /// <summary>
    /// Specifies the relationship of the target object to the link object.
    /// The value is a space-separated list of link types.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Rel { get; set; }
    
    ///// <summary>
    ///// When using a router, it specifies the transition animation when navigating to another page using href.<br/>
    ///// Default: <b>null</b>
    ///// </summary>
    //[Parameter] public string? RouterAnimation { get; set; }
    
    /// <summary>
    /// When using a router, it specifies the transition direction when navigating to another page using href.<br/>
    /// Default: <see cref="IonRouterDirection.Forward"/>
    /// </summary>
    [Parameter] public string? RouterDirection { get; set; } = IonRouterDirection.Forward;
    
    /// <summary>
    /// If true, the fab button will show when in a fab-list.<br/>
    /// Default: <b>false</b>
    /// </summary>
    [Parameter] public bool Show { get; set; }
    
    /// <summary>
    /// The size of the button. Set this to small in order to have a mini fab button.<br/>
    /// Default: <see cref="IonFabButtonSize.Default"/>
    /// </summary>
    [Parameter] public string? Size { get; set; } = IonFabButtonSize.Default;
    
    /// <summary>
    /// Specifies where to display the linked URL. Only applies when an href is provided.<br/>
    /// Special keywords: "_blank", "_self", "_parent", "_top".<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Target { get; set; }
    
    /// <summary>
    /// If true, the fab button will be translucent.
    /// Only applies when the mode is "ios" and the device supports <b>backdrop-filter</b>.<br/>
    /// Default: <b>null</b>
    /// </summary>
    [Parameter] public string? Translucent { get; set; }
    
    /// <summary>
    /// The type of the button.<br/>
    /// Default: <see cref="IonButtonType.Button"/>
    /// </summary>
    [Parameter] public string? Type { get; set; } = IonButtonType.Button;
    
    //Events
    //ionBlur
    //ionFocus
}

public static class IonRouterDirection 
{
    public const string Back = "back";
    public const string Forward = "forward";
    public const string Root = "root";
}

public static class IonFabButtonSize 
{
    public const string? Default = null;
    public const string Small = "small";
}