namespace IonBlazor.Components;

public sealed partial class IonInputPasswordToggle : IonComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    /// <inheritdoc />
    public override ElementReference IonElement => _self;

    /// <inheritdoc />
    [Parameter] public string? Mode { get; set; }

    /// <inheritdoc />
    [Parameter] public string? Color { get; set; }

    /// <summary>
    /// The icon that can be used to represent hiding a password. If not set, the "eyeOff" Ionicon will be used.
    /// </summary>
    [Parameter] public string? HideIcon { get; set; }

    /// <summary>
    /// The icon that can be used to represent showing a password. If not set, the "eye" Ionicon will be used.
    /// </summary>
    [Parameter] public string? ShowIcon { get; set; }
}