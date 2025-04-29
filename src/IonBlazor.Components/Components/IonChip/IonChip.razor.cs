namespace IonBlazor.Components;

public sealed partial class IonChip : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If true, the user cannot interact with the <see cref="IonChip"/>.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Display an outline style <see cref="IonChip"/>.
    /// </summary>
    [Parameter]
    public bool? Outline { get; set; }
}