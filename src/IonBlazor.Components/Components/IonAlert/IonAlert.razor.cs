namespace IonBlazor.Components;

public sealed partial class IonAlert : IonComponent, IIonModeComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionAlertDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionAlertDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionAlertWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionAlertWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _buttonHandlerReference;

    private IReadOnlyList<IAlertButton> _buttons = null!;

    private IReadOnlyList<IAlertInput> _inputs = null!;

    public delegate void ButtonBuilder(AlertButtonBuilder builder);

    public delegate void InputBuilder(AlertInputBuilder builder);

    protected override string JsImportName => nameof(IonAlert);

    /// <summary>
    /// If <b>true</b>, the alert will animate.
    /// </summary>
    [Parameter]
    public bool? Animated { get; init; }

    /// <summary>
    /// If <b>true</b>, the alert will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool? BackdropDismiss { get; init; }

    [Parameter]
    public ButtonBuilder? ButtonsBuilder { get; init; }

    [Parameter]
    public InputBuilder? InputsBuilder { get; init; }

    /// <summary>
    /// Additional classes to apply for custom CSS.
    /// If multiple classes are provided they should be separated by spaces.
    /// </summary>
    [Parameter]
    public string? CssClass { get; init; }

    /// <summary>
    /// The main title in the heading of the alert.
    /// </summary>
    [Parameter]
    public string? Header { get; init; }

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
    public bool? KeyboardClose { get; init; }

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
    public string? Message { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The subtitle in the heading of the alert. Displayed under the title.
    /// </summary>
    [Parameter]
    public string? SubHeader { get; init; }

    /// <summary>
    /// If <b>true</b>, the alert will be translucent. Only applies when the mode is <see cref="IonMode.iOS"/> and the
    /// device supports
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/backdrop-filter#browser_compatibility">backdrop-filter</a>.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; init; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the alert to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; init; }

    /// <summary>
    /// Emitted after the alert has dismissed. Shorthand for ionAlertDidDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> DidDismiss { get; init; }

    /// <summary>
    /// Emitted after the alert has presented. Shorthand for ionAlertWillDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDidPresentEventArgs> DidPresent { get; init; }

    /// <summary>
    /// Emitted after the alert has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> IonAlertDidDismiss { get; init; }

    /// <summary>
    /// Emitted after the alert has presented.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertIonAlertDidPresentEventArgs> IonAlertDidPresent { get; init; }

    /// <summary>
    /// Emitted before the alert has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> IonAlertWillDismiss { get; init; }

    /// <summary>
    /// Emitted before the alert has presented.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertIonAlertWillPresentEventArgs> IonAlertWillPresent { get; init; }

    /// <summary>
    /// Emitted before the alert has dismissed. Shorthand for ionAlertWillDismiss.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertDismissEventArgs> WillDismiss { get; init; }

    /// <summary>
    /// Emitted before the alert has presented. Shorthand for ionAlertWillPresent.
    /// </summary>
    [Parameter]
    public EventCallback<IonAlertWillPresentEventArgs> WillPresent { get; init; }

    [Parameter]
    public EventCallback<AlertButtonHandlerEventArgs> ButtonHandler { get; init; }

    public IonAlert()
    {
        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonAlertDismissEventArgs eventArgs = AsDismissEventArgs(args);
            await DidDismiss.InvokeAsync(eventArgs);
        });

        _didPresentReference = IonicEventCallback.Create(async () => await DidPresent.InvokeAsync(new IonAlertDidPresentEventArgs { Sender = this }));

        _ionAlertDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonAlertDismissEventArgs eventArgs = AsDismissEventArgs(args);
            await IonAlertDidDismiss.InvokeAsync(eventArgs);
        });

        _ionAlertDidPresentReference = IonicEventCallback.Create(async () => await IonAlertDidPresent.InvokeAsync(new IonAlertIonAlertDidPresentEventArgs { Sender = this }));

        _ionAlertWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonAlertDismissEventArgs eventArgs = AsDismissEventArgs(args);
            await IonAlertWillDismiss.InvokeAsync(eventArgs);
        });

        _ionAlertWillPresentReference = IonicEventCallback.Create(async () =>
        {
            await IonAlertWillPresent.InvokeAsync(new IonAlertIonAlertWillPresentEventArgs { Sender = this });
        });

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            IonAlertDismissEventArgs eventArgs = AsDismissEventArgs(args);
            await WillDismiss.InvokeAsync(eventArgs);
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
    public async ValueTask<bool> DismissAsync() => await JsComponent.InvokeAsync<bool>("dismiss", IonElement);

    /// <summary>
    /// Present the alert overlay after it has been created.
    /// </summary>
    /// <returns></returns>
    public async ValueTask PresentAsync() => await JsComponent.InvokeVoidAsync("present", IonElement);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("didDismiss", _didDismissReference),
            IonEvent.Set("didPresent", _didPresentReference),
            IonEvent.Set("ionAlertDidDismiss", _ionAlertDidDismissReference),
            IonEvent.Set("ionAlertDidPresent", _ionAlertDidPresentReference),
            IonEvent.Set("ionAlertWillDismiss", _ionAlertWillDismissReference),
            IonEvent.Set("ionAlertWillPresent", _ionAlertWillPresentReference),
            IonEvent.Set("willDismiss", _willDismissReference),
            IonEvent.Set("willPresent", _willPresentReference)
        );

        if (ButtonsBuilder is not null)
        {
            AlertButtonBuilder buttonBuilder = new();
            ButtonsBuilder.Invoke(buttonBuilder);
            _buttons = buttonBuilder.Build();

            if (_buttons.Count > 0)
                await JsComponent.InvokeVoidAsync("addButtons", IonElement, _buttons, _buttonHandlerReference);
        }

        if (InputsBuilder is not null)
        {
            AlertInputBuilder inputBuilder = new();
            InputsBuilder.Invoke(inputBuilder);
            _inputs = inputBuilder.Build();

            if (_inputs.Count > 0)
                await JsComponent.InvokeVoidAsync("addInputs", IonElement, _inputs);
        }
    }

    private IonAlertDismissEventArgs AsDismissEventArgs(JsonObject? args)
    {
        IAlertValues values = GetValues(args);

        return new IonAlertDismissEventArgs
        {
            Sender = this,
            Role = args?["detail"]?["role"]?.GetValue<string>(),
            Values = values,
        };
    }

    internal static IAlertValues GetValues(JsonObject? jObject)
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

    public override async ValueTask DisposeAsync()
    {
        _didDismissReference.Dispose();
        _didPresentReference.Dispose();
        _ionAlertDidDismissReference.Dispose();
        _ionAlertDidPresentReference.Dispose();
        _ionAlertWillDismissReference.Dispose();
        _ionAlertWillPresentReference.Dispose();
        _willDismissReference.Dispose();
        _willPresentReference.Dispose();
        _buttonHandlerReference.Dispose();
        await base.DisposeAsync();
    }
}