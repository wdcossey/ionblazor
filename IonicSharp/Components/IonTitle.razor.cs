namespace IonicSharp.Components;

public partial class IonTitle: IonComponent, IIonContentComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <inheritdoc/>
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