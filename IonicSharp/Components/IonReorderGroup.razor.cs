namespace IonicSharp.Components;

public partial class IonReorderGroup : IonComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionItemReorderReference;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If true, the reorder will be hidden.
    /// </summary>
    [Parameter] public bool Disabled { get; set; } = true;

    /// <summary>
    /// Event that needs to be listened to in order to complete the reorder action.
    /// Once the event has been emitted, the complete() method then needs to be called in order to
    /// finalize the reorder action.
    /// </summary>
    [Parameter] public EventCallback<IonReorderGroupIonItemReorderEventArgs> IonItemReorder { get; set; }
    
    public IonReorderGroup()
    {
        _ionItemReorderReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            var from = args?["detail"]?["from"]?.GetValue<int>();
            var to = args?["detail"]?["to"]?.GetValue<int>();
            await IonItemReorder.InvokeAsync(new IonReorderGroupIonItemReorderEventArgs() { From  = from, To = to, Sender = this });
        }));
    }
    
    //ionItemReorder
    /// <summary>
    /// Completes the reorder operation. Must be called by the ionItemReorder event.
    /// <br/><br/>
    /// If <b>true</b>, the reorder will complete and the item will remain in the position it was dragged to.<br/>
    /// If <b>false</b> is passed, the reorder will complete and the item will bounce back to its original position.<br/>
    /// </summary>
    public async Task CompleteAsync(bool reorder)
    {
        await JsRuntime.InvokeVoidAsync("completeIonReorderGroup", _self, reorder);
    }
    
    /// <summary>
    /// If a list of items is passed, the list will be reordered and returned in the proper order.
    /// <br/><br/>
    /// If null, the reorder will complete and the item will remain in the position it was dragged to.
    /// </summary>
    public async Task CompleteAsync(object[]? list)
    {
        await JsRuntime.InvokeVoidAsync("completeIonReorderGroup", _self, list);
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new []
        {
            new { Event = "ionItemReorder", Ref = _ionItemReorderReference}
        }, _self);
    }
}

public class IonReorderGroupIonItemReorderEventArgs : EventArgs
{
    public IonReorderGroup? Sender { get; internal set; }
    public int? From { get; internal set; }
    public int? To { get; internal set; }
}