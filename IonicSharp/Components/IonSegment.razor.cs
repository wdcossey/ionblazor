namespace IonicSharp.Components;

public partial class IonSegment : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionChangeReference;

    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the segment.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If <b>true</b>, the segment buttons will overflow and the user can swipe to see them.
    /// In addition, this will disable the gesture to drag the indicator between the buttons in
    /// order to swipe to see hidden buttons.
    /// </summary>
    [Parameter]
    public bool Scrollable { get; set; }

    /// <summary>
    /// If <b>true</b>, navigating to an ion-segment-button with the keyboard will focus and select the element.
    /// If <b>false</b>, keyboard navigation will only focus the ion-segment-button element.
    /// </summary>
    [Parameter]
    public bool SelectOnFocus { get; set; }

    /// <summary>
    /// If <b>true</b>, users will be able to swipe between segment buttons to activate them.
    /// </summary>
    [Parameter]
    public bool SwipeGesture { get; set; }

    /// <summary>
    /// the value of the segment.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }
    
    /// <summary>
    /// Emitted when the value property has changed and any dragging pointer has been released from ion-segment.
    /// </summary>
    [Parameter] public EventCallback<IonSegmentIonChangeEventArgs> IonChange { get; set; }
    
    public IonSegment()
    {
        _ionChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            Value = value;
            
            await IonChange.InvokeAsync(new IonSegmentIonChangeEventArgs() { Sender  = this, Value = value });
        }));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new[]
        {
            new { Event = "ionChange", Ref = _ionChangeReference }
        }, _self);
    }
}

public class IonSegmentIonChangeEventArgs : EventArgs
{
    public IonSegment Sender { get; internal set; } = null!;
    
    public string? Value { get; internal set; }
    
    //public bool? IsTrusted { get; internal set; }
}