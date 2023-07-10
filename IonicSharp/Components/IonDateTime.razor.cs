namespace IonicSharp.Components;

public partial class IonDateTime : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionCancelReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _isDateEnabledReference;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The text to display on the picker's cancel button.
    /// </summary>
    [Parameter]
    public string? CancelText { get; set; }

    /// <summary>
    /// The text to display on the picker's "Clear" button.
    /// </summary>
    [Parameter]
    public string? ClearText { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// Values used to create the list of selectable days.
    /// By default every day is shown for the given month.
    /// However, to control exactly which days of the month to display, the dayValues input can take a number,
    /// an array of numbers, or a string of comma separated numbers.
    /// Note that even if the array days have an invalid number for the selected month, like 31 in February,
    /// it will correctly not show days which are not valid for the selected month.
    /// </summary>
    [Parameter]
    public string? DayValues { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the datetime.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// The text to display on the picker's "Done" button.
    /// </summary>
    [Parameter]
    public string? DoneText { get; set; }

    /// <summary>
    /// The first day of the week to use for <see cref="IonDateTime"/>. The default value is 0 and represents Sunday.
    /// </summary>
    [Parameter]
    public int FirstDayOfWeek { get; set; } = 0;

    ///// <summary>
    ///// Used to apply custom text and background colors to specific dates.
    ///// <br/><br/>
    ///// Can be either an array of objects containing ISO strings and colors, or a callback that receives an ISO
    ///// string and returns the colors.
    ///// <br/><br/>
    ///// Only applies to the date, date-time, and time-date presentations, with preferWheel="false".
    ///// </summary>
    //[Parameter] public object? HighlightedDates { get; set; }

    /// <summary>
    /// The hour cycle of the <see cref="IonDateTime"/>. If no value is set, this is specified by the current locale.
    /// </summary>
    [Parameter]
    public string? HourCycle { get; set; } = IonDateTimeHourCycle.Default;

    /// <summary>
    /// Values used to create the list of selectable hours.
    /// By default the hour values range from 0 to 23 for 24-hour, or 1 to 12 for 12-hour.
    /// However, to control exactly which hours to display, the hourValues input can take a number,
    /// an array of numbers, or a string of comma separated numbers.
    /// </summary>
    [Parameter]
    public string? HourValues { get; set; }

    /// <summary>
    /// Returns if an individual date (calendar day) is enabled or disabled.
    /// <br/><br/>
    /// If <b>true</b>, the day will be enabled/interactive.<br/>
    /// If <b>false</b>, the day will be disabled/non-interactive.
    /// <br/><br/>
    /// The function accepts an ISO 8601 date string of a given day. By default, all days are enabled. Developers can use this function to write custom logic to disable certain days.
    /// <br/><br/>
    /// The function is called for each rendered calendar day, for the previous, current and next month. Custom implementations should be optimized for performance to avoid jank.
    /// </summary>
    [Parameter]
    public Func<string?, bool>? IsDateEnabled { get; set; }

    /// <summary>
    /// The locale to use for <see cref="IonDateTime"/>. This impacts month and day name formatting.
    /// The "default" value refers to the default locale set by your device.
    /// </summary>
    [Parameter]
    public string? Locale { get; set; }

    /// <summary>
    /// The maximum datetime allowed. Value must be a date string following the ISO 8601 datetime format standard,
    /// 1996-12-19. The format does not have to be specific to an exact datetime.
    /// For example, the maximum could just be the year, such as 1994. Defaults to the end of this year.
    /// </summary>
    [Parameter]
    public string? Max { get; set; }

    /// <summary>
    /// The minimum datetime allowed. Value must be a date string following the ISO 8601 datetime format standard,
    /// such as 1996-12-19. The format does not have to be specific to an exact datetime.
    /// For example, the minimum could just be the year, such as 1994.
    /// Defaults to the beginning of the year, 100 years ago from today.
    /// </summary>
    [Parameter]
    public string? Min { get; set; }

    /// <summary>
    /// Values used to create the list of selectable minutes.
    /// By default the minutes range from 0 to 59. However, to control exactly which minutes to display,
    /// the minuteValues input can take a number, an array of numbers, or a string of comma separated numbers.
    /// For example, if the minute selections should only be every 15 minutes, then this input value would be
    /// minuteValues="0,15,30,45".
    /// </summary>
    [Parameter]
    public string? MinuteValues { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Values used to create the list of selectable months. By default the month values range from 1 to 12.
    /// However, to control exactly which months to display, the monthValues input can take a number, an array of
    /// numbers, or a string of comma separated numbers. For example, if only summer months should be shown,
    /// then this input value would be monthValues="6,7,8". Note that month numbers do not have a zero-based index,
    /// meaning January's value is 1, and December's is 12.
    /// </summary>
    [Parameter]
    public string? MonthValues { get; set; }

    /// <summary>
    /// If <b>true</b>, multiple dates can be selected at once.
    /// Only applies to presentation="date" and preferWheel="false".
    /// </summary>
    [Parameter]
    public bool Multiple { get; set; }

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string Name { get; set; }

    /// <summary>
    /// If <b>true</b>, a wheel picker will be rendered instead of a calendar grid where possible.<br/>
    /// If <b>false</b>, a calendar grid will be rendered instead of a wheel picker where possible.
    /// <br/><br/>
    /// A wheel picker can be rendered instead of a grid when presentation is one of the following values:
    /// "date", "date-time", or "time-date".
    /// <br/><br/>
    /// A wheel picker will always be rendered regardless of the preferWheel value when presentation is one of the
    /// following values: "time", "month", "month-year", or "year".
    /// </summary>
    [Parameter]
    public bool PreferWheel { get; set; }

    /// <summary>
    /// Which values you want to select.<br/>
    /// "date" will show a calendar picker to select the month, day, and year.<br/>
    /// "time" will show a time picker to select the hour, minute, and (optionally) AM/PM.<br/>
    /// "date-time" will show the date picker first and time picker second.<br/>
    /// "time-date" will show the time picker first and date picker second.<br/>
    /// </summary>
    [Parameter]
    public string Presentation { get; set; } = IonDateTimePresentation.DateTime;

    /// <summary>
    /// If <b>true</b>, the datetime appears normal but is not interactive.
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// If <b>true</b>, a "Clear" button will be rendered alongside the default "Cancel" and "OK" buttons at the
    /// bottom of the <see cref="IonDateTime"/> component. Developers can also use the button slot if they want to
    /// customize these buttons. If custom buttons are set in the button slot then the default buttons will not be rendered.
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// If <b>true</b>, the default "Cancel" and "OK" buttons will be rendered at the bottom of the
    /// <see cref="IonDateTime"/> component. Developers can also use the button slot if they want to customize
    /// these buttons. If custom buttons are set in the button slot then the default buttons will not be rendered.
    /// </summary>
    [Parameter]
    public bool ShowDefaultButtons { get; set; }

    /// <summary>
    /// If <b>true</b>, the default "Time" label will be rendered for the time selector of the ion-datetime component.
    /// Developers can also use the time-label slot if they want to customize this label.
    /// If a custom label is set in the time-label slot then the default label will not be rendered.
    /// </summary>
    [Parameter]
    public bool ShowDefaultTimeLabel { get; set; } = true;

    /// <summary>
    /// If <b>true</b>, a header will be shown above the calendar picker.
    /// This will include both the slotted title, and the selected date.
    /// </summary>
    [Parameter]
    public bool ShowDefaultTitle { get; set; }

    /// <summary>
    /// If cover, the ion-datetime will expand to cover the full width of its container.
    /// If fixed, the ion-datetime will have a fixed width.
    /// </summary>
    [Parameter]
    public string Size { get; set; } = IonDateTimeSize.Fixed;

    ///// <summary>
    ///// A callback used to format the header text that shows how many dates are selected. Only used if there are 0 or
    ///// more than 1 selected (i.e. unused for exactly 1). By default, the header text is set to "numberOfDates days".
    ///// </summary>
    //[Parameter] public string TitleSelectedDatesFormatter { get; set; }

    /// <summary>
    /// The value of the datetime as a valid ISO 8601 datetime string.
    /// This should be an array of strings only when multiple="true".
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    public async Task<IonDateTime> SetValue(params string[]? value)
    {
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonDateTime.setValue", _self, value ?? Array.Empty<string>());
        Value = value?.Any() is true ? string.Join(',', value) : null;
        return this;
    }

    /// <summary>
    /// Values used to create the list of selectable years. By default the year values range between the min and max
    /// datetime inputs. However, to control exactly which years to display, the yearValues input can take a number,
    /// an array of numbers, or string of comma separated numbers. For example, to show upcoming and recent leap years,
    /// then this input's value would be yearValues="2024,2020,2016,2012,2008".
    /// </summary>
    [Parameter]
    public string? YearValues { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonDateTime"/> loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }

    /// <summary>
    /// Emitted when the datetime selection was cancelled.
    /// </summary>
    [Parameter]
    public EventCallback IonCancel { get; set; }

    /// <summary>
    /// Emitted when the value (selected date) has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonDateTimeChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonDateTime"/> has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }

    public IonDateTime()
    {
        _ionBlurReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonBlur.InvokeAsync(this);
        }));

        _ionCancelReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonCancel.InvokeAsync(this);
        }));

        _ionChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            var isChecked = args?["detail"]?["checked"]?.GetValue<bool?>();
            var value = args?["detail"]?["value"]?.GetValue<string?>();

            //Checked = isChecked is true;
            Value = value;

            await IonChange.InvokeAsync(new IonDateTimeChangeEventArgs { Sender = this, Value = value });
        }));

        _ionFocusReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonFocus.InvokeAsync(this);
        }));

        _isDateEnabledReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(args =>
        {
            var dateString = args?["dateIsoString"]?.GetValue<string?>();
            var result = IsDateEnabled?.Invoke(dateString);
            return Task.FromResult(result);
        }));
    }

    /// <summary>
    /// Emits the ionCancel event and optionally closes the popover or modal that the datetime was presented in.
    /// </summary>
    public async Task CancelAsync(bool? closeOverlay = null) => 
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonDateTime.cancel", _self, closeOverlay);

    /// <summary>
    /// Confirms the selected datetime value, updates the value property, and optionally closes the popover or modal
    /// that the datetime was presented in.
    /// </summary>
    public async Task ConfirmAsync(bool? closeOverlay = null) => 
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonDateTime.confirm", _self, closeOverlay);

    /// <summary>
    /// Resets the internal state of the datetime but does not update the value. Passing a valid ISO-8601 string
    /// will reset the state of the component to the provided date. If no value is provided, the internal state
    /// will be reset to the clamped value of the min, max and today.
    /// </summary>
    public async Task ResetAsync(string? startDate = null) => 
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonDateTime.reset", _self, startDate);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await JsRuntime.InvokeVoidAsync("IonicSharp.attachListeners", new object[]
        {
            new { Event = "ionBlur"  , Ref = _ionBlurReference   },
            new { Event = "ionCancel", Ref = _ionCancelReference },
            new { Event = "ionChange", Ref = _ionChangeReference },
            new { Event = "ionFocus" , Ref = _ionFocusReference  },
            //new { Event = "isDateEnabled", Ref = _isDateEnabledFocusReference}
        }, _self);

        if (IsDateEnabled is not null)
            await JsRuntime.InvokeVoidAsync("IonicSharp.IonDateTime.isDateEnabled", _self, _isDateEnabledReference);
    }
}

public class IonDateTimeChangeEventArgs : EventArgs
{
    public IonDateTime Sender { get; internal set; }
    
    public string? Value { get; internal set; }
}

public static class IonDateTimeHourCycle
{
    public const string? Default = null;
    public const string H12 = "h12";
    public const string H23 = "h23";
}

public static class IonDateTimePresentation
{
    public const string Date = "date";
    public const string DateTime = "date-time";
    public const string Month = "month";
    public const string MonthYear = "month-year";
    public const string Time = "time";
    public const string TimeDate = "time-date";
    public const string Year = "year";
}

public static class IonDateTimeSize
{
    public const string Cover = "cover";
    public const string Fixed = "fixed";
}

public class IonDateTimeEventCallback<TArgs>
{
    private readonly Func<TArgs, Task> _callback;

    public IonDateTimeEventCallback(Func<TArgs, Task> callback) => _callback = callback;

    [JSInvokable]
    public Task OnCallbackEvent(TArgs args) => _callback(args);
}