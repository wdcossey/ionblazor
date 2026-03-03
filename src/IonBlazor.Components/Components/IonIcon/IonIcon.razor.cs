namespace IonBlazor.Components;

public sealed partial class IonIcon : IonComponent, IIonColorComponent
{
    [Parameter]
    public string? Name { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; } = IonColor.Undefined;

    [Parameter]
    public string? Size { get; set; } = IonIconSize.Undefined;
}