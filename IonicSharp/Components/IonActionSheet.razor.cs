﻿using System.Text.Json.Serialization;

namespace IonicSharp.Components;

public partial class IonActionSheet<TButtonData> : IonComponent
    where TButtonData: class, IActionSheetButtonData
{
    private ElementReference _self;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _didDismissObjectReference = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _didPresentObjectReference = null!;
    
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _ionActionSheetDidDismissRef = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _ionActionSheetDidPresentRef = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _ionActionSheetWillDismissRef = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _ionActionSheetWillPresentRef = null!;
    
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _willDismissObjectReference = null!;
    private DotNetObjectReference<ActionSheetEventHelper<JsonObject?>>? _willPresentObjectReference = null!;
    private DotNetObjectReference<ActionSheetButtonEventHelper<JsonObject?>> _buttonHandlerObjectReference = null!;
    
    private readonly Guid _id = Guid.NewGuid();
    
    [Parameter] public bool? Animated { get; set; }
    
    [Parameter] public bool? BackdropDismiss { get; set; }
    
    [Parameter] public Func<IEnumerable<ActionSheetButton<TButtonData>>>? Buttons { get; set; }
    
    [Parameter, Obsolete("Ignored, use `CssClass`", true)] public override string? Class { get; set; }
    
    [Parameter] public string? CssClass { get; set; }
    //[Parameter] public string? EnterAnimation { get; set; }
    [Parameter] public string? Header { get; set; }
    [Parameter] public Func<string[]>? HtmlAttributes { get; set; }
    [Parameter] public bool? IsOpen { get; set; }
    [Parameter] public bool? KeyboardClose { get; set; }
    
    //[Parameter] public string? LeaveAnimation { get; set; }
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
    [Parameter] public string? SubHeader { get; set; }
    [Parameter] public bool? Translucent { get; set; }
    [Parameter] public string? Trigger { get; set; }
    
    [Parameter] public EventCallback<ActionSheetDidDismissEventArgs<TButtonData>> OnDidDismiss { get; set; }
    [Parameter] public EventCallback OnDidPresent { get; set; }
    [Parameter] public EventCallback<IonActionSheetDidDismissEventArgs<TButtonData>> IonActionSheetDidDismiss { get; set; }
    [Parameter] public EventCallback IonActionSheetDidPresent { get; set; }
    [Parameter] public EventCallback<IonActionSheetDidDismissEventArgs<TButtonData>> IonActionSheetWillDismiss { get; set; }
    [Parameter] public EventCallback IonActionSheetWillPresent { get; set; }
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
        
        _ionActionSheetDidDismissRef = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await IonActionSheetDidDismiss.InvokeAsync(new IonActionSheetDidDismissEventArgs<TButtonData>()
            {
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        }));

        _ionActionSheetDidPresentRef = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await IonActionSheetDidPresent.InvokeAsync();
        }));
        
        _ionActionSheetWillDismissRef = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await IonActionSheetWillDismiss.InvokeAsync(new IonActionSheetDidDismissEventArgs<TButtonData>()
            {
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<TButtonData>(),
            });
        }));
        
        _ionActionSheetWillPresentRef = DotNetObjectReference.Create(new ActionSheetEventHelper<JsonObject?>(async args =>
        {
            await IonActionSheetWillPresent.InvokeAsync();
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

        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new []
        {
            new { Event = "didDismiss", Ref = _didDismissObjectReference},
            new { Event = "didPresent", Ref = _didPresentObjectReference},
            
            new { Event = "ionActionSheetDidDismiss", Ref = _ionActionSheetDidDismissRef},
            new { Event = "ionActionSheetDidPresent", Ref = _ionActionSheetDidPresentRef},
            new { Event = "ionActionSheetWillDismiss", Ref = _ionActionSheetWillDismissRef},
            new { Event = "ionActionSheetWillPresent", Ref = _ionActionSheetWillPresentRef},
            
            new { Event = "willDismiss", Ref = _willDismissObjectReference},
            new { Event = "willPresent", Ref = _willPresentObjectReference},
        }, _self);
        
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

public class IonActionSheetDidDismissEventArgs<TData> : EventArgs
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