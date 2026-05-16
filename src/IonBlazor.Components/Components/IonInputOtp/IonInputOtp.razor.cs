namespace IonBlazor.Components;


public sealed partial class IonInputOtp : IonJsContentComponent, IIonColorComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionCompleteReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionInputReference;

    internal override string JsImportName => nameof(IonInputOtp);

    /// <summary>
    /// Indicates whether and how the text value should be automatically capitalized as it is entered/edited by the user.
    /// Available options: <see cref="IonInputAutocapitalize.Off"/>, <see cref="IonInputAutocapitalize.None"/>, <see cref="IonInputAutocapitalize.On"/>,
    /// <see cref="IonInputAutocapitalize.Sentences"/>, <see cref="IonInputAutocapitalize.Words"/>, <see cref="IonInputAutocapitalize.Characters"/>.
    /// </summary>
    [Parameter]
    public string? Autocapitalize { get; set; } = IonInputAutocapitalize.Undefined;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the input.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// The fill for the item.
    /// <ul>
    /// <li>If <see cref="IonInputFill.Solid"/> the item will have a background.</li>
    /// <li>If <see cref="IonInputFill.Outline"/> the item will be transparent with a border.</li>
    /// </ul>
    /// Only available in md mode.
    /// </summary>
    [Parameter]
    public string? Fill { get; set; } = IonInputFill.Undefined;

    /// <summary>
    /// A hint to the browser for which keyboard to display.
    /// Possible values: <see cref="IonInputMode.None"/>, <see cref="IonInputMode.Text"/>,
    /// <see cref="IonInputMode.Tel"/>, <see cref="IonInputMode.Url"/>, <see cref="IonInputMode.Email"/>,
    /// <see cref="IonInputMode.Numeric"/>, <see cref="IonInputMode.Decimal"/>, and
    /// <see cref="IonInputMode.Search"/>.
    /// </summary>
    [Parameter]
    public string? InputMode { get; set; } = IonInputMode.Undefined;

    /// <summary>
    /// The number of input boxes to display.
    /// </summary>
    [Parameter]
    public byte? Length { get; set; } = 4;

    /// <summary>
    /// A regex pattern string for allowed characters. Defaults based on type.
    /// For numbers (<b>type="number"</b>): <b>"[\p{N}]"</b> For text (<b>type="text"</b>): <b>"[\p{L}\p{N}]"</b>
    /// </summary>
    [Parameter]
    public string? Pattern { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot modify the value.
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// Where separators should be shown between input boxes. Can be a comma-separated string or an array of numbers.
    /// For example: <b>"3"</b> will show a separator after the 3rd input box. <b>[1,4]</b> will show a separator after the 1st and 4th input boxes. <b>"all"</b> will show a separator between every input box.
    /// </summary>
    [Parameter]
    public string? Separators { get; set; }

    /// <summary>
    /// The shape of the input. If "round" it will have an increased border radius.
    /// </summary>
    [Parameter]
    public string? Shape { get; set; } = IonInputOtpShape.Undefined;

    /// <summary>
    /// The size of the input boxes.
    /// </summary>
    [Parameter]
    public string? Size { get; set; } = IonInputOtpSize.Undefined;

    /// <summary>
    /// The type of control to display. The default type is <see cref="IonInputType.Text"/>.
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonInputOtpType.Number;

    /// <summary>
    /// The value of the input group.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Emitted when the input loses focus.
    /// </summary>
    [Parameter]
    public EventCallback<IonInputOtp> IonBlur { get; set; }

    /// <summary>
    /// The ionChange event is fired when the user modifies the input's value. Unlike the ionInput event,
    /// the ionChange event is only fired when changes are committed, not as the user types.
    /// <br/><br/>
    /// Depending on the way the users interacts with the element, the ionChange event fires at a different moment: -
    /// When the user commits the change explicitly (e.g. by selecting a date from a date picker
    /// for &lt;IonInput Type="date"&gt;, pressing the "Enter" key, etc.). - When the element loses focus after its value
    /// has changed: for elements where the user's interaction is typing.
    /// </summary>
    [Parameter]
    public EventCallback<IonInputOtpChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when all input boxes have been filled with valid values.
    /// </summary>
    [Parameter]
    public EventCallback<IonInputOtpCompleteEventArgs> IonComplete { get; set; }

    /// <summary>
    /// Emitted when the input has focus.
    /// </summary>
    [Parameter]
    public EventCallback<IonInputOtp> IonFocus { get; set; }

    /// <summary>
    /// The ionInput event is fired each time the user modifies the input's value. Unlike the ionChange event, the
    /// ionInput event is fired for each alteration to the input's value. This typically happens for each keystroke as
    /// the user types.
    /// <br/><br/>
    /// For elements that accept text input (type=text, type=tel, etc.), the interface is InputEvent;
    /// for others, the interface is Event. If the input is cleared on edit, the type is null.
    /// </summary>
    [Parameter]
    public EventCallback<IonInputOtpInputEventArgs> IonInputEvent { get; set; }

    public IonInputOtp()
    {
        _ionBlurReference = IonicEventCallback.Create(async () =>
        {
            await IonBlur.InvokeAsync(this);
        });

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>() is true;
            await IonChange.InvokeAsync(new IonInputOtpChangeEventArgs { Sender = this, Value = value, Event = new IonInputEvent(isTrusted) });
        });

        _ionCompleteReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            await IonComplete.InvokeAsync(new IonInputOtpCompleteEventArgs { Sender = this, Value = value });
        });

        _ionFocusReference = IonicEventCallback.Create(async () =>
        {
            await IonFocus.InvokeAsync(this);
        });

        _ionInputReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>() is true;
            await IonInputEvent.InvokeAsync(new IonInputOtpInputEventArgs { Sender = this, Value = value, Event = new IonInputEvent(isTrusted) });
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionBlur", _ionBlurReference),
            IonEvent.Set("ionChange", _ionChangeReference),
            IonEvent.Set("ionComplete", _ionCompleteReference),
            IonEvent.Set("ionFocus", _ionFocusReference),
            IonEvent.Set("ionInput", _ionInputReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionChangeReference.Dispose();
        _ionCompleteReference.Dispose();
        _ionFocusReference.Dispose();
        _ionInputReference.Dispose();
        await base.DisposeAsync();
    }

    public async ValueTask SetFocusAsync() =>
        await JsComponent.InvokeVoidAsync("setFocus", IonElement);

}