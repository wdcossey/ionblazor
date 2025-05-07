namespace IonBlazor.Components;

public sealed partial class IonBackButton : IonComponent, IIonModeComponent, IIonColorComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Color { get; init; }

    /// <summary>
    /// The url to navigate back to by default when there is no history.
    /// </summary>
    [Parameter] public string? DefaultHref { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the button.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// The built-in named SVG icon name or the exact src of an SVG file to use for the back button.
    /// </summary>
    [Parameter] public string? Icon { get; set; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;

    //routerAnimation

    /// <summary>
    /// The text to display in the back button.
    /// </summary>
    [Parameter] public string? Text { get; set; }

    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter] public string Type { get; set; } = IonBackButtonType.Button;
}