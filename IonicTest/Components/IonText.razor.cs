﻿namespace IonicTest.Components;

public partial class IonText : IonControl
{
    private ElementReference _self;
    
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
}