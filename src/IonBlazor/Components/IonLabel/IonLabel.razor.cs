namespace IonBlazor.Components;

public sealed partial class IonLabel : IonContentComponent, IIonModeComponent, IIonColorComponent
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
    /// The position determines where and how the label behaves inside an item.
    /// </summary>
    [Parameter]
    public string? Position { get; set; } = IonLabelPosition.Default;
}