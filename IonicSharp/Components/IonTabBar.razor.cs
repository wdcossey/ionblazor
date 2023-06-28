namespace IonicSharp.Components;

public partial class IonTabBar : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The selected tab component
    /// </summary>
    [Parameter]
    public string? SelectedTab { get; set; }

    /// <summary>
    /// If <b>true</b>, the tab bar will be translucent.
    /// Only applies when the mode is <see cref="IonMode.iOS"/> and the device supports <i>backdrop-filter</i>.
    /// </summary>
    [Parameter]
    public bool Translucent { get; set; }
}