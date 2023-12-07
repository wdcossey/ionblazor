using System.Text.Json.Serialization;
using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonActionSheet<TButtonData> : IonComponent, IIonModeComponent
    where TButtonData : class, IActionSheetButtonData
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback> _didPresentReference = null!;

    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionActionSheetDidDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback> _ionActionSheetDidPresentReference = null!;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionActionSheetWillDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback> _ionActionSheetWillPresentReference = null!;

    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback> _willPresentReference = null!;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _buttonHandlerReference = null!;
    
    //TODO: Remove `_id`
    private readonly Guid _id = Guid.NewGuid();

    [Parameter] 
    public bool? Animated { get; set; }

    [Parameter]
    public bool? BackdropDismiss { get; set; }

    [Parameter] 
    public Func<IEnumerable<ActionSheetButton<TButtonData>>>? Buttons { get; set; }

    [Parameter, Obsolete("Ignored, use `CssClass`", true)]
    public override string? Class { get; set; }

    [Parameter] 
    public string? CssClass { get; set; }

    //[Parameter] public string? EnterAnimation { get; set; }
    [Parameter] 
    public string? Header { get; set; }
    
    //[Parameter] 
    //public Func<string[]>? HtmlAttributes { get; set; }
    
    [Parameter] 
    public bool? IsOpen { get; set; }
    
    [Parameter] 
    public bool? KeyboardClose { get; set; }

    //[Parameter] public string? LeaveAnimation { get; set; }
    
    [Parameter] 
    public string? Mode { get; set; } = IonMode.Default;
    
    [Parameter] 
    public string? SubHeader { get; set; }
    
    [Parameter] 
    public bool? Translucent { get; set; }
    
    [Parameter] 
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the action sheet has dismissed. Shorthand for <see cref="IonActionSheetDidDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetDismissEventArgs<TButtonData>> DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the action sheet has presented. Shorthand for <see cref="IonActionSheetWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> DidPresent { get; set; }

    /// <summary>
    /// Emitted after the action sheet has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetDismissEventArgs<TButtonData>> IonActionSheetDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the action sheet has presented.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> IonActionSheetDidPresent { get; set; }

    /// <summary>
    /// Emitted before the action sheet has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetDismissEventArgs<TButtonData>> IonActionSheetWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the action sheet has presented.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> IonActionSheetWillPresent { get; set; }

    /// <summary>
    /// Emitted before the action sheet has dismissed. Shorthand for <see cref="IonActionSheetWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetDismissEventArgs<TButtonData>> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the action sheet has presented. Shorthand for <see cref="IonActionSheetWillPresent"/>.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> WillPresent { get; set; }

    [Parameter] 
    public EventCallback<ActionSheetButtonHandlerEventArgs<TButtonData>> ButtonHandler { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        var buttons = Buttons?.Invoke();

        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            await DidDismiss.InvokeAsync(new ActionSheetDismissEventArgs<TButtonData>()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        });

        _didPresentReference = IonicEventCallback.Create(async () =>
        {
            await DidPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        });

        _ionActionSheetDidDismissReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                await IonActionSheetDidDismiss.InvokeAsync(new ActionSheetDismissEventArgs<TButtonData>()
                {
                    Sender = this,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
                });
            });

        _ionActionSheetDidPresentReference = IonicEventCallback.Create(async () =>
        {
            await IonActionSheetDidPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        });

        _ionActionSheetWillDismissReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                await IonActionSheetWillDismiss.InvokeAsync(new ActionSheetDismissEventArgs<TButtonData>()
                {
                    Sender = this,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
                });
            });

        _ionActionSheetWillPresentReference = IonicEventCallback.Create(async () =>
        {
            await IonActionSheetWillPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        });

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            await WillDismiss.InvokeAsync(new ActionSheetDismissEventArgs<TButtonData>()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        });

        _willPresentReference = IonicEventCallback.Create(async () =>
        {
            await WillPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        });

        _buttonHandlerReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                var index = args?["index"]?.GetValue<int?>();
                var button = buttons?.ElementAtOrDefault(index ?? -1);
                await (button?.Handler?.Invoke(button, index) ?? ValueTask.CompletedTask);
                
                await ButtonHandler.InvokeAsync(new ActionSheetButtonHandlerEventArgs<TButtonData>()
                {
                    Sender = this,
                    Index = index,
                    Button = button
                });
            });
        
        await this.AttachIonListenersAsync(_self, new IonEvent[]
        {
            IonEvent.Set("didDismiss", _didDismissReference ),
            IonEvent.Set("didPresent", _didPresentReference ),

            IonEvent.Set("ionActionSheetDidDismiss", _ionActionSheetDidDismissReference ),
            IonEvent.Set("ionActionSheetDidPresent", _ionActionSheetDidPresentReference ),
            IonEvent.Set("ionActionSheetWillDismiss", _ionActionSheetWillDismissReference ),
            IonEvent.Set("ionActionSheetWillPresent", _ionActionSheetWillPresentReference ),

            IonEvent.Set("willDismiss", _willDismissReference ),
            IonEvent.Set("willPresent", _willPresentReference ),
        });
        
        await JsComponent.InvokeVoidAsync("addButtons", _self, buttons, _buttonHandlerReference);
    }

    /// <summary>
    /// Dismiss the action sheet overlay after it has been presented.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public ValueTask<bool> DismissAsync(IEnumerable<ActionSheetButton<TButtonData>>? data, string? role) => 
        JsComponent.InvokeAsync<bool>("dismiss", _self, data, role);

    /// <summary>
    /// Returns a promise that resolves when the action sheet did dismiss.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask OnDidDismissAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Returns a promise that resolves when the action sheet will dismiss.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask OnWillDismissAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Present the action sheet overlay after it has been created.
    /// </summary>
    public ValueTask PresentAsync() => JsComponent.InvokeVoidAsync("present", _self);
}

public interface IActionSheetButtonData
{
    public string? Action { get; set; }
}


public class ActionSheetButtonData : IActionSheetButtonData
{
    [JsonPropertyName("action"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Action { get; set; }
}

public class ActionSheetButton<TData>
    where TData: class, IActionSheetButtonData
{
    public delegate ValueTask HandlerDelegate<TButtonData>(ActionSheetButton<TButtonData>? button, int? index)
        where TButtonData : class, IActionSheetButtonData;
    
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }
        
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }
        
    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }
    
    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }
    
    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string> HtmlAttributes { get; set; } = null!;
    
    [JsonIgnore]
    public HandlerDelegate<TData>? Handler { get; set; }
        
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; set; }
}

public class SimpleActionSheetButton : ActionSheetButton<ActionSheetButtonData>
{
    
}

/*public class ActionSheetButtonEventHelper<TArgs>
{
    private readonly Func<TArgs, Task> _callback;

    public ActionSheetButtonEventHelper(Func<TArgs, Task> callback) => _callback = callback;

    [JSInvokable]
    public Task OnCallbackEvent(TArgs args) => _callback(args);
}*/

public class ActionSheetEventArgs<TData> : EventArgs
    where TData: class, IActionSheetButtonData
{
    public IonActionSheet<TData>? Sender { get; internal set; }
}

public class ActionSheetDismissEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public string? Role { get; internal set; }
    public TData? Data { get; set; }
}

public class ActionSheetButtonHandlerEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public int? Index { get; internal set; }
    public ActionSheetButton<TData>? Button { get; internal set; }
}