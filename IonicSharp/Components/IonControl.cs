namespace IonicSharp.Components;

public abstract class IonControl : ComponentBase
{
    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    [Parameter]
    public virtual string? Class { get; set; }
}

public abstract class IonSlotControl : IonControl
{
    [Parameter]
    public string? Slot { get; set; }
}