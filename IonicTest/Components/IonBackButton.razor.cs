namespace IonicTest.Components;

public partial class IonBackButton : IonControl
{
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <p/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// The url to navigate back to by default when there is no history.
    /// </summary>
    [Parameter] public string? DefaultHref { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the button.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// The built-in named SVG icon name or the exact src of an SVG file to use for the back button.
    /// </summary>
    [Parameter] public string? Icon { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
    
    //routerAnimation
    
    /// <summary>
    /// The text to display in the back button.
    /// </summary>
    [Parameter] public string? Text { get; set; }
    
    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter] public string Type { get; set; } = IonBackButtonType.Button;
    
}

public static class IonBackButtonType
{
    public const string Button = "button";
    public const string Reset = "reset";
    public const string Submit = "submit";
}