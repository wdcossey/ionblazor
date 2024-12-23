namespace IonBlazor.Components;

public sealed partial class IonToast : IonComponent, IIonColorComponent, IIonModeComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionToastDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionToastDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionToastWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionToastWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _willPresentReference;

    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _buttonHandlerReference = null!;

    protected override string JsImportName => nameof(IonToast);

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the toast will animate.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// An array of buttons for the toast.
    /// </summary>
    [Parameter]
    public Func<IReadOnlyCollection<IIonToastButton>>? Buttons { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// How many milliseconds to wait before hiding the toast.
    /// By default, it will show until <see cref="DismissAsync"/> is called.
    /// </summary>
    [Parameter]
    public int? Duration { get; set; }

    //enterAnimation

    /// <summary>
    /// Header to be shown in the toast.
    /// </summary>
    [Parameter]
    public string? Header { get; set; }

    //htmlAttributes

    /// <summary>
    /// The name of the icon to display, or the path to a valid SVG file. See <see cref="IonIcon"/>
    /// <a href="https://ionic.io/ionicons">https://ionic.io/ionicons</a>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// If <b>true</b>, the toast will open. If <b>false</b>, the toast will close. Use this if you need finer
    /// grained control over presentation, otherwise just use the toastController or the <b>trigger</b> property.
    /// Note: <see cref="IsOpen"/> will not automatically be set back to <b>false</b> when the toast dismisses.
    /// You will need to do that in your code.
    /// </summary>
    [Parameter]
    public bool? IsOpen { get; set; }

    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.
    /// </summary>
    [Parameter]
    public bool? KeyboardClose { get; set; }

    /// <summary>
    /// Defines how the message and buttons are laid out in the toast.
    /// <see cref="IonToastLayout.Baseline"/>: The message and the buttons will appear on the same line.
    /// Message text may wrap within the message container.
    /// <see cref="IonToastLayout.Stacked"/>: The buttons containers and message will stack on top of each other.
    /// Use this if you have long text in your buttons.
    /// </summary>
    [Parameter]
    public string? Layout { get; set; } = IonToastLayout.Default;

    //leaveAnimation

    /// <summary>
    /// Message to be shown in the toast. This property accepts custom HTML as a string.
    /// Content is parsed as plaintext by default.
    /// innerHTMLTemplatesEnabled must be set to true in the Ionic config before custom HTML can be used.
    /// </summary>
    [Parameter]
    public string? Message { get; set; }


    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The position of the toast on the screen.
    /// </summary>
    [Parameter]
    public string? Position { get; set; } = IonToastPosition.Default;

    /// <summary>
    /// The element to anchor the toast's position to. Can be set as a direct reference or the ID of the element.<br/>
    /// With <see cref="IonToastPosition.Bottom"/>, the toast will sit above the chosen element.<br/>
    /// With <see cref="IonToastPosition.Top"/>, the toast will sit below the chosen element.<br/>
    /// With <see cref="IonToastPosition.Middle"/>, the value of positionAnchor is ignored.
    /// </summary>
    [Parameter]
    public string? PositionAnchor { get; set; }

    /// <summary>
    /// If <b>true</b>, the toast will be translucent.
    /// Only applies when the mode is <see cref="IonMode.iOS"/> and the device supports
    /// <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/backdrop-filter#browser_compatibility">backdrop-filter</a>.
    /// </summary>
    [Parameter]
    public bool? Translucent { get; set; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the toast to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the toast has dismissed. Shorthand for <see cref="IonToastDidDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonToastDismissEventArgs> DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the toast has presented. Shorthand for <see cref="IonToastWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback DidPresent { get; set; }

    /// <summary>
    /// Emitted after the toast has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonToastDismissEventArgs> IonToastDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the toast has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonToastDidPresent { get; set; }

    /// <summary>
    /// Emitted before the toast has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonToastDismissEventArgs> IonToastWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the toast has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonToastWillPresent { get; set; }

    /// <summary>
    /// Emitted before the toast has dismissed. Shorthand for <see cref="IonToastWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonToastDismissEventArgs> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the toast has presented. Shorthand for <see cref="IonToastWillPresent"/>.
    /// </summary>
    [Parameter]
    public EventCallback WillPresent { get; set; }

    [Parameter]
    public EventCallback<IonToastButtonEventArgs> ButtonHandler { get; set; }

    /// <summary>
    /// Dismiss the toast overlay after it has been presented.
    /// </summary>
    public ValueTask DismissAsync() =>
        JsComponent.InvokeVoidAsync("dismiss", _self);

    public IonToast()
    {
         _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            await DidDismiss.InvokeAsync(new IonToastDismissEventArgs { Sender = this, Role = role });
        });

        _didPresentReference = IonicEventCallback.Create(async () =>
        {
            //IsOpen = true;
            await DidPresent.InvokeAsync(this);
        });

        _ionToastDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            await IonToastDidDismiss.InvokeAsync(new IonToastDismissEventArgs { Sender = this, Role = role });
        });

        _ionToastDidPresentReference = IonicEventCallback.Create(async () => await IonToastDidPresent.InvokeAsync());

        _ionToastWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            await IonToastWillDismiss.InvokeAsync(new IonToastDismissEventArgs { Sender = this, Role = role });
        });

        _ionToastWillPresentReference = IonicEventCallback.Create(async () => await IonToastWillPresent.InvokeAsync(this));

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            await WillDismiss.InvokeAsync(new IonToastDismissEventArgs { Sender = this, Role = role });
        });

        _willPresentReference = IonicEventCallback.Create(async () => await WillPresent.InvokeAsync(this));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            _self,
            IonEvent.Set("didDismiss", _didDismissReference),
            IonEvent.Set("didPresent", _didPresentReference),
            IonEvent.Set("ionToastDidDismiss", _ionToastDidDismissReference),
            IonEvent.Set("ionToastDidPresent", _ionToastDidPresentReference),
            IonEvent.Set("ionToastWillDismiss", _ionToastWillDismissReference),
            IonEvent.Set("ionToastWillPresent", _ionToastWillPresentReference),
            IonEvent.Set("willDismiss", _willDismissReference),
            IonEvent.Set("willPresent", _willPresentReference)
        );

        var buttons = Buttons?.Invoke();

        _buttonHandlerReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                int? index = args?["index"]?.GetValue<int?>();
                IIonToastButton? button = buttons?.ElementAtOrDefault(index ?? -1);
                IonToastButtonEventArgs arguments = new()
                {
                    Sender = this,
                    Index = index,
                    Button = button
                };

                await (button?.Handler?.Invoke(arguments) ?? ValueTask.CompletedTask);

                await ButtonHandler.InvokeAsync(arguments);
            });

        await JsComponent.InvokeVoidAsync("withButtons", _self, buttons, _buttonHandlerReference);
    }

    public override async ValueTask DisposeAsync()
    {
        _didDismissReference.Dispose();
        _didPresentReference.Dispose();
        _ionToastDidDismissReference.Dispose();
        _ionToastDidPresentReference.Dispose();
        _ionToastWillDismissReference.Dispose();
        _ionToastWillPresentReference.Dispose();
        _willDismissReference.Dispose();
        _willPresentReference.Dispose();
        _buttonHandlerReference.Dispose();
        await base.DisposeAsync();
    }
}