using System.Text.Json.Serialization;
using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonRange : IonComponent, IIonContentComponent, IIonColorComponent, IIonModeComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionInputReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionKnobMoveEndReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionKnobMoveStartReference;
    private readonly DotNetObjectReference<IonicEventCallbackResult<int, string?>> _pinFormatterReference;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The start position of the range active bar. This feature is only available with a single knob
    /// (dualKnobs="false"). Valid values are greater than or equal to the min value and less than or equal to the
    /// max value.
    /// </summary>
    [Parameter]
    public int? ActiveBarStart { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }
    
    /// <summary>
    /// How long, in milliseconds, to wait to trigger the ionInput event after each change in the range value.
    /// </summary>
    [Parameter]
    public long? Debounce { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the range.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }
    
    /// <summary>
    /// Show two knobs.
    /// </summary>
    [Parameter]
    public bool? DualKnobs { get; set; }
    
    /// <summary>
    /// The text to display as the control's label. Use this over the label slot if you only need plain text.
    /// The <see cref="Label"/> property will take priority over the <see cref="Label"/> slot if both are used.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Where to place the label relative to the range.
    /// <see cref="IonRangeLabelPlacement.Start"/>: The label will appear to the left of the range in LTR and to the
    /// right in RTL.
    /// <see cref="IonRangeLabelPlacement.End"/>: The label will appear to the right of the range in LTR and to the
    /// left in RTL.
    /// <see cref="IonRangeLabelPlacement.Fixed"/>: The label has the same behavior as
    /// <see cref="IonRangeLabelPlacement.Start"/> except it also has a fixed width.
    /// Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter]
    public string? LabelPlacement { get; set; } = IonRangeLabelPlacement.Default;

    /// <summary>
    /// Set the legacy property to true to forcibly use the legacy form control markup. Ionic will only opt components
    /// in to the modern form markup when they are using either the aria-label attribute or the label property.
    /// As a result, the legacy property should only be used as an escape hatch when you want to avoid this automatic
    /// opt-in behavior.
    /// </summary>
    [Parameter, Obsolete("Note that this property will be removed in an upcoming major release of Ionic")]
    public string? Legacy { get; set; }

    /// <summary>
    /// Maximum integer value of the range.
    /// </summary>
    [Parameter]
    public int? Max { get; set; }

    /// <summary>
    /// Minimum integer value of the range.
    /// </summary>
    [Parameter]
    public int? Min { get; set; } 
    
    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// The name of the control, which is submitted with the form data.<br/>
    /// Default: <b>this.rangeId</b>
    /// </summary>
    [Parameter]
    public string? Name { get; set; }
    
    /// <summary>
    /// If <b>true</b>, a pin with integer value is shown when the knob is pressed.
    /// </summary>
    [Parameter]
    public bool? Pin { get; set; }
    
    /// <summary>
    /// A callback used to format the pin text. By default the pin text is set to Math.round(value).
    /// </summary>
    [Parameter]
    public Func<int,string>? PinFormatter { get; set; }

    /// <summary>
    /// If <b>true</b>, the knob snaps to tick marks evenly spaced based on the step property value.
    /// </summary>
    [Parameter]
    public bool? Snaps { get; set; }
    
    /// <summary>
    /// Specifies the value granularity.
    /// </summary>
    [Parameter]
    public int? Step { get; set; }
    
    /// <summary>
    /// If <b>true</b>, tick marks are displayed based on the step value. Only applies when
    /// <see cref="Snaps"/> is <b>true</b>.
    /// </summary>
    [Parameter]
    public bool? Ticks { get; set; }
    
    /// <summary>
    /// the value of the range.
    /// </summary>
    [Parameter]
    public IRangeValue? Value { get; set; }

    /// <summary>
    /// Set the value of the range.
    /// </summary>
    public ValueTask SetValueAsync(int value) => 
        JsComponent.InvokeVoidAsync("setValue", _self, value);

    /// <summary>
    /// Set the value of the range.
    /// </summary>
    public ValueTask SetValueAsync(int lower, int upper) => 
        JsComponent.InvokeVoidAsync("setUpperLowerValue", _self, lower, upper);
    
    /// <summary>
    /// Emitted when the <see cref="IonRange"/> loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }
    
    /// <summary>
    /// The <see cref="IonChange"/> event is fired for <see cref="IonRange"/> elements when the user modifies the
    /// element's value: - When the user releases the knob after dragging; - When the user moves the knob with
    /// keyboard arrows<br/><br/>
    /// <see cref="IonChange"/> is not fired when the value is changed programmatically.
    /// </summary>
    [Parameter]
    public EventCallback<IRangeChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonRange"/> has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }
    
    /// <summary>
    /// The <see cref="IonInput"/> event is fired for <see cref="IonRange"/> elements when the value is modified.
    /// Unlike <see cref="IonChange"/>, <see cref="IonInput"/> is fired continuously while the user is dragging the knob.
    /// </summary>
    [Parameter]
    public EventCallback<RangeChangeEventArgs> IonInput { get; set; }
    
    /// <summary>
    /// Emitted when the user finishes moving the range knob, whether through mouse drag, touch gesture, or keyboard
    /// interaction.
    /// </summary>
    [Parameter]
    public EventCallback<RangeChangeEventArgs> IonKnobMoveEnd { get; set; }
    
    /// <summary>
    /// Emitted when the user starts moving the range knob, whether through mouse drag, touch gesture, or keyboard
    /// interaction.
    /// </summary>
    [Parameter]
    public EventCallback<RangeChangeEventArgs> IonKnobMoveStart { get; set; }

    public IonRange()
    {
        _ionBlurReference = IonicEventCallback.Create(async () =>
        {
            await IonBlur.InvokeAsync();
        });

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = GetRangeValue(args);
            await IonChange.InvokeAsync(new RangeChangeEventArgs { Sender = this, Value = value });
        });

        _ionFocusReference = IonicEventCallback.Create(async () =>
        {
            await IonFocus.InvokeAsync();
        });

        _ionInputReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = GetRangeValue(args);
            await IonInput.InvokeAsync(new RangeChangeEventArgs { Sender = this, Value = value });
        });

        _ionKnobMoveEndReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = GetRangeValue(args);
            await IonKnobMoveEnd.InvokeAsync(new RangeChangeEventArgs { Sender = this, Value = value });
        });

        _ionKnobMoveStartReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = GetRangeValue(args);
            await IonKnobMoveStart.InvokeAsync(new RangeChangeEventArgs { Sender = this, Value = value });
        });
        
        _pinFormatterReference = IonicEventCallbackResult<int, string?>.Create(value =>
        {
            var result = PinFormatter?.Invoke(value);
            return Task.FromResult(result);
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await this.AttachIonListenersAsync(_self, new []
        {
            IonEvent.Set("ionBlur"         , _ionBlurReference         ),
            IonEvent.Set("ionChange"       , _ionChangeReference       ),
            IonEvent.Set("ionFocus"        , _ionFocusReference        ),
            IonEvent.Set("ionInput"        , _ionInputReference        ),
            IonEvent.Set("ionKnobMoveEnd"  , _ionKnobMoveEndReference  ),
            IonEvent.Set("ionKnobMoveStart", _ionKnobMoveStartReference)
        });
        
        //await (await _lazyIonComponent.Value).InvokeVoidAsync("pinFormatter", _self, _pinFormatterReference);
        
        switch (Value)
        {
            case RangeUpperLowerValue dualRangeValue:
                await SetValueAsync(dualRangeValue.Lower, dualRangeValue.Upper);
                break;
            case RangeValue rangeValue:
                await SetValueAsync(rangeValue.Value);
                break;
        }
    }

    private static IRangeValue? GetRangeValue(JsonObject? args)
    {
        var node = args?["detail"]?["value"];
        return node switch
        {
            JsonValue => new RangeValue
            {
                Value = node.GetValue<int>()
            },
            JsonObject => new RangeUpperLowerValue
            {
                Lower = node["lower"]?.GetValue<int>() ?? 0, Upper = node["upper"]?.GetValue<int>() ?? 0
            },
            _ => null
        };
    }
}

public static class IonRangeLabelPlacement 
{
    public const string? Default = null;
    public const string End = "end";
    public const string Fixed = "fixed";
    public const string Start = "start";
}
public interface IRangeChangeEventArgs
{
    [JsonPropertyName("value")]
    IRangeValue? Value { get; }
}

public class RangeChangeEventArgs : IRangeChangeEventArgs
{
    /// <summary>
    /// The <see cref="IonRange" /> that this event occurred on.
    /// </summary>
    public IonRange? Sender { get; init; }
    
    [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IRangeValue? Value { get; init; }

}

public interface IRangeValue { }

public class RangeValue : IRangeValue
{
    public int Value { get; init; }
}

public class RangeUpperLowerValue : IRangeValue
{
    public int Lower { get; init; }
    public int Upper { get; init; }
}