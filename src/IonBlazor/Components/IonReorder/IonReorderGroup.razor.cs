namespace IonBlazor.Components;

public sealed partial class IonReorderGroup : IonContentComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionItemReorderReference;

    protected override string JsImportName => nameof(IonReorderGroup);

    /// <summary>
    /// If true, the reorder will be hidden.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; } = true;

    /// <summary>
    /// Event that needs to be listened to in order to complete the reorder action.
    /// Once the event has been emitted, the complete() method then needs to be called in order to
    /// finalize the reorder action.
    /// </summary>
    [Parameter]
    public EventCallback<IonReorderGroupIonItemReorderEventArgs> IonItemReorder { get; set; }

    public IonReorderGroup()
    {
        _ionItemReorderReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var from = args?["detail"]?["from"]?.GetValue<int>();
            var to = args?["detail"]?["to"]?.GetValue<int>();
            await IonItemReorder.InvokeAsync(new IonReorderGroupIonItemReorderEventArgs()
                { From = from, To = to, Sender = this });
        });
    }

    /// <summary>
    /// Completes the reorder operation. Must be called by the ionItemReorder event.
    /// <br/><br/>
    /// If <b>true</b>, the reorder will complete and the item will remain in the position it was dragged to.<br/>
    /// If <b>false</b> is passed, the reorder will complete and the item will bounce back to its original position.<br/>
    /// </summary>
    public async Task CompleteAsync(bool reorder = true)
    {
        await JsComponent.InvokeVoidAsync("complete", IonElement, reorder);
    }

    /// <summary>
    /// If a list of items is passed, the list will be reordered and returned in the proper order.
    /// <br/><br/>
    /// If null, the reorder will complete and the item will remain in the position it was dragged to.
    /// </summary>
    public async Task<IEnumerable<T>?> CompleteAsync<T>(IEnumerable<T>? list)
    {
        JsonElement items = await JsComponent.InvokeAsync<JsonElement>("complete", IonElement, list);
        if (items.ValueKind != JsonValueKind.Array || items.GetArrayLength() == 0)
        {
            return null;
        }

        return items.Deserialize<IEnumerable<T>>();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement, IonEvent.Set("ionItemReorder", _ionItemReorderReference ));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionItemReorderReference.Dispose();
        await base.DisposeAsync();
    }
}