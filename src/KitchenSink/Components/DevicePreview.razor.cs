namespace IonicTest.Components;

public partial class DevicePreview : IIonContentComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// The mode from the parent (<see cref="IonApp"/>).
    /// </summary>
    [CascadingParameter(Name = "ion-app-mode")]
    internal string? CascadingMode { get; set; }

    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;


}