namespace IonicTest.Components;

public partial class DevicePreview : IIonContentComponent
{
    private ElementReference _self;
    
    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }
    
    [Parameter, EditorRequired]
    public string? Mode { get; set; } = IonMode.Default;

    
}