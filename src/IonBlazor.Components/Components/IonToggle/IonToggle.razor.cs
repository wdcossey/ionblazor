namespace IonBlazor.Components;

public sealed partial class IonToggle : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;

    protected override string JsImportName => nameof(IonToggle);

    /// <summary>
    /// If true, the toggle is selected.
    /// </summary>
    [Parameter]
    public bool? Checked { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; }

    /// <summary>
    /// If true, the user cannot interact with the toggle.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; init; }

    /// <summary>
    /// Enables the on/off accessibility switch labels within the toggle.
    /// </summary>
    [Parameter]
    public bool? EnableOnOffLabels { get; init; }

    /// <summary>
    /// How to pack the label and toggle within a line.<br/><br/>
    /// <see cref="IonJustify.Start"/>: The label and <see cref="IonToggle"/> will appear on the left in LTR
    /// and on the right in RTL.<br/><br/>
    /// <see cref="IonJustify.End"/>: The label and <see cref="IonToggle"/> will appear on the right in LTR
    /// and on the left in RTL.<br/><br/>
    /// <see cref="IonJustify.SpaceBetween"/>: The label and <see cref="IonToggle"/> will appear on opposite
    /// ends of the line with space between the two element.
    /// </summary>
    [Parameter]
    public string? Justify { get; set; } = IonJustify.Default;

    /// <summary>
    /// Where to place the label relative to the input.<br/>
    /// <see cref="IonLabelPlacement.Start"/>: The label will appear to the left of the toggle in LTR
    /// and to the right in RTL.<br/>
    /// <see cref="IonLabelPlacement.End"/>: The label will appear to the right of the toggle in LTR
    /// and to the left in RTL.<br/>
    /// <see cref="IonLabelPlacement.Fixed"/>: The label has the same behavior as <see cref="IonLabelPlacement.Start"/>
    /// except it also has a fixed width. Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter]
    public string? LabelPlacement { get; set; } = IonLabelPlacement.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; init; }

    /// <summary>
    /// The value of the toggle does not mean if it's checked or not, use the checked property for that. <br/><br/>
    /// The value of a toggle is analogous to the value of a &lt;input type="checkbox"&gt;,
    /// it's only used when the toggle participates in a native &lt;form&gt;.
    /// </summary>
    [Parameter]
    public string? Value { get; init; }

    /// <summary>
    /// Emitted when the <see cref="IonToggle"/> loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }

    /// <summary>
    /// Emitted when the user switches the toggle on or off.
    /// Does not emit when programmatically changing the value of the checked property.
    /// </summary>
    [Parameter]
    public EventCallback<IonToggleChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonToggle"/> has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }

    public IonToggle()
    {
        _ionBlurReference = IonicEventCallback.Create(async () =>
        {
            await IonBlur.InvokeAsync();
        });

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var isChecked = args?["detail"]?["checked"]?.GetValue<bool?>();
            var value = args?["detail"]?["value"]?.GetValue<string?>();
            await IonChange.InvokeAsync(new IonToggleChangeEventArgs { Sender = this, Checked = isChecked, Value = value });
        });

        _ionFocusReference = IonicEventCallback.Create(async () =>
        {
            await IonFocus.InvokeAsync();
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement, new []
        {
            IonEvent.Set("ionBlur"  , _ionBlurReference  ),
            IonEvent.Set("ionChange", _ionChangeReference),
            IonEvent.Set("ionFocus" , _ionFocusReference ),
        });
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionChangeReference.Dispose();
        _ionFocusReference.Dispose();
        await base.DisposeAsync();
    }

    public ValueTask SetChecked(bool value)
        => JsComponent.InvokeVoidAsync("setChecked", IonElement, value);
}