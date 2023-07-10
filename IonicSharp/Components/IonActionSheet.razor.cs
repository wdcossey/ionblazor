using System.Text.Json.Serialization;

namespace IonicSharp.Components;

public partial class IonActionSheet<TButtonData> : IonComponent, IIonModeComponent
    where TButtonData : class, IActionSheetButtonData
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _didDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback>? _didPresentReference = null!;

    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionActionSheetDidDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback>? _ionActionSheetDidPresentReference = null!;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionActionSheetWillDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback>? _ionActionSheetWillPresentReference = null!;

    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _willDismissReference = null!;
    private DotNetObjectReference<IonicEventCallback>? _willPresentReference = null!;
    private DotNetObjectReference<ActionSheetButtonEventHelper<JsonObject?>> _buttonHandlerReference = null!;

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
    public EventCallback<ActionSheetDidDismissEventArgs<TButtonData>> DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the action sheet has presented. Shorthand for <see cref="IonActionSheetWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> DidPresent { get; set; }

    /// <summary>
    /// Emitted after the action sheet has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonActionSheetDidDismissEventArgs<TButtonData>> IonActionSheetDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the action sheet has presented.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> IonActionSheetDidPresent { get; set; }

    /// <summary>
    /// Emitted before the action sheet has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonActionSheetDidDismissEventArgs<TButtonData>> IonActionSheetWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the action sheet has presented.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetEventArgs<TButtonData>> IonActionSheetWillPresent { get; set; }

    /// <summary>
    /// Emitted before the action sheet has dismissed. Shorthand for <see cref="IonActionSheetWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<ActionSheetWillDismissEventArgs<TButtonData>> WillDismiss { get; set; }

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

        _didDismissReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(async args =>
        {
            await DidDismiss.InvokeAsync(new ActionSheetDidDismissEventArgs<TButtonData>()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        }));

        _didPresentReference = DotNetObjectReference.Create(new IonicEventCallback(async () =>
        {
            await DidPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        }));

        _ionActionSheetDidDismissReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(
            async args =>
            {
                await IonActionSheetDidDismiss.InvokeAsync(new IonActionSheetDidDismissEventArgs<TButtonData>()
                {
                    Sender = this,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
                });
            }));

        _ionActionSheetDidPresentReference = DotNetObjectReference.Create(new IonicEventCallback(async () =>
        {
            await IonActionSheetDidPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        }));

        _ionActionSheetWillDismissReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(
            async args =>
            {
                await IonActionSheetWillDismiss.InvokeAsync(new IonActionSheetDidDismissEventArgs<TButtonData>()
                {
                    Sender = this,
                    Role = args?["detail"]?["role"]?.GetValue<string>(),
                    Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
                });
            }));

        _ionActionSheetWillPresentReference = DotNetObjectReference.Create(new IonicEventCallback(async () =>
        {
            await IonActionSheetWillPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        }));

        _willDismissReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(async args =>
        {
            await WillDismiss.InvokeAsync(new ActionSheetWillDismissEventArgs<TButtonData>()
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        }));

        _willPresentReference = DotNetObjectReference.Create(new IonicEventCallback(async () =>
        {
            await WillPresent.InvokeAsync(new ActionSheetEventArgs<TButtonData>() { Sender = this });
        }));

        _buttonHandlerReference = DotNetObjectReference.Create(new ActionSheetButtonEventHelper<JsonObject?>(
            async args =>
            {
                var index = args?["index"]?.GetValue<int?>();
                var button = buttons?.ElementAtOrDefault(index ?? -1);
                await (button?.Handler?.Invoke(button.Text, button.Role, index, button.Data) ?? ValueTask.CompletedTask);
                
                await ButtonHandler.InvokeAsync(new ActionSheetButtonHandlerEventArgs<TButtonData>()
                {
                    Sender = this,
                    Index = index,
                    Button = button
                });
            }));

        await JsRuntime.InvokeVoidAsync("IonicSharp.attachListeners", new object[]
        {
            new { Event = "didDismiss", Ref = _didDismissReference },
            new { Event = "didPresent", Ref = _didPresentReference },

            new { Event = "ionActionSheetDidDismiss", Ref = _ionActionSheetDidDismissReference },
            new { Event = "ionActionSheetDidPresent", Ref = _ionActionSheetDidPresentReference },
            new { Event = "ionActionSheetWillDismiss", Ref = _ionActionSheetWillDismissReference },
            new { Event = "ionActionSheetWillPresent", Ref = _ionActionSheetWillPresentReference },

            new { Event = "willDismiss", Ref = _willDismissReference },
            new { Event = "willPresent", Ref = _willPresentReference },
        }, _self);

        await JsRuntime.InvokeVoidAsync("IonicSharp.IonActionSheet.addButtons", _self, buttons,
            _buttonHandlerReference);

        //var self = await JsRuntime.InvokeAsync<IJSObjectReference>("document.querySelector", $"ion-action-sheet[data-ionic-id=\"{_id}\"]");
        //await self.InvokeVoidAsync("setProperty", "buttons", JsonSerializer.Serialize(Buttons?.Invoke()));
    }

    /// <summary>
    /// Dismiss the action sheet overlay after it has been presented.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async ValueTask<bool> DismissAsync(IEnumerable<ActionSheetButton<TButtonData>>? data, string? role) =>
        await JsRuntime.InvokeAsync<bool>("IonicSharp.IonActionSheet.dismiss", _self, data, role);

    /// <summary>
    /// Returns a promise that resolves when the action sheet did dismiss.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public async ValueTask OnDidDismissAsync() =>
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonActionSheet.onDidDismiss", _self);

    /// <summary>
    /// Returns a promise that resolves when the action sheet will dismiss.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public async ValueTask OnWillDismissAsync() =>
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonActionSheet.onWillDismiss", _self);

    /// <summary>
    /// Present the action sheet overlay after it has been created.
    /// </summary>
    public async ValueTask PresentAsync() =>
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonActionSheet.present", _self);
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
    public delegate ValueTask HandlerDelegate (string? text, string? role, int? index, TData? data);
    
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }
        
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }
        
    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }
        
    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }
        
    [JsonIgnore]
    public HandlerDelegate? Handler { get; set; }
        
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; set; }
}

public class ActionSheetButtonEventHelper<TArgs>
{
    private readonly Func<TArgs, Task> _callback;

    public ActionSheetButtonEventHelper(Func<TArgs, Task> callback) => _callback = callback;

    [JSInvokable]
    public Task OnCallbackEvent(TArgs args) => _callback(args);
}

public class ActionSheetEventArgs<TData> : EventArgs
    where TData: class, IActionSheetButtonData
{
    public IonActionSheet<TData>? Sender { get; internal set; }
}

public class ActionSheetDidDismissEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public string? Role { get; internal set; }
    public TData? Data { get; set; }
}

public class IonActionSheetDidDismissEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public string? Role { get; internal set; }
    public TData? Data { get; set; }
}

public class ActionSheetWillDismissEventArgs<TData> : ActionSheetEventArgs<TData>
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