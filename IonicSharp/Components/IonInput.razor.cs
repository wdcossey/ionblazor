namespace IonicSharp.Components;

public partial class IonInput : IonComponent, IIonColorComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionInputReference;
    //private readonly DotNetObjectReference<IonicEventCallbackResult<JsonObject, string?>> _counterFormatterReference;

    public ElementReference Reference => _self;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// This attribute is ignored. Deprecated
    /// </summary>
    [Obsolete("This attribute is ignored", true)]
    public string? Accept { get; set; }

    /// <summary>
    /// Indicates whether and how the text value should be automatically capitalized as it is entered/edited by the user.
    /// Available options: `"off"`, `"none"`, `"on"`, `"sentences"`, `"words"`, `"characters"`.
    /// </summary>
    [Parameter]
    public string? Autocapitalize { get; set; }

    ///<summary>
    /// Indicates whether the value of the control can be automatically completed by the browser.
    ///</summary>
    [Parameter]
    public string? Autocomplete { get; set; }

    /// <summary>
    /// Whether auto correction should be enabled when the user is entering/editing the text value.
    /// </summary>
    [Parameter]
    public string? Autocorrect { get; set; }

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
    public string? Color { get; set; }
    
    /// <summary>
    /// If <b>true</b>, a character counter will display the ratio of characters used and the total character limit.
    /// Developers must also set the <b>maxlength</b> property for the counter to be calculated correctly.
    /// </summary>
    [Parameter]
    public bool? Counter { get; set; }
    
    ///// <summary>
    ///// A callback used to format the counter text. By default the counter text is set to "itemLength / maxLength".
    ///// </summary>
    //[Parameter]
    //public Func<int, int, string>? CounterFormatter { get; set; }
    
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
    /// "start": The label will appear to the left of the input in LTR and to the right in RTL.
    /// "end": The label will appear to the right of the input in LTR and to the left in RTL.
    /// "floating": The label will appear smaller and above the input when the input is focused or it has a value.
    /// Otherwise it will appear on top of the input.
    /// "stacked": The label will appear smaller and above the input regardless even when the input is blurred or
    /// has no value.
    /// "fixed": The label has the same behavior as "start" except it also has a fixed width.
    /// Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter]
    public string? LabelPlacement { get; set; }
    
    /// <summary>
    /// Set the legacy property to true to forcibly use the legacy form control markup.
    /// Ionic will only opt components in to the modern form markup when they are using either the aria-label attribute
    /// or the label property. As a result, the legacy property should only be used as an escape hatch when you want to
    /// avoid this automatic opt-in behavior.
    /// <br/><br/>
    /// Note that this property will be removed in an upcoming major release of Ionic, and all form components will be
    /// opted-in to using the modern form markup.
    /// </summary>
    [Parameter, Obsolete("Note that this property will be removed in an upcoming major release of Ionic")]
    public bool? Legacy { get; set; }
    
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
    public string? Shape { get; set; }
    
    /// <summary>
    /// The initial size of the control. This value is in pixels unless the value of the type attribute is
    /// "text" or "password", in which case it is an integer number of characters.
    /// This attribute applies only when the type attribute is set to "text", "search", "tel", "url",
    /// "email", or "password", otherwise it is ignored.
    /// </summary>
    [Parameter]
    public int? Size { get; set; }
    
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
    public string? Type { get; set; }
    
    /// <summary>
    /// The value of the input.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }
    
    /// <summary>
    /// Emitted when the input loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }

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
    public EventCallback IonFocus { get; set; }

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
                await JsComponent.InvokeVoidAsync("setValue", _self, inputArgs.Value);
            }
        });
        
        //_counterFormatterReference = IonicEventCallbackResult<JsonObject, string?>.Create(args =>
        //{
        //    var inputLength = args["inputLength"]!.GetValue<int>();
        //    var maxLength = args["maxLength"]!.GetValue<int>();
        //    return Task.FromResult(CounterFormatter?.Invoke(inputLength, maxLength));
        //});
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, new IonEvent[]
        {
            IonEvent.Set("ionBlur"  , _ionBlurReference  ),
            IonEvent.Set("ionChange", _ionChangeReference),
            IonEvent.Set("ionFocus" , _ionFocusReference ),
            IonEvent.Set("ionInput" , _ionInputReference )
        });
        
        await JsComponent.InvokeVoidAsync("counterFormat", _self, CounterFormat);
    }
    
    /*
 Methods
getInputElement
Description	Returns the native <input> element used under the hood.
Signature	getInputElement() => Promise<HTMLInputElement>

setFocus
Description	Sets focus on the native input in ion-input. Use this method instead of the global input.focus().
Developers who wish to focus an input when a page enters should call setFocus() in the ionViewDidEnter() lifecycle method.
Developers who wish to focus an input when an overlay is presented should call setFocus after didPresent has resolved.
Signature	setFocus() => Promise<void>
 */
    
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
        await JsComponent.InvokeVoidAsync("setFocus", _self);
}

public static class IonInputAutocapitalize
{
    public const string? Default = null;
    public const string Off = "off";
    public const string None = "none";
    public const string On = "on";
    public const string Sentences = "sentences";
    public const string Words = "words";
    public const string Characters = "characters";
}

public static class IonInputAutocomplete
{
    public const string Name = "name";
    public const string Email = "email";
    public const string Tel = "tel";
    public const string Url = "url";
    public const string On = "on";
    public const string Off = "off";
    public const string HonorificPrefix = "honorific-prefix";
    public const string GivenName = "given-name";
    public const string AdditionalName = "additional-name";
    public const string FamilyName = "family-name";
    public const string HonorificSuffix = "honorific-suffix";
    public const string Nickname = "nickname";
    public const string Username = "username";
    public const string NewPassword ="new-password";
    public const string CurrentPassword = "current-password";
    public const string OneTimeCode = "one-time-code";
    public const string OrganizationTitle = "organization-title";
    public const string Organization = "organization";
    public const string StreetAddress ="street-address";
    public const string AddressLine1 ="address-line1";
    public const string AddressLine2 ="address-line2";
    public const string AddressLine3 ="address-line3";
    public const string AddressLevel4 ="address-level4";
    public const string AddressLevel3 ="address-level3";
    public const string AddressLevel2 ="address-level2";
    public const string AddressLevel1 ="address-level1";
    public const string Country = "country";
    public const string CountryName = "country-name";
    public const string PostalCode = "postal-code";
    public const string CcName = "cc-name";
    public const string CcGivenName = "cc-given-name";
    public const string CcAdditionalName = "cc-additional-name";
    public const string CcFamilyName = "cc-family-name";
    public const string CcNumber = "cc-number";
    public const string CcExp = "cc-exp";
    public const string CcExpMonth = "cc-exp-month";
    public const string CcExpYear = "cc-exp-year";
    public const string CcCsc = "cc-csc";
    public const string CcType = "cc-type";
    public const string TransactionCurrency = "transaction-currency";
    public const string TransactionAmount = "transaction-amount";
    public const string Language = "language";
    public const string Bday = "bday";
    public const string BdayDay = "bday-day";
    public const string BdayMonth = "bday-month";
    public const string BdayYear = "bday-year";
    public const string Sex = "sex";
    public const string TelCountryCode = "tel-country-code";
    public const string TelNational = "tel-national";
    public const string TelAreaCode = "tel-area-code";
    public const string TelLocal = "tel-local";
    public const string TelExtension = "tel-extension";
    public const string Impp = "impp";
    public const string Photo = "photo";
}

public static class IonInputAutocorrect
{
    public const string? Default = null;
    public const string Off = "off";
    public const string On = "on";
}

public static class IonInputEnterkeyhint
{
    public const string? Default = null;
    public const string Enter = "enter";
    public const string Done = "done";
    public const string Go = "go";
    public const string Next = "next";
    public const string Previous = "previous";
    public const string Search = "search";
    public const string Send = "send";
}

public static class IonInputFill
{
    public const string? Default = null;
    public const string Outline = "outline";
    public const string Solid = "solid";
}

public static class IonInputInputMode
{
    public const string? Default = null;
    public const string Decimal = "decimal";
    public const string Email = "email";
    public const string None = "none";
    public const string Numeric = "numeric";
    public const string Search = "search";
    public const string Tel = "tel";
    public const string Text = "text";
    public const string Url = "url";
}

public static class IonInputLabelPlacement
{
    public const string? Default = null;
    public const string End = "end";
    public const string Fixed = "fixed";
    public const string Floating = "floating";
    public const string Stacked = "stacked";
    public const string Start = "start";
}

public static class IonInputShape
{
    public const string? Default = null;
    public const string Round = "round";
}

public static class IonInputType
{
    public const string? Default = null;
    public const string Date = "date";
    public const string DatetimeLocal = "datetime-local";
    public const string Email = "email";
    public const string Month = "month";
    public const string Number = "number";
    public const string Password = "password";
    public const string Search = "search";
    public const string Tel = "tel";
    public const string Text = "text";
    public const string Time = "time";
    public const string Url = "url";
    public const string Week = "week";
}

public struct IonInputEvent
{
    internal IonInputEvent(bool isTrusted)
    {
        IsTrusted = isTrusted;
    }
    
    /// <summary>
    /// Is <b>true</b> when the event was generated by a user action,
    /// and <b>false</b> when the event was created or modified by a script or dispatcher event
    /// </summary>
    public bool IsTrusted { get; init; }
}

public interface IIonInputEventArgs
{
    string? Value { get; }

    IonInputEvent Event { get; } 
}

public class IonInputChangeEventArgs : EventArgs, IIonInputEventArgs
{
    public IonInput Sender { get; init; } = null!;

    public string? Value { get; init; }

    public IonInputEvent Event { get; init; } 
}

public class IonInputInputEventArgs : EventArgs, IIonInputEventArgs
{
    public IonInput Sender { get; init; } = null!;

    public string? Value { get; set; }

    public IonInputEvent Event { get; init; }
}