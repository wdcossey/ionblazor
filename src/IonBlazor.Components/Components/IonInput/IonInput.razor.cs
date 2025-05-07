namespace IonBlazor.Components;

public sealed partial class IonInput : IonContentComponent, IIonColorComponent, IIonModeComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionInputReference;

    protected override string JsImportName => nameof(IonInput);

    /// <summary>
    /// Indicates whether and how the text value should be automatically capitalized as it is entered/edited by the user.
    /// Available options: <see cref="IonInputAutocapitalize.Off"/>, <see cref="IonInputAutocapitalize.None"/>, <see cref="IonInputAutocapitalize.On"/>,
    /// <see cref="IonInputAutocapitalize.Sentences"/>, <see cref="IonInputAutocapitalize.Words"/>, <see cref="IonInputAutocapitalize.Characters"/>.
    /// </summary>
    [Parameter]
    public string? Autocapitalize { get; set; } = IonInputAutocapitalize.Default;

    ///<summary>
    /// Indicates whether the value of the control can be automatically completed by the browser.
    ///</summary>
    [Parameter]
    public string? Autocomplete { get; set; } = IonInputAutocomplete.Default;

    /// <summary>
    /// Whether auto correction should be enabled when the user is entering/editing the text value.
    /// </summary>
    [Parameter]
    public string? Autocorrect { get; set; } = IonInputAutocorrect.Default;

    /// <summary>
    /// This Boolean attribute lets you specify that a form control should have input focus when the page loads.
    /// </summary>
    [Parameter]
    public bool? Autofocus { get; set; }

    /// <summary>
    /// If <b>true</b>, a clear icon will appear in the input when there is a value. Clicking it clears the input.
    /// </summary>
    [Parameter]
    public bool? ClearInput { get; set; }

    /// <summary>
    /// If <b>true</b>, the value will be cleared after focus upon edit.
    /// Defaults to <b>true</b> when type is <b>password</b>", <b>false</b> for all other types.
    /// </summary>
    [Parameter]
    public bool? ClearOnEdit { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; }

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
    public string? CounterFormatter { get; init; }

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
    /// Possible values: <see cref="IonInputEnterkeyhint.Enter"/>, <see cref="IonInputEnterkeyhint.Done"/>, <see cref="IonInputEnterkeyhint.Go"/>,
    /// <see cref="IonInputEnterkeyhint.Next"/>, <see cref="IonInputEnterkeyhint.Previous"/>, <see cref="IonInputEnterkeyhint.Search"/>,
    /// and <see cref="IonInputEnterkeyhint.Send"/>.
    /// </summary>
    [Parameter]
    public string? Enterkeyhint { get; set; } = IonInputEnterkeyhint.Default;

    /// <summary>
    /// Text that is placed under the input and displayed when an error is detected.
    /// </summary>
    [Parameter]
    public string? ErrorText { get; set; }

    /// <summary>
    /// The fill for the item.
    /// <ul>
    /// <li>If <see cref="IonInputFill.Solid"/> the item will have a background.</li>
    /// <li>If <see cref="IonInputFill.Outline"/> the item will be transparent with a border.</li>
    /// </ul>
    /// Only available in md mode.
    /// </summary>
    [Parameter]
    public string? Fill { get; set; } = IonInputFill.Default;

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
    public string? InputMode { get; set; } = IonInputInputMode.Default;

    /// <summary>
    /// The visible label associated with the input.
    /// <p/>
    /// Use this if you need to render a plaintext label
    /// <p/>
    /// The label property will take priority over the label slot if both are used.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Where to place the label relative to the input.
    /// <ul>
    /// <li><see cref="IonInputLabelPlacement.Start"/>: The label will appear to the left of the input in LTR and to the right in RTL.</li>
    /// <li><see cref="IonInputLabelPlacement.End"/>: The label will appear to the right of the input in LTR and to the left in RTL.</li>
    /// <li><see cref="IonInputLabelPlacement.Floating"/>: The label will appear smaller and above the input when the input is focused or it has a value.
    /// Otherwise it will appear on top of the input.</li>
    /// <li><see cref="IonInputLabelPlacement.Stacked"/>: The label will appear smaller and above the input regardless even when the input is blurred or
    /// has no value.</li>
    /// <li><see cref="IonInputLabelPlacement.Fixed"/>: The label has the same behavior as "start" except it also has a fixed width.
    /// Long text will be truncated with ellipses ("...").</li>
    /// </ul>
    /// </summary>
    [Parameter]
    public string? LabelPlacement { get; set; } = IonInputLabelPlacement.Default;

    /// <summary>
    /// The maximum value, which must not be less than its minimum (min attribute) value.
    /// </summary>
    [Parameter]
    public int? Max { get; set; }

    /// <summary>
    /// If the value of the type attribute is text, email, search, password, tel, or url, this attribute specifies
    /// the maximum number of characters that the user can enter.
    /// </summary>
    [Parameter]
    public int? Maxlength { get; set; }

    /// <summary>
    /// The minimum value, which must not be greater than its maximum (max attribute) value.
    /// </summary>
    [Parameter]
    public int? Min { get; set; }

    /// <summary>
    /// If the value of the type attribute is text, email, search, password, tel, or url, this attribute specifies
    /// the minimum number of characters that the user can enter.
    /// </summary>
    [Parameter]
    public int? Minlength { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If <b>true</b>, the user can enter more than one value.
    /// This attribute applies when the type attribute is set to "email", otherwise it is ignored.
    /// </summary>
    [Parameter]
    public bool? Multiple { get; set; }

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// A regular expression that the value is checked against.
    /// The pattern must match the entire value, not just some subset.
    /// Use the title attribute to describe the pattern to help the user.
    /// This attribute applies when the value of the type attribute is "text", "search", "tel", "url",
    /// "email", "date", or "password", otherwise it is ignored.
    /// When the type attribute is "date", pattern will only be used in browsers that do not support the "date"
    /// input type natively. See https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/date for more
    /// information.
    /// </summary>
    [Parameter]
    public string? Pattern { get; set; }

    /// <summary>
    /// Instructional text that shows before the input has a value.
    /// This property applies only when the type property is set to "email", "number", "password", "search", "tel",
    /// "text", or "url", otherwise it is ignored.
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
    /// The shape of the input. If "round" it will have an increased border radius.
    /// </summary>
    [Parameter]
    public string? Shape { get; set; } = IonInputShape.Default;

    /// <summary>
    /// If <b>true</b>, the element will have its spelling and grammar checked.
    /// </summary>
    [Parameter]
    public bool? Spellcheck { get; set; }

    /// <summary>
    /// Works with the min and max attributes to limit the increments at which a value can be set.
    /// Possible values are: "any" or a positive floating point number.
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// The type of control to display. The default type is <see cref="IonInputType.Text"/>.
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonInputType.Default;

    /// <summary>
    /// The value of the input.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Emitted when the input loses focus.
    /// </summary>
    [Parameter]
    public EventCallback<IonInput> IonBlur { get; set; }

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
    public EventCallback<IonInputChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the input has focus.
    /// </summary>
    [Parameter]
    public EventCallback<IonInput> IonFocus { get; set; }

    /// <summary>
    /// The ionInput event is fired each time the user modifies the input's value. Unlike the ionChange event, the
    /// ionInput event is fired for each alteration to the input's value. This typically happens for each keystroke as
    /// the user types.
    /// <br/><br/>
    /// For elements that accept text input (type=text, type=tel, etc.), the interface is InputEvent;
    /// for others, the interface is Event. If the input is cleared on edit, the type is null.
    /// </summary>
    [Parameter]
    public EventCallback<IonInputInputEventArgs> IonInputEvent { get; set; }

    public IonInput()
    {
        _ionBlurReference = IonicEventCallback.Create(async () =>
        {
            await IonBlur.InvokeAsync(this);
        });

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>() is true;
            await IonChange.InvokeAsync(new IonInputChangeEventArgs() { Sender = this, Value = value, Event = new IonInputEvent(isTrusted) });
        });

        _ionFocusReference = IonicEventCallback.Create(async () =>
        {
            await IonFocus.InvokeAsync(this);
        });

        _ionInputReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>() is true;
            var inputArgs = new IonInputInputEventArgs { Sender = this, Value = value, Event = new IonInputEvent(isTrusted) };
            await IonInputEvent.InvokeAsync(inputArgs);

            if (inputArgs.Value?.Equals(value) is false)
            {
                Value = inputArgs.Value;
                await JsComponent.InvokeVoidAsync("setValue", IonElement, inputArgs.Value);
            }
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
            IonEvent.Set("ionFocus", _ionFocusReference),
            IonEvent.Set("ionInput", _ionInputReference)
        );

        await JsComponent.InvokeVoidAsync("counterFormatter", IonElement, CounterFormatter);
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
    /// Returns the native &lt;input&gt; element used under the hood.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public ValueTask GetInputElement() => throw new NotImplementedException();

    /// <summary>
    /// Sets focus on the native input in ion-input. Use this method instead of the global input.focus().
    /// <br/><br/>
    /// Developers who wish to focus an input when a page enters should call <see cref="SetFocusAsync"/> in the
    /// ionViewDidEnter() lifecycle method.
    /// <br/><br/>
    /// Developers who wish to focus an input when an overlay is presented should call <see cref="SetFocusAsync"/> after
    /// <see cref="IonModal.DidPresent"/> has resolved.
    /// </summary>
    public async ValueTask SetFocusAsync() =>
        await JsComponent.InvokeVoidAsync("setFocus", IonElement);

    public async ValueTask SetValueAsync(string? value) =>
        await JsComponent.InvokeVoidAsync("setValue", IonElement, value);

    public async ValueTask MarkTouchedAsync() =>
        await JsComponent.InvokeVoidAsync("markTouched", IonElement);

    public async ValueTask MarkUnTouchedAsync() =>
        await JsComponent.InvokeVoidAsync("markUnTouched", IonElement);

    public async ValueTask MarkInvalidAsync() =>
        await JsComponent.InvokeVoidAsync("markInvalid", IonElement);

    public async ValueTask MarkValidAsync() =>
        await JsComponent.InvokeVoidAsync("markValid", IonElement);

    public async ValueTask RemoveMarkingAsync() =>
        await JsComponent.InvokeVoidAsync("removeMarking", IonElement);
}