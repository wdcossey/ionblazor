namespace IonicSharp.Components;

public partial class IonListHeader
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
    /// How the bottom border should be displayed on the list header.
    /// </summary> 
    [Parameter] public string? Lines { get; set; } = IonListHeaderLines.Default;
        
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
}

public static class IonListHeaderLines
{
    public const string? Default = null;
    public const string Full = "full";
    public const string Inset = "inset";
    public const string None = "none";
}