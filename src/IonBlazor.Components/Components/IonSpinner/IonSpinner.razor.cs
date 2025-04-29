namespace IonBlazor.Components;

public sealed partial class IonSpinner : IonComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// Duration of the spinner animation in milliseconds.
    /// The default varies based on the spinner.
    /// </summary>
    [Parameter]
    public int? Duration { get; set; }

    /// <summary>
    /// The name of the SVG spinner to use.
    /// If a name is not provided, the platform's default spinner will be used.
    /// </summary>
    [Parameter]
    public string? Name { get; set; } = IonSpinnerName.Default;

    /// <summary>
    /// If <b>true</b>, the spinner's animation will be paused.
    /// </summary>
    [Parameter]
    public bool Paused { get; set; }
}