namespace IonicSharp.Components;

public partial class IonCardSubtitle : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
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
}