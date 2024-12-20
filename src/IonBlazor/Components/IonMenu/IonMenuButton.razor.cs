namespace IonBlazor.Components;

public sealed partial class IonMenuButton : IonComponent, IIonColorComponent, IIonModeComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; }

    /// <summary>
    /// Automatically hides the menu button when the corresponding menu is not active
    /// </summary>
    [Parameter]
    public bool? AutoHide { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the menu button.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// Optional property that maps to a Menu's <see cref="IonMenu.MenuId"/> prop.
    /// Can also be <b>start</b> or <b>end</b> for the menu side.
    /// This is used to find the correct menu to toggle
    /// </summary>
    [Parameter]
    public string? Menu { get; set; }

    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonMenuButtonType.Default;
}