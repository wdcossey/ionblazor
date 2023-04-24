using Microsoft.AspNetCore.Components;

namespace IonicTest.Components;

public partial class IonChip : IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonicColor.Primary"/>, <see cref="IonicColor.Secondary"/>,
    /// <see cref="IonicColor.Tertiary"/>, <see cref="IonicColor.Success"/>,
    /// <see cref="IonicColor.Warning"/>, <see cref="IonicColor.Danger"/>,
    /// <see cref="IonicColor.Light"/>, <see cref="IonicColor.Medium"/>,
    /// and <see cref="IonicColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If true, the user cannot interact with the <see cref="IonChip"/>.
    /// </summary>
    [Parameter] public bool? Disabled { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// Display an outline style <see cref="IonChip"/>.
    /// </summary>
    [Parameter] public bool? Outline { get; set; }
}

public static class IonicColor 
{
    public const string Danger = "danger";
    public const string Dark = "dark";
    public const string Light = "light";
    public const string Medium = "medium";
    public const string Primary = "primary";
    public const string Secondary = "secondary";
    public const string Success = "success";
    public const string Tertiary = "tertiary";
    public const string Warning = "warning";
}
