namespace IonicTest.Components;

public partial class IonTabBar: IonSlotControl
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
    /// The selected tab component
    /// </summary>
    [Parameter] public string? SelectedTab { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the tab bar will be translucent.
    /// Only applies when the mode is <see cref="IonMode.iOS"/> and the device supports <i>backdrop-filter</i>.
    /// </summary>
    [Parameter] public bool Translucent { get; set; }
}