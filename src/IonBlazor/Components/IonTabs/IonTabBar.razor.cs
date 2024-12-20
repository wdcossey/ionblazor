namespace IonBlazor.Components;

public sealed partial class IonTabBar : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

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
    /// Only applies when the mode is <see cref="IonMode.iOS"/> and the device supports
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/backdrop-filter#Browser_compatibility">backdrop-filter</a>.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }
}