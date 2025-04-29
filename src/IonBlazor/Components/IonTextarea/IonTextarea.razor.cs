namespace IonBlazor.Components;

public sealed partial class IonTextarea : IonContentComponent, IIonColorComponent, IIonModeComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionInputReference;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the textarea container will grow and shrink based on the contents of the textarea.
    /// </summary>
    [Parameter]
    public bool? AutoGrow { get; set; }

    /// <summary>
    /// Indicates whether and how the text value should be automatically capitalized as it is entered/edited by the user.
    /// Available options: `"off"`, `"none"`, `"on"`, `"sentences"`, `"words"`, `"characters"`.
    /// </summary>
    [Parameter]
    public string? Autocapitalize { get; set; }

    /// <summary>
    /// Sets the autofocus attribute on the native input element.
    /// <br/><br/>
    /// This may not be sufficient for the element to be focused on page load. See managing focus for more information.
    /// </summary>
    [Parameter]
    public bool? Autofocus { get; set; }

    /// <summary>
    /// If <b>true</b>, the value will be cleared after focus upon edit.
    /// Defaults to <b>true</b> when type is <b>password</b>", <b>false</b> for all other types.
    /// </summary>
    [Parameter]
    public bool? ClearOnEdit { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// The visible width of the text control, in average character widths. If it is specified, it must be a positive integer.
    /// </summary>
    [Parameter]
    public int? Cols { get; set; }

    /// <summary>
    /// If <b>true</b>, a character counter will display the ratio of characters used and the total character limit.
    /// Developers must also set the <b>maxlength</b> property for the counter to be calculated correctly.
    /// </summary>
    [Parameter]
    public bool? Counter { get; set; }

    /// <summary>
    /// Format the counter text. By default the counter text is set to "itemLength / maxLength".
    /// </summary>
    [Parameter]
    public string? CounterFormat { get; set; }

    /// <summary>
    /// Set the amount of time, in milliseconds, to wait to trigger the <see cref="IonInput"/> event after each
    /// keystroke.
    /// </summary>
    [Parameter]
    public int? Debounce { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the input.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// A hint to the browser for which enter key to display.
    /// Possible values: "enter", "done", "go", "next", "previous", "search", and "send".
    /// </summary>
    [Parameter]
    public string? Enterkeyhint { get; set; }

    /// <summary>
    /// Text that is placed under the input and displayed when an error is detected.
    /// </summary>
    [Parameter]
    public string? ErrorText { get; set; }

    /// <summary>
    /// The fill for the item.
    /// If "solid" the item will have a background.
    /// If "outline" the item will be transparent with a border. Only available in md mode.
    /// </summary>
    [Parameter]
    public string? Fill { get; set; }

    /// <summary>
    /// Text that is placed under the input and displayed when no error is detected.
    /// </summary>
    [Parameter]
    public string? HelperText { get; set; }

    /// <summary>
    /// A hint to the browser for which keyboard to display.
    /// Possible values: <see cref="IonInputInputMode.None"/>, <see cref="IonInputInputMode.Text"/>,
    /// <see cref="IonInputInputMode.Tel"/>, <see cref="IonInputInputMode.Url"/>, <see cref="IonInputInputMode.Email"/>,
    /// <see cref="IonInputInputMode.Numeric"/>, <see cref="IonInputInputMode.Decimal"/>, and
    /// <see cref="IonInputInputMode.Search"/>.
    /// </summary>
    [Parameter]
    public string? InputMode { get; set; }

    /// <summary>
    /// The visible label associated with the textarea.
    /// <p/>
    /// Use this if you need to render a plaintext label
    /// <p/>
    /// The label property will take priority over the label slot if both are used.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Where to place the label relative to the textarea.
    /// "start": The label will appear to the left of the textarea in LTR and to the right in RTL.
    /// "end": The label will appear to the right of the textarea in LTR and to the left in RTL.
    /// "floating": The label will appear smaller and above the textarea when the textarea is focused or it has a value.
    /// Otherwise it will appear on top of the textarea.
    /// "stacked": The label will appear smaller and above the textarea regardless even when the textarea is blurred or
    /// has no value.
    /// "fixed": The label has the same behavior as "start" except it also has a fixed width.
    /// Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter]
    public string? LabelPlacement { get; set; }

    /// <summary>
    /// This attribute specifies the maximum number of characters that the user can enter.
    /// </summary>
    [Parameter]
    public int? Maxlength { get; set; }

    /// <summary>
    /// This attribute specifies the minimum number of characters that the user can enter.
    /// </summary>
    [Parameter]
    public int? Minlength { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// IInstructional text that shows before the input has a value.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot modify the value.
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// If <b>true</b>, the user must fill in a value before submitting a form.
    /// </summary>
    [Parameter]
    public bool? Required { get; set; }

    /// <summary>
    /// The number of visible text lines for the control.
    /// </summary>
    [Parameter]
    public int? Rows { get; set; }

    /// <summary>
    /// The shape of the <see cref="IonTextarea"/>. If "round" it will have an increased border radius.
    /// </summary>
    [Parameter]
    public string? Shape { get; set; }

    /// <summary>
    /// If <b>true</b>, the element will have its spelling and grammar checked.
    /// </summary>
    [Parameter]
    public bool? Spellcheck { get; set; }

    /// <summary>
    /// The value of the input.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Indicates how the control wraps text.
    /// </summary>
    [Parameter]
    public bool? Wrap { get; set; }

    /// <summary>
    /// Emitted when the input loses focus.
    /// </summary>
    [Parameter]
    public EventCallback<IonTextarea> IonBlur { get; set; }

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
    public EventCallback<IonTextareaChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the input has focus.
    /// </summary>
    [Parameter]
    public EventCallback<IonTextarea> IonFocus { get; set; }

    /// <summary>
    /// The ionInput event is fired each time the user modifies the input's value. Unlike the ionChange event, the
    /// ionInput event is fired for each alteration to the input's value. This typically happens for each keystroke as
    /// the user types.
    /// <br/><br/>
    /// For elements that accept text input (type=text, type=tel, etc.), the interface is InputEvent;
    /// for others, the interface is Event. If the input is cleared on edit, the type is null.
    /// </summary>
    [Parameter]
    public EventCallback<IonTextareaInputEventArgs> IonInput { get; set; }


    public IonTextarea()
    {
        _ionBlurReference = IonicEventCallback.Create(async () =>
        {
            await IonBlur.InvokeAsync(this);
        });

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>() is true;
            await IonChange.InvokeAsync(new IonTextareaChangeEventArgs() { Sender = this, Value = value, Event = new IonInputEvent(isTrusted) });
        });

        _ionFocusReference = IonicEventCallback.Create(async () =>
        {
            await IonFocus.InvokeAsync(this);
        });

        _ionInputReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>() is true;
            var inputArgs = new IonTextareaInputEventArgs { Sender = this, Value = value, Event = new IonInputEvent(isTrusted) };
            await IonInput.InvokeAsync(inputArgs);

            if (inputArgs.Value?.Equals(value) is false)
            {
                Value = inputArgs.Value;
                await JsComponent.InvokeVoidAsync("setValue", _self, inputArgs.Value);
            }
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            _self,
            IonEvent.Set("ionBlur", _ionBlurReference),
            IonEvent.Set("ionChange", _ionChangeReference),
            IonEvent.Set("ionFocus", _ionFocusReference),
            IonEvent.Set("ionInput", _ionInputReference)
        );

        await JsComponent.InvokeVoidAsync("counterFormat", _self, CounterFormat);
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionChangeReference.Dispose();
        _ionFocusReference.Dispose();
        _ionInputReference.Dispose();
        await base.DisposeAsync();
    }

    /// <summary>
    /// Sets focus on the native textarea in <see cref="IonTextarea"/>. Use this method instead of the global textarea.focus().<br/>
    /// See <a href="https://ionicframework.com/docs/developing/managing-focus">managing focus</a> for more information.
    /// </summary>
    public async ValueTask SetFocusAsync() =>
        await JsComponent.InvokeVoidAsync("setFocus", _self);

    public async ValueTask SetValueAsync(string? value) =>
        await JsComponent.InvokeVoidAsync("setValue", _self, value);

    public async ValueTask MarkTouchedAsync() =>
        await JsComponent.InvokeVoidAsync("markTouched", _self);

    public async ValueTask MarkUnTouchedAsync() =>
        await JsComponent.InvokeVoidAsync("markUnTouched", _self);

    public async ValueTask MarkInvalidAsync() =>
        await JsComponent.InvokeVoidAsync("markInvalid", _self);

    public async ValueTask MarkValidAsync() =>
        await JsComponent.InvokeVoidAsync("markValid", _self);

    public async ValueTask RemoveMarkingAsync() =>
        await JsComponent.InvokeVoidAsync("removeMarking", _self);
}