namespace IonicSharp.Components;

public partial class IonSpinner: IonComponent
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
    /// Duration of the spinner animation in milliseconds.
    /// The default varies based on the spinner.
    /// </summary>
    [Parameter] public int? Duration { get; set; }

    /// <summary>
    /// The name of the SVG spinner to use.
    /// If a name is not provided, the platform's default spinner will be used.
    /// </summary>
    [Parameter]
    public string? Name { get; set; } = IonSpinnerName.Default;
    
    /// <summary>
    /// If <b>true</b>, the spinner's animation will be paused.
    /// </summary>
    [Parameter] public bool Paused { get; set; }

}

public static class IonSpinnerName
{
    /// <summary>
    /// Platform's default spinner
    /// </summary>
    public const string? Default = null;
    public const string Bubbles = "bubbles";
    public const string Circles = "circles";
    public const string Circular = "circular";
    public const string Crescent = "crescent";
    public const string Dots = "dots";
    public const string Lines = "lines";
    public const string LinesSharp = "lines-sharp";
    public const string LinesSharpSmall = "lines-sharp-small";
    public const string LinesSmall = "lines-small";
}