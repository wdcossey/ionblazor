﻿namespace IonicSharp.Components;

public partial class IonLabel : IonControl
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
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// The position determines where and how the label behaves inside an item.
    /// </summary>
    [Parameter] public string? Position { get; set; }
}

public static class IonLabelPosition
{
    public const string? Default = null;
    public const string Fixed = "fixed";
    public const string Floating = "floating";
    public const string Stacked = "stacked";
}