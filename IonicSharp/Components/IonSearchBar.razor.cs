namespace IonicSharp.Components;

public partial class IonSearchBar : IonComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    private readonly DotNetObjectReference<IonicEventCallback>? _ionBlurReference = null;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionCancelReference = null;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionChangeReference = null;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionClearReference = null;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionFocusReference = null;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionInputReference = null;

    /// <summary>
    /// If true, enable searchbar animation.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// Set the input's autocomplete property.
    /// </summary>
    [Parameter]
    public string? AutoComplete { get; set; } = SearchBarAutoComplete.Off;

    /// <summary>
    /// Set the input's autocorrect property. <p/>
    /// Defaults to <b>off</b>.
    /// </summary>
    [Parameter]
    public bool? AutoCorrect { get; set; } = false;

    /// <summary>
    /// Set the cancel button icon. Only applies to md mode. <p/>
    /// Defaults to <b>arrow-back-sharp</b>.
    /// </summary>
    [Parameter]
    public string? CancelButtonIcon { get; set; } = "arrow-back-sharp";

    /// <summary>
    /// Set the the cancel button text. Only applies to ios mode.
    /// </summary>
    [Parameter]
    public string? CancelButtonText { get; set; } = "Cancel";

    /// <summary>
    /// Set the clear icon. <p/>
    /// Defaults to <b>close-circle</b> for ios and <b>close-sharp</b> for md.
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// Set the amount of time, in milliseconds, to wait to trigger the `ionInput` event after each keystroke.
    /// </summary>
    [Parameter]
    public int? Debounce { get; set; }

    /// <summary>
    /// If true, the user cannot interact with the input.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// A hint to the browser for which enter key to display.
    /// Possible values:
    /// <see cref="EnterKeyHints.Enter"/>,
    /// <see cref="EnterKeyHints.Done"/>,
    /// <see cref="EnterKeyHints.Go"/>,
    /// <see cref="EnterKeyHints.Next"/>,
    /// <see cref="EnterKeyHints.Previous"/>,
    /// <see cref="EnterKeyHints.Search"/>,
    /// and <see cref="EnterKeyHints.Send"/>.
    /// </summary>
    [Parameter]
    public EnterKeyHints? EnterKeyHint { get; set; }

    /// <summary>
    /// A hint to the browser for which keyboard to display.
    /// Possible values:
    /// <see cref="InputModes.None"/>,
    /// <see cref="InputModes.Text"/>,
    /// <see cref="InputModes.Tel"/>,
    /// <see cref="InputModes.Url"/>,
    /// <see cref="InputModes.Email"/>,
    /// <see cref="InputModes.Numeric"/>,
    /// <see cref="InputModes.Decimal"/>,
    /// and <see cref="InputModes.Search"/>.
    /// </summary>
    [Parameter]
    public InputModes? InputMode { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Set the input's placeholder. placeholder can accept either plaintext or HTML as a string.
    /// To display characters normally reserved for HTML, they must be escaped.
    /// For example &lt;Ionic&gt; would become &amp;lt;Ionic&amp;gt; <p/>
    /// Defaults to <b>Search</b>
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// The icon to use as the search icon.
    /// Defaults to <b>search-outline</b> in ios mode and <b>search-sharp</b> in md mode.
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// Sets the behavior for the cancel button. Defaults to <see cref="ButtonBehavior.Never"/>.<br/>
    /// Setting to <see cref="ButtonBehavior.Focus"/> shows the cancel button on focus.<br/>
    /// Setting to <see cref="ButtonBehavior.Never"/> hides the cancel button.<br/>
    /// Setting to <see cref="ButtonBehavior.Always"/> shows the cancel button regardless of focus state.
    /// </summary>
    [Parameter]
    public ButtonBehavior? ShowCancelButton { get; set; }

    /// <summary>
    /// Sets the behavior for the clear button. Defaults to <see cref="ButtonBehavior.Focus"/>.<br/>
    /// Setting to <see cref="ButtonBehavior.Focus"/> shows the clear button on focus if the input is not empty.<br/>
    /// Setting to <see cref="ButtonBehavior.Never"/> hides the clear button.<br/>
    /// Setting to <see cref="ButtonBehavior.Always"/> shows the clear button regardless of focus state,
    /// but only if the input is not empty.
    /// </summary>
    [Parameter]
    public ButtonBehavior? ShowClearButton { get; set; }

    /// <summary>
    /// If <b>true</b>, enable spellcheck on the input.
    /// </summary>
    [Parameter]
    public bool? SpellCheck { get; set; }

    /// <summary>
    /// Set the type of the input.
    /// </summary>
    /// <returns></returns>
    [Parameter]
    public InputTypes? Type { get; set; }

    /// <summary>
    /// the value of the searchbar.<p/>
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Emitted when the input loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }

    /// <summary>
    /// Emitted when the cancel button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback IonCancel { get; set; }

    /// <summary>
    /// The <see cref="IonChange"/> event is fired for <see cref="IonSearchBar"/> elements when the user modifies the
    /// element's value.
    /// Unlike the <see cref="IonInput"/> event, the <see cref="IonChange"/> event is not necessarily fired for each
    /// alteration to an element's value. <p/>
    /// The <see cref="IonChange"/> event is fired when the value has been committed by the user.
    /// This can happen when the element loses focus or when the "Enter" key is pressed.
    /// <see cref="IonChange"/> can also fire when clicking the clear or cancel buttons.
    /// </summary>
    [Parameter]
    public EventCallback<IonSearchBarChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the clear input button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback IonClear { get; set; }

    /// <summary>
    /// Emitted when the input has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }

    /// <summary>
    /// Emitted when the value of the <see cref="IonSearchBar"/> element has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonSearchBarInputEventArgs> IonInput { get; set; }

    public IonSearchBar()
    {
        _ionBlurReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonBlur.InvokeAsync();
        }));

        _ionCancelReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonCancel.InvokeAsync();
        }));

        _ionChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>();
            Value = value;

            await IonChange.InvokeAsync(new IonSearchBarChangeEventArgs { Value = value, IsTrusted = isTrusted });
        }));

        _ionClearReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonClear.InvokeAsync();
        }));

        _ionFocusReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonFocus.InvokeAsync();
        }));

        _ionInputReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>();
            //Value = value;

            await IonInput.InvokeAsync(new IonSearchBarInputEventArgs { Value = value, IsTrusted = isTrusted });
        }));
    }

    public async Task<IonSearchBar> SetValue(string? value)
    {
        throw new NotImplementedException();
        //await JsRuntime.InvokeVoidAsync("setAccordionGroupValue", _self, value);
        Value = value;
        return this;
    }

    /// <summary>
    /// Returns the native &lt;input&gt; element used under the hood.
    /// </summary>
    public async Task<JsonElement?> GetInputElementAsync()
    {
        return await JsRuntime.InvokeAsync<JsonElement?>("getSearchbarInputElement", _self);
    }

    /// <summary>
    /// Sets focus on the native input in ion-searchbar. Use this method instead of the global input.focus().<p/>
    /// Developers who wish to focus an input when a page enters should call setFocus()
    /// in the ionViewDidEnter() lifecycle method.<p/>
    /// Developers who wish to focus an input when an overlay is presented
    /// should call setFocus after didPresent has resolved.
    /// </summary>
    public async Task SetFocusAsync()
    {
        await JsRuntime.InvokeVoidAsync("setSearchbarFocus", _self);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new object[]
        {
            new { Event = "ionBlur", Ref = _ionBlurReference },
            new { Event = "ionCancel", Ref = _ionCancelReference },
            new { Event = "ionChange", Ref = _ionChangeReference },
            new { Event = "ionClear", Ref = _ionClearReference },
            new { Event = "ionFocus", Ref = _ionFocusReference },
            new { Event = "ionInput", Ref = _ionInputReference },
        }, _self);
    }
}

public static class SearchBarAutoComplete
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
    public const string NewPassword = "new-password";
    public const string CurrentPassword = "current-password";
    public const string OneTimeCode = "one-time-code";
    public const string OrganizationTitle = "organization-title";
    public const string Organization = "organization";
    public const string StreetAddress = "street-address";
    public const string AddressLine1 = "address-line1";
    public const string AddressLine2 = "address-line2";
    public const string AddressLine3 = "address-line3";
    public const string AddressLevel4 = "address-level4";
    public const string AddressLevel3 = "address-level3";
    public const string AddressLevel2 = "address-level2";
    public const string AddressLevel1 = "address-level1";
    public const string Country = "country";
    public const string CountryName = "country-name";
    public const string PostalCode = "postal-code";
    public const string CreditCardName = "cc-name";
    public const string CreditCardGivenName = "cc-given-name";
    public const string CreditCardAdditionalName = "cc-additional-name";
    public const string CreditCardFamilyName = "cc-family-name";
    public const string CreditCardNumber = "cc-number";
    public const string CreditCardExpiry = "cc-exp";
    public const string CreditCardExpiryMonth = "cc-exp-month";
    public const string CreditCardExpiryYear = "cc-exp-year";
    public const string CreditCardCsc = "cc-csc";
    public const string CreditCardType = "cc-type";
    public const string TransactionCurrency = "transaction-currency";
    public const string TransactionAmount = "transaction-amount";
    public const string Language = "language";
    public const string BirthDay = "bday";
    public const string BirthDayDay = "bday-day";
    public const string BirthDayMonth = "bday-month";
    public const string BirthDayYear = "bday-year";
    public const string Sex = "sex";
    public const string TelephoneCountryCode = "tel-country-code";
    public const string TelephoneNational = "tel-national";
    public const string TelephoneAreaCode = "tel-area-code";
    public const string TelephoneLocal = "tel-local";
    public const string TelephoneExtension = "tel-extension";
    public const string InstantMessagingProtocolEndpoint = "impp";
    public const string Photo = "photo";
}

public enum EnterKeyHints
{
    /// <summary>
    /// done
    /// </summary>
    Done,
    
    /// <summary>
    /// enter
    /// </summary>
    Enter,
    
    /// <summary>
    /// go
    /// </summary>
    Go,
    
    /// <summary>
    /// next
    /// </summary>
    Next,
    
    /// <summary>
    /// previous
    /// </summary>
    Previous,
    
    /// <summary>
    /// search
    /// </summary>
    Search,
    
    /// <summary>
    /// send
    /// </summary>
    Send
}

public enum InputModes
{
    Decimal,
    Email,
    None,
    Numeric,
    Search,
    Tel,
    Text,
    Url
}

public enum InputTypes
{
    Email,
    Number,
    Password,
    Search,
    Tel,
    Text,
    Url
}

public enum ButtonBehavior
{
    Always,
    Focus,
    Never
}

public class IonSearchBarChangeEventArgs : EventArgs
{
    public string? Value { get; internal set; }
    public bool? IsTrusted { get; internal set; }
}

public class IonSearchBarInputEventArgs : EventArgs
{
    public string? Value { get; internal set; }
    public bool? IsTrusted { get; internal set; }
}