namespace IonicTest.Components;

public partial class IonLabel : IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonicColor.Primary"/>, <see cref="IonicColor.Secondary"/>,
    /// <see cref="IonicColor.Tertiary"/>, <see cref="IonicColor.Success"/>,
    /// <see cref="IonicColor.Warning"/>, <see cref="IonicColor.Danger"/>,
    /// <see cref="IonicColor.Light"/>, <see cref="IonicColor.Medium"/>,
    /// and <see cref="IonicColor.Dark"/>. <p/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public IonicStyleMode? Mode { get; set; }
    
    /// <summary>
    /// The position determines where and how the label behaves inside an item.
    /// </summary>
    [Parameter] public string? Position { get; set; }
}

public static class IonPosition
{
    public const string Fixed = "fixed";
    public const string Floating = "floating";
    public const string Stacked = "stacked";
}