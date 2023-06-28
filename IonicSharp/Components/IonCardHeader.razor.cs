namespace IonicSharp.Components;

public partial class IonCardHeader : IonComponent
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// If <b>true</b>, the card header will be translucent.
    /// Only applies when the <see cref="Mode"/> is <see cref="IonMode.iOS"/> and the device supports
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/backdrop-filter#Browser_compatibility">backdrop-filter</a>.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }
}