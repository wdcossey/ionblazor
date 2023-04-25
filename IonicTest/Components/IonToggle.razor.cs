using System.Text.Json.Nodes;

namespace IonicTest.Components;

public partial class IonToggle : IonControl
{
    private ElementReference _self;
    
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionBlurReference = null;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionChangeReference = null;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionFocusReference = null;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If true, the toggle is selected.
    /// </summary>
    [Parameter] public bool Checked { get; set; }

    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <p/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If true, the user cannot interact with the toggle.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// Enables the on/off accessibility switch labels within the toggle.
    /// </summary>
    [Parameter] public bool? EnableOnOffLabels { get; set; }

    /// <summary>
    /// How to pack the label and toggle within a line.<br/><br/>
    /// <see cref="IonJustify.Start"/>: The label and <see cref="IonToggle"/> will appear on the left in LTR
    /// and on the right in RTL.<br/><br/>
    /// <see cref="IonJustify.End"/>: The label and <see cref="IonToggle"/> will appear on the right in LTR
    /// and on the left in RTL.<br/><br/>
    /// <see cref="IonJustify.SpaceBetween"/>: The label and <see cref="IonToggle"/> will appear on opposite
    /// ends of the line with space between the two element.
    /// </summary>
    [Parameter] public string Justify { get; set; } = IonJustify.SpaceBetween;

    /// <summary>
    /// Where to place the label relative to the input.<br/>
    /// <see cref="IonLabelPlacement.Start"/>: The label will appear to the left of the toggle in LTR
    /// and to the right in RTL.<br/>
    /// <see cref="IonLabelPlacement.End"/>: The label will appear to the right of the toggle in LTR
    /// and to the left in RTL.<br/>
    /// <see cref="IonLabelPlacement.Fixed"/>: The label has the same behavior as <see cref="IonLabelPlacement.Start"/>
    /// except it also has a fixed width. Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter] public string LabelPlacement { get; set; } = IonLabelPlacement.Fixed;

    /// <summary>
    /// Set the legacy property to true to forcibly use the legacy form control markup.
    /// Ionic will only opt components in to the modern form markup when they are using either the aria-label
    /// attribute or the default slot that contains the label text.
    /// As a result, the legacy property should only be used as an escape hatch when you want to avoid this
    /// automatic opt-in behavior.<br/><br/>
    /// Note that this property will be removed in an upcoming major release of Ionic,
    /// and all form components will be opted-in to using the modern form markup.
    /// </summary>
    [Parameter, Obsolete("Note that this property will be removed in an upcoming major release of Ionic")] 
    public bool? Legacy { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter] public string Name { get; set; } = null!;

    /// <summary>
    /// The value of the toggle does not mean if it's checked or not, use the checked property for that. <br/><br/>
    /// The value of a toggle is analogous to the value of a &lt;input type="checkbox"&gt;,
    /// it's only used when the toggle participates in a native &lt;form&gt;.
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
    [Parameter] public EventCallback<IonToggleChangeEventArgs> IonChange { get; set; }
    
    /// <summary>
    /// Emitted when the <see cref="IonToggle"/> has focus.
    /// </summary>
    [Parameter] public EventCallback IonFocus { get; set; }
    
    public IonToggle()
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
            
            await IonChange.InvokeAsync(new IonToggleChangeEventArgs { Checked = isChecked, Value = value });
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

public static class IonJustify
{
    public const string End = "end";
    public const string SpaceBetween = "space-between";
    public const string Start = "start";
}

public static class IonLabelPlacement
{
    public const string End = "end";
    public const string Fixed = "fixed";
    public const string Start = "start";
}

public class IonToggleChangeEventArgs : EventArgs
{
    public bool? Checked { get; internal set; }
    
    /// <summary>
    /// The value of the <see cref="IonToggle"/> does not mean if it's checked or not,
    /// use the <see cref="Checked"/> property for that. <p/>
    /// The value of a <see cref="IonToggle"/> is analogous to the value of a &lt;input type="checkbox"&gt;,
    /// it's only used when the <see cref="IonToggle"/> participates in a native &lt;form&gt;.
    /// </summary>
    public string? Value { get; internal set; }
}