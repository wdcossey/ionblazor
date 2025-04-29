namespace IonBlazor.Components;

public sealed partial class IonCheckbox : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the <see cref="IonCheckbox"/> is selected.
    /// </summary>
    [Parameter]
    public bool? Checked { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the <see cref="IonCheckbox"/>.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// If <b>true</b>, the <see cref="IonCheckbox"/> will visually appear as indeterminate.
    /// </summary>
    [Parameter]
    public bool? Indeterminate { get; set; }

    /// <summary>
    /// How to pack the label and <see cref="IonCheckbox"/> within a line.<br/><br/>
    /// <see cref="IonJustify.Start"/>: The label and <see cref="IonCheckbox"/> will appear on the left in LTR
    /// and on the right in RTL.<br/><br/>
    /// <see cref="IonJustify.End"/>: The label and <see cref="IonCheckbox"/> will appear on the right in LTR
    /// and on the left in RTL.<br/><br/>
    /// <see cref="IonJustify.SpaceBetween"/>: The label and <see cref="IonCheckbox"/> will appear on opposite
    /// ends of the line with space between the two elements.
    /// </summary>
    [Parameter]
    public string? Justify { get; set; } = IonJustify.Default;

    /// <summary>
    /// Where to place the label relative to the checkbox.<br/><br/>
    /// <see cref="IonLabelPlacement.Start"/>: The label will appear to the left of the <see cref="IonCheckbox"/> in LTR
    /// and to the right in RTL.<br/><br/>
    /// <see cref="IonLabelPlacement.End"/>: The label will appear to the right of the <see cref="IonCheckbox"/> in LTR
    /// and to the left in RTL.<br/><br/>
    /// <see cref="IonLabelPlacement.Fixed"/>: The label has the same behavior as "start" except it also
    /// has a fixed width.
    /// <br/><br/>
    /// Long text will be truncated with ellipses ("...").
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
    public string Name { get; set; } = null!;

    /// <summary>
    /// The value of the <see cref="IonCheckbox"/> does not mean if it's checked or not,
    /// use the <see cref="Checked"/> property for that.
    /// <br/><br/>
    /// The value of a <see cref="IonCheckbox"/> is analogous to the value of an &lt;input type="checkbox"&gt;,
    /// it's only used when the checkbox participates in a native &lt;form&gt;.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

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
    public EventCallback<IonCheckboxChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonToggle"/> has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }

    public IonCheckbox()
    {
        _ionBlurReference = IonicEventCallback.Create(async () => await IonBlur.InvokeAsync());

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var isChecked = args?["detail"]?["checked"]?.GetValue<bool?>();
            var value = args?["detail"]?["value"]?.GetValue<string?>();

            Checked = isChecked is true;
            Value = value;

            await IonChange.InvokeAsync(new IonCheckboxChangeEventArgs
            {
                Sender = this,
                Checked = isChecked,
                Value = value
            });
        });

        _ionFocusReference = IonicEventCallback.Create(async () => await IonFocus.InvokeAsync());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;


        await this.AttachIonListenersAsync(
            _self,
            IonEvent.Set("ionBlur", _ionBlurReference),
            IonEvent.Set("ionChange", _ionChangeReference),
            IonEvent.Set("ionFocus", _ionFocusReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionChangeReference.Dispose();
        _ionFocusReference.Dispose();
        await base.DisposeAsync();
    }
}