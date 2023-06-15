namespace IonicSharp.Components;

public partial class IonTitle: IonComponent
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
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
    /// The size of the toolbar title.
    /// </summary>
    [Parameter] public string? Size { get; set; } = IonTitleSize.Default;

}

public static class IonTitleSize
{
    public const string? Default = null;
    public const string Large = "large";
    public const string Small = "small";
}