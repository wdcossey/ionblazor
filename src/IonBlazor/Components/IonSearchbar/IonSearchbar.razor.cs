namespace IonBlazor.Components;

public sealed partial class IonSearchbar : IonComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionCancelReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionClearReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionInputReference;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If true, enable searchbar animation.
    /// </summary>
    [Parameter]
    public bool? Animated { get; set; }

    /// <summary>
    /// Set the input's autocomplete property.
    /// </summary>
    [Parameter]
    public string? AutoComplete { get; set; } = SearchbarAutoComplete.Off;

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
    public string? CancelButtonIcon { get; set; }

    /// <summary>
    /// Set the the cancel button text. Only applies to ios mode.
    /// </summary>
    [Parameter]
    public string? CancelButtonText { get; set; }

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
    /// <see cref="IonSearchbarUserAction.Enter"/>,
    /// <see cref="IonSearchbarUserAction.Done"/>,
    /// <see cref="IonSearchbarUserAction.Go"/>,
    /// <see cref="IonSearchbarUserAction.Next"/>,
    /// <see cref="IonSearchbarUserAction.Previous"/>,
    /// <see cref="IonSearchbarUserAction.Search"/>,
    /// and <see cref="IonSearchbarUserAction.Send"/>.
    /// </summary>
    [Parameter]
    public string? EnterKeyHint { get; set; } = IonSearchbarUserAction.Default;

    /// <summary>
    /// A hint to the browser for which keyboard to display.
    /// Possible values:
    /// <see cref="IonSearchbarInputMode.None"/>,
    /// <see cref="IonSearchbarInputMode.Text"/>,
    /// <see cref="IonSearchbarInputMode.Tel"/>,
    /// <see cref="IonSearchbarInputMode.Url"/>,
    /// <see cref="IonSearchbarInputMode.Email"/>,
    /// <see cref="IonSearchbarInputMode.Numeric"/>,
    /// <see cref="IonSearchbarInputMode.Decimal"/>,
    /// and <see cref="IonSearchbarInputMode.Search"/>.
    /// </summary>
    [Parameter]
    public string? InputMode { get; set; } = IonSearchbarInputMode.Default;

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
    /// Sets the behavior for the cancel button. Defaults to <see cref="IonSearchbarButtonBehavior.Never"/>.<br/>
    /// Setting to <see cref="IonSearchbarButtonBehavior.Focus"/> shows the cancel button on focus.<br/>
    /// Setting to <see cref="IonSearchbarButtonBehavior.Never"/> hides the cancel button.<br/>
    /// Setting to <see cref="IonSearchbarButtonBehavior.Always"/> shows the cancel button regardless of focus state.
    /// </summary>
    [Parameter]
    public string? ShowCancelButton { get; set; } = IonSearchbarButtonBehavior.Default;

    /// <summary>
    /// Sets the behavior for the clear button. Defaults to <see cref="IonSearchbarButtonBehavior.Focus"/>.<br/>
    /// Setting to <see cref="IonSearchbarButtonBehavior.Focus"/> shows the clear button on focus if the input is not empty.<br/>
    /// Setting to <see cref="IonSearchbarButtonBehavior.Never"/> hides the clear button.<br/>
    /// Setting to <see cref="IonSearchbarButtonBehavior.Always"/> shows the clear button regardless of focus state,
    /// but only if the input is not empty.
    /// </summary>
    [Parameter]
    public string? ShowClearButton { get; set; } = IonSearchbarButtonBehavior.Default;

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
    public string? Type { get; set; } = IonSearchbarInputType.Default;

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
    /// The <see cref="IonChange"/> event is fired for <see cref="IonSearchbar"/> elements when the user modifies the
    /// element's value.
    /// Unlike the <see cref="IonInput"/> event, the <see cref="IonChange"/> event is not necessarily fired for each
    /// alteration to an element's value. <p/>
    /// The <see cref="IonChange"/> event is fired when the value has been committed by the user.
    /// This can happen when the element loses focus or when the "Enter" key is pressed.
    /// <see cref="IonChange"/> can also fire when clicking the clear or cancel buttons.
    /// </summary>
    [Parameter]
    public EventCallback<IonSearchbarChangeEventArgs> IonChange { get; set; }

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
    /// Emitted when the value of the <see cref="IonSearchbar"/> element has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonSearchbarInputEventArgs> IonInput { get; set; }

    public IonSearchbar()
    {
        _ionBlurReference = IonicEventCallback.Create(async () => await IonBlur.InvokeAsync());

        _ionCancelReference = IonicEventCallback.Create(async () => await IonCancel.InvokeAsync());

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>();
            Value = value;

            await IonChange.InvokeAsync(new IonSearchbarChangeEventArgs { Value = value, IsTrusted = isTrusted });
        });

        _ionClearReference = IonicEventCallback.Create(async () => await IonClear.InvokeAsync());

        _ionFocusReference = IonicEventCallback.Create(async () => await IonFocus.InvokeAsync());

        _ionInputReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>();
            //Value = value;

            await IonInput.InvokeAsync(new IonSearchbarInputEventArgs { Value = value, IsTrusted = isTrusted });
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
            IonEvent.Set("ionCancel", _ionCancelReference),
            IonEvent.Set("ionChange", _ionChangeReference),
            IonEvent.Set("ionClear", _ionClearReference),
            IonEvent.Set("ionFocus", _ionFocusReference),
            IonEvent.Set("ionInput", _ionInputReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionCancelReference.Dispose();
        _ionChangeReference.Dispose();
        _ionClearReference.Dispose();
        _ionFocusReference.Dispose();
        _ionInputReference.Dispose();
        await base.DisposeAsync();
    }

    public async Task<IonSearchbar> SetValueAsync(string? value = null)
    {
        await JsComponent.InvokeVoidAsync("setValue", _self, value);
        Value = value;
        return this;
    }

    /// <summary>
    /// Returns the native &lt;input&gt; element used under the hood.
    /// </summary>
    [Obsolete("Not available in Blazor (Razor) projects", true)]
    public async ValueTask<ElementReference> GetInputElementAsync() => await JsComponent.InvokeAsync<ElementReference>("getInputElement", _self);

    /// <summary>
    /// Sets focus on the native input in <see cref="IonSearchbar"/>. Use this method instead of the global
    /// input.focus().<br/>
    /// Developers who wish to focus an input when a page enters should call <see cref="SetFocusAsync"/> in the
    /// ionViewDidEnter() lifecycle method.<br/>
    /// Developers who wish to focus an input when an overlay is presented should call <see cref="SetFocusAsync"/> after
    /// <see cref="IonModal.DidPresent"/> has resolved.
    /// </summary>
    public async ValueTask SetFocusAsync() => await JsComponent.InvokeAsync<string>("setFocus", _self);
}