﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace IonBlazor.Components;

public partial class IonSelect<TValue> : IonComponent, IIonContentComponent, IIonColorComponent, IIonModeComponent 
    where TValue : notnull
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionCancelReference;
    //private readonly DotNetObjectReference<IonicEventCallback<__ionSelectChangeEventArgs<TValue>>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionDismissReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc />
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? Color { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? Mode { get; set; }

    /// <summary>
    /// The text to display on the cancel button.
    /// </summary>
    [Parameter]
    public string? CancelText { get; set; }
    
    /// <summary>
    /// A property name or function used to compare object values
    /// </summary>
    [Parameter]
    public object? CompareWith { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the select.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }
    
    /// <summary>
    /// The toggle icon to show when the select is open.<br/>
    /// If defined, the icon rotation behavior in <b>md</b> mode will be disabled.<br/>
    /// If undefined, <see cref="ToggleIcon"/> will be used for when the select is both open and closed.
    /// </summary>
    [Parameter]
    public string? ExpandedIcon { get; set; }

    /// <summary>
    /// The fill for the item.<br/>
    /// If <see cref="IonSelectFill.Solid"/> the item will have a background.<br/>
    /// If <see cref="IonSelectFill.Outline"/> the item will be transparent with a border. Only available in md mode.
    /// </summary>
    [Parameter]
    public string? Fill { get; set; } = IonSelectFill.Default;
    
    /// <summary>
    /// The interface the select should use:
    /// <see cref="IonSelectInterface.ActionSheet"/>,
    /// <see cref="IonSelectInterface.Alert"/> or
    /// <see cref="IonSelectInterface.Popover"/>.
    /// </summary>
    [Parameter]
    public string? Interface { get; set; } = IonSelectInterface.Default;

    /// <summary>
    /// Any additional options that the <b>alert</b>, <b>action-sheet</b> or <b>popover</b> interface can take.
    /// See the ion-alert docs, the ion-action-sheet docs and the ion-popover docs for the create options for each interface.
    /// <br/><br/>
    /// Note: <see cref="SetInterfaceOptions"/> will not override <b>inputs</b> or <b>buttons</b> with the <b>alert</b>
    /// interface.
    /// </summary>
    public void SetInterfaceOptions()
    {
        throw new NotImplementedException();
    }   

    /// <summary>
    /// How to pack the label and select within a line. <see cref="Justify"/> does not apply when the label and select
    /// are on different lines when <see cref="LabelPlacement"/> is set to <b>"floating"</b> or <b>"stacked"</b>.
    /// <br/><br/>
    /// <see cref="IonSelectJustify.Start"/>: The label and select will appear on the left in LTR and on the right in
    /// RTL.
    /// <br/><br/>
    /// <see cref="IonSelectJustify.End"/>: The label and select will appear on the right in LTR and on the left in
    /// RTL.
    /// <br/><br/>
    /// <see cref="IonSelectJustify.SpaceBetween"/>: The label and select will appear on opposite ends of the line with
    /// space between the
    /// two elements.
    /// </summary>
    [Parameter]
    public string? Justify { get; set; } = IonSelectJustify.Default;

    /// <summary>
    /// The visible label associated with the select.
    /// <br/><br/>
    /// Use this if you need to render a plaintext label.
    /// <br/><br/>
    /// The <see cref="Label"/> property will take priority over the <see cref="Label"/> slot if both are used.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Where to place the label relative to the select.<br/>
    /// <see cref="IonSelectLabelPlacement.Start"/>: The label will appear to the left of the select in LTR and to the
    /// right in RTL. <br/>
    /// <see cref="IonSelectLabelPlacement.End"/>: The label will appear to the right of the select in LTR and to the
    /// left in RTL.<br/>
    /// <see cref="IonSelectLabelPlacement.Floating"/>: The label will appear smaller and above the select when the
    /// select is focused or it has a value. Otherwise it will appear on top of the select.<br/>
    /// <see cref="IonSelectLabelPlacement.Stacked"/>: The label will appear smaller and above the select regardless
    /// even when the select is blurred or has no value.<br/>
    /// <see cref="IonSelectLabelPlacement.Fixed"/>: The label has the same behavior as <b>"start"</b> except it also
    /// has a fixed width. Long text will be truncated with ellipses ("...").<br/>
    /// When using <see cref="IonSelectLabelPlacement.Floating"/> or <see cref="IonSelectLabelPlacement.Stacked"/> we
    /// recommend initializing the select with either a <see cref="Value"/> or a <see cref="Placeholder"/>.
    /// </summary>
    [Parameter]
    public string? LabelPlacement { get; set; } = IonSelectLabelPlacement.Default;

    /// <summary>
    /// Set the <b>legacy</b> property to <b>true</b> to forcibly use the legacy form control markup.
    /// Ionic will only opt components in to the modern form markup when they are using either the <b>aria-label</b>
    /// attribute or the <b>label</b> property. As a result, the <b>legacy</b> property should only be used as an escape
    /// hatch when you want to avoid this automatic opt-in behavior. Note that this property will be removed in an
    /// upcoming major release of Ionic, and all form components will be opted-in to using the modern form markup.
    /// </summary>
    [Parameter, Obsolete("Note that this property will be removed in an upcoming major release of Ionic")]
    public bool? Legacy { get; set; }

    /// <summary>
    /// If <b>true</b>, the select can accept multiple values.
    /// </summary>
    [Parameter]
    public bool? Multiple { get; set; }

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// The text to display on the ok button.
    /// </summary>
    [Parameter]
    public string? OkText { get; set; }

    /// <summary>
    /// The text to display when the select is empty.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// The text to display instead of the selected option's value.
    /// </summary>
    [Parameter]
    public string? SelectedText { get; set; }

    /// <summary>
    /// The shape of the select. If <see cref="IonSelectShape.Round"/> it will have an increased border radius
    /// </summary>
    [Parameter]
    public string? Shape { get; set; } = IonSelectShape.Default;
    
    /// <summary>
    /// The toggle icon to use. Defaults to <b>chevronExpand</b> for ios mode, or <b>caretDownSharp</b> for md mode.
    /// </summary>
    [Parameter]
    public string? ToggleIcon { get; set; }

    /// <summary>
    /// The value of the select.
    /// </summary>
    [Parameter]
    public object? Value { get; set; }

    /// <summary>
    /// Emitted when the input loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }

    /// <summary>
    /// Emitted when the selection is cancelled.
    /// </summary>
    [Parameter]
    public EventCallback IonCancel { get; set; }

    /// <summary>
    /// Emitted when the value has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonSelectChangeEventArgs<TValue>> IonChange { get; set; }

    /// <summary>
    /// Emitted when the overlay is dismissed.
    /// </summary>
    [Parameter]
    public EventCallback IonDismiss { get; set; }

    /// <summary>
    /// Emitted when the input has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }
    
    
    public IonSelect()
    {
        _ionBlurReference = IonicEventCallback.Create(async () => await IonBlur.InvokeAsync());

        _ionCancelReference = IonicEventCallback.Create(async () => await IonCancel.InvokeAsync());
        
        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"];
            var values = value switch
            {
                null => Array.Empty<TValue>(),
                JsonArray => value.Deserialize<TValue[]>(),
                _ => new[] { value.GetValue<TValue>() }
            };
            
            //Value = Multiple is true ? args.Detail.Value.ToArray() : args.Detail.Value.FirstOrDefault();
            //args.Detail.Sender = this;
            
            await IonChange.InvokeAsync(new IonSelectChangeEventArgs<TValue>() { Sender = this, Value = new IonSelectValue<TValue>(values ?? Array.Empty<TValue>()) });
        });

        _ionDismissReference = IonicEventCallback.Create(async () => await IonDismiss.InvokeAsync());

        _ionFocusReference = IonicEventCallback.Create(async () => await IonFocus.InvokeAsync());
        
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, new []
        {
            IonEvent.Set("ionBlur"   , _ionBlurReference   ),
            IonEvent.Set("ionCancel" , _ionCancelReference ),
            IonEvent.Set("ionChange" , _ionChangeReference ),
            IonEvent.Set("ionDismiss", _ionDismissReference),
            IonEvent.Set("ionFocus"  , _ionFocusReference  )
        });
    }
    
    /// <summary>
    /// Open the select overlay.
    /// The overlay is either an alert, action sheet, or popover, depending on the interface property on the
    /// <see cref="IonSelect{TValue}"/>.
    /// </summary>
    /// <returns></returns>
    public ValueTask OpenAsync(/*event?: UIEvent*/)
    {
        throw new NotImplementedException();
    }
}

// ReSharper disable ClassNeverInstantiated.Global
public class IonSelectFill
{
    public const string? Default = null;
    public const string Solid = "solid";
    public const string Outline = "outline";
}

public class IonSelectInterface
{
    public const string? Default = null;
    public const string? ActionSheet = "action-sheet";
    public const string? Alert = "alert";
    public const string? Popover = "popover";
}

public class IonSelectJustify
{
    public const string? Default = null;
    public const string End = "end";
    public const string SpaceBetween = "space-between";
    public const string Start = "start";
}

public class IonSelectLabelPlacement
{
    public const string? Default = null;
    public const string End = "end";
    public const string Fixed = "fixed";
    public const string Floating = "floating";
    public const string Stacked = "stacked";
    public const string Start = "start";
}

public class IonSelectShape
{
    public const string? Default = null;
    public const string Round = "round";
}
// ReSharper restore ClassNeverInstantiated.Global

/*internal sealed class __ionSelectChangeEventArgs<TValue> where TValue : notnull
{
    [JsonPropertyName("tagName")]
    public string? TagName { get; set; }
    
    [JsonPropertyName("detail")]
    public IonSelectChangeEventArgs<TValue> Detail { get; set; } = null! ;
}*/

public sealed class IonSelectChangeEventArgs<TValue> : EventArgs where TValue : notnull
{
    [JsonIgnore]
    public IonSelect<TValue> Sender { get; internal set; } = null!;
    
    [JsonPropertyName("value")] 
    //[JsonConverter(typeof(IonSelectValueConverter))]
    public IonSelectValue<TValue> Value { get; init; } = null!;
}

public class IonSelectValue<TValue> : ReadOnlyCollection<TValue> where TValue : notnull
{
    public IonSelectValue(IList<TValue> list) : base(list) { }
    
    public override string ToString() => string.Join(",", this);
}

/*public class IonSelectValueConverter : JsonConverter<IonSelectValue>
{
    public override IonSelectValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var items = new List<string>();

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                items.Add(reader.GetString()!);
                break;
            case JsonTokenType.StartArray:
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;

                    if (reader.TokenType != JsonTokenType.String)
                        continue;

                    items.Add(reader.GetString()!);
                }
                break;
            }
        }

        return new IonSelectValue(items);
    }
    
    public override void Write(Utf8JsonWriter writer, IonSelectValue value, JsonSerializerOptions options) 
        => throw new NotSupportedException();
}*/