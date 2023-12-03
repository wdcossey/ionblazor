using System.Text.Json.Serialization;
using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonAlert : IonComponent, IIonModeComponent
{
    private ElementReference _self;

    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionAlertDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionAlertDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionAlertWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionAlertWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willPresentReference;

    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _buttonHandlerReference;

    private AlertButton[]? _buttons;
    private AlertInput[]? _inputs;
    
    private Func<ValueTask<bool>> _dismissWrapper;
    //private Func<ValueTask> _onDidDismissWrapper;
    //private Func<ValueTask> _onWillDismissWrapper;
    private Func<ValueTask> _presentWrapper;

    /// <summary>
    /// If <b>true</b>, the alert will animate.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// If <b>true</b>, the alert will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool? BackdropDismiss { get; set; }

    [Parameter] public Func<AlertButton[]>? Buttons { get; set; }
    [Parameter] public Func<AlertInput[]>? Inputs { get; set; }

    /// <summary>
    /// Additional classes to apply for custom CSS.
    /// If multiple classes are provided they should be separated by spaces.
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// The main title in the heading of the alert.
    /// </summary>
    [Parameter]
    public string? Header { get; set; }

    /// <summary>
    /// If <b>true</b>, the alert will open. If <b>false</b>, the alert will close. Use this if you need finer
    /// grained control over presentation, otherwise just use the alertController or the <see cref="Trigger"/> property.
    /// Note: <see cref="IsOpen"/> will not automatically be set back to <b>false</b> when the alert dismisses.
    /// You will need to do that in your code.
    /// </summary>
    [Parameter]
    public bool? IsOpen { get; set; }

    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.<p/>
    /// Default: <b>true</b>
    /// </summary>
    [Parameter]
    public bool? KeyboardClose { get; set; }

    /// <summary>
    /// <p>The main message to be displayed in the alert. <b>message</b> can accept either plaintext or HTML as a
    /// string. To display characters normally reserved for HTML, they must be escaped. For example <b>&lt;Ionic&gt;</b>
    /// would become <b>&amp;lt;Ionic&amp;gt;</b></p><br/>
    /// <p>For more information:
    /// <a href="https://ionicframework.com/docs/faq/security">Security Documentation</a></p><br/>
    /// <p>This property accepts custom HTML as a string. Content is parsed as plaintext by default.
    /// innerHTMLTemplatesEnabled must be set to true in the Ionic config before custom HTML can be used.</p>
    /// </summary>
    [Parameter]
    public string? Message { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The subtitle in the heading of the alert. Displayed under the title.
    /// </summary>
    [Parameter]
    public string? SubHeader { get; set; }

    /// <summary>
    /// If <b>true</b>, the alert will be translucent. Only applies when the mode is <see cref="IonMode.iOS"/> and the
    /// device supports
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/backdrop-filter#browser_compatibility">backdrop-filter</a>.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the alert to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the alert has dismissed. Shorthand for ionAlertDidDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the alert has presented. Shorthand for ionAlertWillDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDidPresentEventArgs> DidPresent { get; set; }

    /// <summary>
    /// Emitted after the alert has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> IonAlertDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the alert has presented.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertIonAlertDidPresentEventArgs> IonAlertDidPresent { get; set; }

    /// <summary>
    /// Emitted before the alert has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> IonAlertWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the alert has presented.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertIonAlertWillPresentEventArgs> IonAlertWillPresent { get; set; }

    /// <summary>
    /// Emitted before the alert has dismissed. Shorthand for ionAlertWillDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the alert has presented. Shorthand for ionAlertWillPresent.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertWillPresentEventArgs> WillPresent { get; set; }

    [Parameter] 
    public EventCallback<AlertButtonHandlerEventArgs> ButtonHandler { get; set; }

    public IonAlert()
    {
        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var values = GetValues(args);
            
            await DidDismiss.InvokeAsync(new IonAlertDismissEventArgs
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Values = values
            });
        });

        _didPresentReference = IonicEventCallback.Create(async () => await DidPresent.InvokeAsync(new IonAlertDidPresentEventArgs { Sender = this }));

        _ionAlertDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var values = GetValues(args);

            await IonAlertDidDismiss.InvokeAsync(new IonAlertDismissEventArgs
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Values = values,
            });
        });

        _ionAlertDidPresentReference = IonicEventCallback.Create(async () => await IonAlertDidPresent.InvokeAsync(new IonAlertIonAlertDidPresentEventArgs { Sender = this }));

        _ionAlertWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var values = GetValues(args);

            await IonAlertWillDismiss.InvokeAsync(new IonAlertDismissEventArgs
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Values = values,
            });
        });

        _ionAlertWillPresentReference = IonicEventCallback.Create(async () =>
        {
            await IonAlertWillPresent.InvokeAsync(new IonAlertIonAlertWillPresentEventArgs { Sender = this });
        });

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var values = GetValues(args);
            
            await WillDismiss.InvokeAsync(new IonAlertDismissEventArgs
            {
                Sender = this,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Values = values,
            });
        });

        _willPresentReference = IonicEventCallback.Create(async () =>
        {
            await WillPresent.InvokeAsync(new IonAlertWillPresentEventArgs { Sender = this});
        });

        _buttonHandlerReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                var index = args?["index"]?.GetValue<int?>();
                var button = _buttons?.ElementAtOrDefault(index ?? -1);

                await (button?.Handler?.Invoke(new AlertButtonEventArgs { Button = button, Sender = this, Index = index }) ?? ValueTask.CompletedTask);
                
                await ButtonHandler.InvokeAsync(new AlertButtonHandlerEventArgs { Sender = this, Index = index, Button = button });
            });

    }

    /// <summary>
    /// Dismiss the alert overlay after it has been presented.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> DismissAsync() => await _dismissWrapper();

    /// <summary>
    /// Returns a promise that resolves when the alert did dismiss.
    /// </summary>
    /// <returns></returns>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask OnDidDismissAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Returns a promise that resolves when the alert will dismiss.
    /// </summary>
    /// <returns></returns>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask OnWillDismissAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Present the alert overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentAsync() => await _presentWrapper();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _buttons = Buttons?.Invoke();
        _inputs = Inputs?.Invoke();
        
        var ionComponent = await JsRuntime.ImportAsync("ionAlert");
        
        _dismissWrapper = () => ionComponent.InvokeAsync<bool>("dismiss", _self);
        //_onDidDismissWrapper = () => ionAlertJs.InvokeVoidAsync("onDidDismiss", _self);
        //_onWillDismissWrapper = () => ionAlertJs.InvokeVoidAsync("onWillDismiss", _self);
        _presentWrapper = () => ionComponent.InvokeVoidAsync("present", _self);
        
        await this.AttachIonListenersAsync(_self, new IonEvent[]
        {
            IonEvent.Set("didDismiss", _didDismissReference ),
            IonEvent.Set("didPresent", _didPresentReference ),
            IonEvent.Set("ionAlertDidDismiss", _ionAlertDidDismissReference ),
            IonEvent.Set("ionAlertDidPresent", _ionAlertDidPresentReference ),
            IonEvent.Set("ionAlertWillDismiss", _ionAlertWillDismissReference ),
            IonEvent.Set("ionAlertWillPresent", _ionAlertWillPresentReference ),
            IonEvent.Set("willDismiss", _willDismissReference ),
            IonEvent.Set("willPresent", _willPresentReference )
        });

        if (_buttons?.Length > 0)
            await ionComponent.InvokeVoidAsync("addButtons", _self, _buttons, _buttonHandlerReference);

        if (_inputs?.Length > 0)
            await ionComponent.InvokeVoidAsync("addInputs", _self, _inputs);
    }

    private static IAlertValues GetValues(JsonObject? jObject)
    {
        return jObject?["detail"]?["data"]?["values"] switch
        {
            JsonArray jsonArray => new AlertValuesArray { Values = jsonArray.Deserialize<string[]>() },
            JsonObject jsonObject => new AlertValuesDictionary
            {
                Values = jsonObject.Deserialize<Dictionary<string, string>>()
            },
            _ => new AlertValues { Values = jObject?["detail"]?["data"]?["values"]?.GetValue<string>() }
        };
    }
}

public interface IAlertButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Text { get; }
        
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Role { get; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? CssClass { get; }
    
    [JsonIgnore] 
    Func<AlertButtonEventArgs, ValueTask>? Handler { get; }
}

public record AlertButton : IAlertButton
{
    [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }
        
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }
    
    [JsonPropertyName("htmlAttributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string> HtmlAttributes { get; set; } = null!;

    [JsonIgnore] 
    public Func<AlertButtonEventArgs, ValueTask>? Handler { get; set; } = null!;
}

public class AlertButtonEventArgs : EventArgs
{
    /// <summary>
    /// The <see cref="IonAlert"/> that this event occurred on.
    /// </summary>
    public IonAlert Sender { get; internal set; } = null!;
    
    /// <summary>
    /// The index of the button that was clicked.
    /// </summary>
    public int? Index { get; internal set; }
    
    /// <summary>
    /// The <see cref="IAlertButton" /> that was clicked.
    /// </summary>
    public IAlertButton? Button { get; internal set; }
}

public record AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string? Type { get; set; }
    
    [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    [JsonPropertyName("placeholder"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Placeholder { get; set; }

    [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; set; }

    [JsonPropertyName("label"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; set; }

    [JsonPropertyName("checked"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Checked { get; set; }

    [JsonPropertyName("disabled"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }

    [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Id { get; set; }

    [JsonPropertyName("min"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Min { get; set; }

    [JsonPropertyName("max"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Max { get; set; }

    [JsonPropertyName("cssClass"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CssClass { get; set; }

    [JsonPropertyName("attributes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Attributes { get; set; }

    [JsonPropertyName("tabindex"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TabIndex { get; set; }
    
    /*
type?: TextFieldTypes | 'checkbox' | 'radio' | 'textarea';
  name?: string;
  placeholder?: string;
  value?: any;
    label?: string;
    checked?: boolean;
    disabled?: boolean;
    id?: string;
    handler?: (input: AlertInput) => void;
    min?: string | number;
    max?: string | number;
    cssClass?: string | string[];
    attributes?: { [key: string]: any };
tabindex?: number;
     */
}

public record AlertInputNumber : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "number";
}

public record AlertInputTextArea : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "textarea";
}

public record AlertInputRadio : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "radio";
}

public record AlertInputCheckbox : AlertInput
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Type => "checkbox";
}

public class AlertButtonHandlerEventArgs : EventArgs
{
    
    /// <summary>
    /// The <see cref="IonAlert" /> that this event occurred on.
    /// </summary>
    public IonAlert Sender { get; internal init; } = null!;
    
    /// <summary>
    /// The index of the button that was clicked.
    /// </summary>
    public int? Index { get; internal init; }
    
    /// <summary>
    /// The <see cref="IAlertButton" /> that was clicked.
    /// </summary>
    public IAlertButton? Button { get; internal init; }
}

public class IonAlertDidPresentEventArgs : EventArgs
{
    public IonAlert Sender { get; internal init; } = null!;
}
    
public class IonAlertDismissEventArgs : EventArgs
{
    public IonAlert Sender { get; internal init; } = null!;
    
    public string? Role { get; internal init; }
    
    public IAlertValues? Values { get; internal init; }
}

public class IonAlertIonAlertDidPresentEventArgs : EventArgs
{
    public IonAlert Sender { get; internal init; } = null!;
}

public interface IAlertValues { }

public interface IAlertValues<out TData> : IAlertValues
{
    TData? Values { get; }
}

public class AlertValues : IAlertValues<object>
{
    public object? Values { get; internal init; }
}

public class AlertValuesArray : IAlertValues<string[]>
{
    public string[]? Values { get; internal init; }
}

public class AlertValuesDictionary : IAlertValues<IDictionary<string, string>>
{
    public IDictionary<string, string>? Values { get; internal init; }
}

public class IonAlertIonAlertWillPresentEventArgs : EventArgs
{
    public IonAlert Sender { get; internal init; } = null!;
}

public class IonAlertWillPresentEventArgs : EventArgs
{
    public IonAlert Sender { get; internal init; } = null!;
}
    
