using IonBlazor.Abstractions;

namespace IonicTest.Components;

public partial class DevicePreview : IonContentComponent
{
    /// <inheritdoc/>
    [Parameter, EditorRequired] public override RenderFragment? ChildContent { get; init; }

    /// <summary>
    /// The mode from the parent (<see cref="IonApp"/>).
    /// </summary>
    [CascadingParameter(Name = "ion-app-mode")]
    internal string? CascadingMode { get; set; }

    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;


}