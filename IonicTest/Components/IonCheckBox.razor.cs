using System.Text.Json.Nodes;

namespace IonicTest.Components;

public partial class IonCheckBox : IonSlotControl
{
    private ElementReference _self;
    
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionBlurReference = null;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionChangeReference = null;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionFocusReference = null;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the <see cref="IonCheckBox"/> is selected.
    /// </summary>
    [Parameter] public bool Checked { get; set; } = false;
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonicColor.Primary"/>, <see cref="IonicColor.Secondary"/>,
    /// <see cref="IonicColor.Tertiary"/>, <see cref="IonicColor.Success"/>,
    /// <see cref="IonicColor.Warning"/>, <see cref="IonicColor.Danger"/>,
    /// <see cref="IonicColor.Light"/>, <see cref="IonicColor.Medium"/>,
    /// and <see cref="IonicColor.Dark"/>. <p/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the user cannot interact with the <see cref="IonCheckBox"/>.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the <see cref="IonCheckBox"/> will visually appear as indeterminate.
    /// </summary>
    [Parameter] public bool Indeterminate { get; set; } = false;

    /// <summary>
    /// How to pack the label and <see cref="IonCheckBox"/> within a line.<br/><br/>
    /// <see cref="IonJustify.Start"/>: The label and <see cref="IonCheckBox"/> will appear on the left in LTR
    /// and on the right in RTL.<br/><br/>
    /// <see cref="IonJustify.End"/>: The label and <see cref="IonCheckBox"/> will appear on the right in LTR
    /// and on the left in RTL.<br/><br/>
    /// <see cref="IonJustify.SpaceBetween"/>: The label and <see cref="IonCheckBox"/> will appear on opposite
    /// ends of the line with space between the two elements.
    /// </summary>
    [Parameter] public string Justify { get; set; } = IonJustify.SpaceBetween;

    /// <summary>
    /// Where to place the label relative to the checkbox.<br/><br/>
    /// <see cref="IonLabelPlacement.Start"/>: The label will appear to the left of the <see cref="IonCheckBox"/> in LTR
    /// and to the right in RTL.<br/><br/>
    /// <see cref="IonLabelPlacement.End"/>: The label will appear to the right of the <see cref="IonCheckBox"/> in LTR
    /// and to the left in RTL.<br/><br/>
    /// <see cref="IonLabelPlacement.Fixed"/>: The label has the same behavior as "start" except it also
    /// has a fixed width. Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter] public string LabelPlacement { get; set; } = IonLabelPlacement.Fixed;

    /// <summary>
    /// Set the legacy property to true to forcibly use the legacy form control markup.
    /// Ionic will only opt checkboxes in to the modern form markup when they are using either the aria-label
    /// attribute or have text in the default slot. As a result, the legacy property should only be used as an
    /// escape hatch when you want to avoid this automatic opt-in behavior.
    /// <br/><br/>
    /// Note that this property will be removed in an upcoming major release of Ionic,
    /// and all form components will be opted-in to using the modern form markup.
    /// </summary>
    [Parameter, Obsolete("Note that this property will be removed in an upcoming major release of Ionic")] 
    public bool? Legacy { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public IonicStyleMode? Mode { get; set; }

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter] public string Name { get; set; } = null!;

    /// <summary>
    /// The value of the <see cref="IonCheckBox"/> does not mean if it's checked or not,
    /// use the <see cref="Checked"/> property for that.
    /// <br/><br/>
    /// The value of a <see cref="IonCheckBox"/> is analogous to the value of an &lt;input type="checkbox"&gt;,
    /// it's only used when the checkbox participates in a native &lt;form&gt;.
    /// </summary>
    [Parameter] public string? Value { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonToggle"/> loses focus.
    /// </summary>
    [Parameter] public EventCallback IonBlur { get; set; }

    /// <summary>
    /// Emitted when the user switches the toggle on or off.
    /// Does not emit when programmatically changing the value of the checked property.
    /// </summary>
    [Parameter] public EventCallback<IonCheckBoxChangeEventArgs> IonChange { get; set; }
    
    /// <summary>
    /// Emitted when the <see cref="IonToggle"/> has focus.
    /// </summary>
    [Parameter] public EventCallback IonFocus { get; set; }
    
    public IonCheckBox()
    {
        _ionBlurReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async _ =>
        {
            await IonBlur.InvokeAsync();
        }));
        
        _ionChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new (async args =>
        {
            var isChecked = args?["detail"]?["checked"]?.GetValue<bool?>();
            var value = args?["detail"]?["value"]?.GetValue<string?>();

            Checked = isChecked is true;
            Value = value;
            
            await IonChange.InvokeAsync(new IonCheckBoxChangeEventArgs { Checked = isChecked, Value = value });
        }));

        _ionFocusReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new (async _ =>
        {
            await IonFocus.InvokeAsync();
        }));
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        //await JsRuntime.InvokeVoidAsync("attachIonEventListener", "ionBlur", _self, _ionBlurReference);
        //await JsRuntime.InvokeVoidAsync("attachIonEventListener", "ionChange", _self, _ionChangeReference);
        //await JsRuntime.InvokeVoidAsync("attachIonEventListener", "ionFocus", _self, _ionFocusReference);
        
        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new []
        {
            new { Event = "ionBlur", Ref = _ionBlurReference},
            new { Event = "ionChange", Ref = _ionChangeReference},
            new { Event = "ionFocus", Ref = _ionFocusReference}
        }, _self);
    }
    
}

public class IonCheckBoxChangeEventArgs : EventArgs
{
    public bool? Checked { get; internal set; }
    
    /// <summary>
    /// The value of the <see cref="IonCheckBox"/> does not mean if it's checked or not,
    /// use the <see cref="Checked"/> property for that.
    /// <br/><br/>
    /// The value of a <see cref="IonCheckBox"/> is analogous to the value of an &lt;input type="checkbox"&gt;,
    /// it's only used when the checkbox participates in a native &lt;form&gt;.
    /// </summary>
    public string? Value { get; internal set; }
}