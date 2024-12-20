using System.Text.Json.Serialization;

namespace IonBlazor.Components;

public partial class IonPickerLegacy<TColumn, TColumnOption, TButton> : IonComponent, IIonModeComponent
    where TColumn: class, IPickerColumn<TColumnOption>
    where TColumnOption: class, IPickerColumnOption
    where TButton: class, IPickerButton
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _didPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionPickerDidDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionPickerDidPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionPickerWillDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionPickerWillPresentReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _willPresentReference;

    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _buttonHandlerReference = null!;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the picker will animate.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// If <b>true</b>, the picker will be dismissed when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool? BackdropDismiss { get; set; }

    /// <summary>
    /// Array of buttons to be displayed at the top of the picker.
    /// </summary>
    [Parameter]
    public Func<IReadOnlyCollection<TButton>>? Buttons { get; set; }

    /// <summary>
    /// Array of columns to be displayed in the picker.
    /// </summary>
    [Parameter]
    public Func<IReadOnlyCollection<TColumn>>? Columns { get; set; }

    /// <summary>
    /// Additional classes to apply for custom CSS. If multiple classes are provided they should be separated by spaces.
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Number of milliseconds to wait before dismissing the picker.
    /// </summary>
    [Parameter]
    public int? Duration { get; set; }

    ///// <summary>
    ///// Animation to use when the picker is presented.
    ///// </summary>
    //[Parameter]
    //public object? EnterAnimation { get; set; }

    ///// <summary>
    ///// Additional attributes to pass to the picker.
    ///// </summary>
    //[Parameter]
    //public object? HtmlAttributes { get; set; }

    /// <summary>
    /// If <b>true</b>, the picker will open. If <b>false</b>, the picker will close. Use this if you need finer
    /// grained control over presentation, otherwise just use the pickerController or the <b>trigger</b> property.
    /// Note: <see cref="IsOpen"/> will not automatically be set back to <b>false</b> when the picker dismisses.
    /// You will need to do that in your code.
    /// </summary>
    [Parameter]
    public bool? IsOpen { get; set; }

    /// <summary>
    /// If <b>true</b>, the keyboard will be automatically dismissed when the overlay is presented.
    /// </summary>
    [Parameter]
    public bool? KeyboardClose { get; set; }

    ///// <summary>
    ///// Animation to use when the picker is dismissed.
    ///// </summary>
    //[Parameter]
    //public object? LeaveAnimation { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If <b>true</b>, a backdrop will be displayed behind the picker.
    /// </summary>
    [Parameter]
    public bool? ShowBackdrop { get; set; }

    /// <summary>
    /// An ID corresponding to the trigger element that causes the picker to open when clicked.
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// Emitted after the picker has dismissed. Shorthand for <see cref="IonPickerDidDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>> DidDismiss { get; set; }

    /// <summary>
    /// Emitted after the picker has presented. Shorthand for <see cref="IonPickerWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback DidPresent { get; set; }

    /// <summary>
    /// Emitted after the picker has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>> IonPickerDidDismiss { get; set; }

    /// <summary>
    /// Emitted after the picker has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonPickerDidPresent { get; set; }

    /// <summary>
    /// Emitted before the picker has dismissed.
    /// </summary>
    [Parameter]
    public EventCallback<IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>> IonPickerWillDismiss { get; set; }

    /// <summary>
    /// Emitted before the picker has presented.
    /// </summary>
    [Parameter]
    public EventCallback IonPickerWillPresent { get; set; }

    /// <summary>
    /// Emitted before the picker has dismissed. Shorthand for <see cref="IonPickerWillDismiss"/>.
    /// </summary>
    [Parameter]
    public EventCallback<IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>> WillDismiss { get; set; }

    /// <summary>
    /// Emitted before the picker has presented. Shorthand for <see cref="IonPickerWillPresent"/>.
    /// </summary>
    [Parameter]
    public EventCallback WillPresent { get; set; }

    [Parameter]
    public EventCallback<IonPickerLegacyButtonHandlerEventArgs<TColumn, TColumnOption, TButton>> ButtonHandler { get; set; }

    public IonPickerLegacy()
    {
        _didDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            var data = args?["detail"]?["data"]?.Deserialize<Dictionary<string, PickedColumnOption>>();
            await DidDismiss.InvokeAsync(new IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>() { Sender = this, Data = data, Role = role });
        });

        _didPresentReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            //IsOpen = true;
            await DidPresent.InvokeAsync(this);
        });

        _ionPickerDidDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            var data = args?["detail"]?["data"]?.Deserialize<Dictionary<string, PickedColumnOption>>();
            await IonPickerDidDismiss.InvokeAsync(new IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton> { Sender = this, Data = data, Role = role });
        });

        _ionPickerDidPresentReference = IonicEventCallback<JsonObject?>.Create(async _ =>
        {
            await IonPickerDidPresent.InvokeAsync(this);
        });

        _ionPickerWillDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            var data = args?["detail"]?["data"]?.Deserialize<Dictionary<string, PickedColumnOption>>();
            await IonPickerWillDismiss.InvokeAsync(new IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton> { Sender = this, Data = data, Role = role });
        });

        _ionPickerWillPresentReference = IonicEventCallback<JsonObject?>.Create(async _ => await IonPickerWillPresent.InvokeAsync(this));

        _willDismissReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var role = args?["detail"]?["role"]?.GetValue<string>();
            var data = args?["detail"]?["data"]?.Deserialize<Dictionary<string, PickedColumnOption>>();
            await WillDismiss.InvokeAsync(new IonPickerLegacyDismissEventArgs<TColumn, TColumnOption, TButton>() { Sender = this, Data = data, Role = role });
        });

        _willPresentReference = IonicEventCallback<JsonObject?>.Create(async _ => await WillPresent.InvokeAsync(this));
    }

    /// <summary>
    /// Dismiss the picker overlay after it has been presented.
    /// </summary>
    public async Task DismissAsync(object[]? data = null, string? role = null) =>
        await JsComponent.InvokeVoidAsync("dismiss", _self, data, role);

    /// <summary>
    /// Get the column that matches the specified name.
    /// </summary>
    public async Task<object> GetColumnAsync(string name) =>
        await JsComponent.InvokeAsync<JsonObject>("getColumn", _self, name);

    /// <summary>
    /// Present the picker overlay after it has been created.
    /// </summary>
    public async Task PresentAsync() =>
        await JsComponent.InvokeVoidAsync("present", _self);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            _self,
            IonEvent.Set("didDismiss", _didDismissReference),
            IonEvent.Set("didPresent", _didPresentReference),
            IonEvent.Set("ionPickerDidDismiss", _ionPickerDidDismissReference),
            IonEvent.Set("ionPickerDidPresent", _ionPickerDidPresentReference),
            IonEvent.Set("ionPickerWillDismiss", _ionPickerWillDismissReference),
            IonEvent.Set("ionPickerWillPresent", _ionPickerWillPresentReference),
            IonEvent.Set("willDismiss", _willDismissReference),
            IonEvent.Set("willPresent", _willPresentReference)
        );

        var columns = Columns?.Invoke();
        var buttons = Buttons?.Invoke();

        _buttonHandlerReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                var index = args?["index"]?.GetValue<int?>();
                var value = args?["value"]?.Deserialize<Dictionary<string, PickedColumnOption>>();
                var button = buttons?.ElementAtOrDefault(index ?? -1);
                await (button?.Handler?.Invoke(value) ?? ValueTask.CompletedTask);

                await ButtonHandler.InvokeAsync(
                    new IonPickerLegacyButtonHandlerEventArgs<TColumn, TColumnOption, TButton>()
                    {
                        Sender = this,
                        Index = index,
                        Button = button,
                        Value = value
                    });
            });

        await JsComponent.InvokeVoidAsync("withColumns", _self, columns);
        await JsComponent.InvokeVoidAsync("withButtons", _self, buttons, _buttonHandlerReference);
    }

    public override async ValueTask DisposeAsync()
    {
        _didDismissReference.Dispose();
        _didPresentReference.Dispose();
        _ionPickerDidDismissReference.Dispose();
        _ionPickerDidPresentReference.Dispose();
        _ionPickerWillDismissReference.Dispose();
        _ionPickerWillPresentReference.Dispose();
        _willDismissReference.Dispose();
        _willPresentReference.Dispose();
        _buttonHandlerReference.Dispose();
        await base.DisposeAsync();
    }
}