using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IonicTest.Components;

public partial class IonActionSheet<TButtonData> : IonControl
    where TButtonData: class, IActionSheetButtonData
{
    private ElementReference _self;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _didDismissObjectReference = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _didPresentObjectReference = null!;
    
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _willDismissObjectReference = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _willPresentObjectReference = null!;
    private DotNetObjectReference<ActionSheetButtonEventHelper<JsonObject?>> _buttonHandlerObjectReference = null!;
    
    private Guid _id = Guid.NewGuid();
    
    [Parameter] public bool? Animated { get; set; }
    
    [Parameter] public bool? BackdropDismiss { get; set; }
    
    [Parameter, EditorRequired] public Func<IEnumerable<ActionSheetButton<TButtonData>>>? Buttons { get; set; }
    
    [Parameter, Obsolete("Ignored, use `CssClass`", true)] public override string? Class { get; set; }
    
    [Parameter] public string? CssClass { get; set; }
    //[Parameter] public string? EnterAnimation { get; set; }
    [Parameter] public string? Header { get; set; }
    [Parameter] public Func<string[]>? HtmlAttributes { get; set; }
    [Parameter] public bool? IsOpen { get; set; }
    [Parameter] public bool? KeyboardClose { get; set; }
    
    //[Parameter] public string? LeaveAnimation { get; set; }
    [Parameter] public IonicStyleMode? Mode { get; set; }
    [Parameter] public string? SubHeader { get; set; }
    [Parameter] public bool? Translucent { get; set; }
    [Parameter] public string? Trigger { get; set; }
    
    [Parameter] public EventCallback<ActionSheetDidDismissEventArgs<TButtonData>> OnDidDismiss { get; set; }
    [Parameter] public EventCallback OnDidPresent { get; set; }
    [Parameter] public EventCallback<ActionSheetWillDismissEventArgs<TButtonData>> OnWillDismiss { get; set; }
    [Parameter] public EventCallback OnWillPresent { get; set; }
    [Parameter] public EventCallback<ActionSheetButtonHandlerEventArgs<TButtonData>> OnButtonHandler { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        var buttons = Buttons?.Invoke();
        
        _didDismissObjectReference = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await OnDidDismiss.InvokeAsync(new ActionSheetDidDismissEventArgs<TButtonData>()
            {
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        }));
        
        _didPresentObjectReference = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await OnDidPresent.InvokeAsync();
        }));
        
        _willDismissObjectReference = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await OnWillDismiss.InvokeAsync(new ActionSheetWillDismissEventArgs<TButtonData>()
            {
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        }));
        
        _willPresentObjectReference = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await OnWillPresent.InvokeAsync();
        }));
        
        _buttonHandlerObjectReference =  DotNetObjectReference.Create(new ActionSheetButtonEventHelper<JsonObject?>(async args =>
        {
            var index = args?["index"]?.GetValue<int?>();
            await OnButtonHandler.InvokeAsync(new ActionSheetButtonHandlerEventArgs<TButtonData>()
                { Index = index, Button = buttons?.ElementAtOrDefault(index ?? -1) });
        }));

        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "didDismiss", _self, _didDismissObjectReference);
        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "didPresent", _self, _didPresentObjectReference);

        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "willDismiss", _self, _willDismissObjectReference);
        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "willPresent", _self, _willPresentObjectReference);
        
        await JsRuntime.InvokeVoidAsync("addActionSheetButtons", _self, buttons, _buttonHandlerObjectReference);
        //var self = await JsRuntime.InvokeAsync<IJSObjectReference>("document.querySelector", $"ion-action-sheet[data-ionic-id=\"{_id}\"]");
        //await self.InvokeVoidAsync("setProperty", "buttons", JsonSerializer.Serialize(Buttons?.Invoke()));
    }

    public class ActionSheetEventHelper<TArgs>
    {
        private readonly Func<TArgs, Task> _callback;

        public ActionSheetEventHelper(Func<TArgs, Task> callback) => _callback = callback;

        [JSInvokable]
        public Task OnCallbackEvent(TArgs args) => _callback(args);
    }
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
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }
        
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }
        
    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }
        
    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }
        
    //[JsonPropertyName("handler"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //public string? Handler { get; set; }
        
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

public class ActionSheetDidDismissEventArgs<TData> : EventArgs
    where TData: class, IActionSheetButtonData
{
    public string? Role { get; internal set; }
    public TData? Data { get; set; }
}
    
public class ActionSheetWillDismissEventArgs<TData> : EventArgs
    where TData: class, IActionSheetButtonData
{
    public string? Role { get; internal set; }
    public TData? Data { get; set; }
}
    
public class ActionSheetButtonHandlerEventArgs<TData> : EventArgs
    where TData: class, IActionSheetButtonData
{
    public int? Index { get; internal set; }
    public ActionSheetButton<TData>? Button { get; internal set; }
}