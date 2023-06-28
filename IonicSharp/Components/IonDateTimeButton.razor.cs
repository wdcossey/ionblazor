namespace IonicSharp.Components;

public partial class IonDateTimeButton: IonComponent
{
    private ElementReference _self;
    
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
    /// The ID of the ion-datetime instance associated with the datetime button.
    /// </summary>
    [Parameter, EditorRequired] public string? DateTime { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the button.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
}