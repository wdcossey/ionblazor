namespace IonicSharp.Components;

public partial class IonRadio : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback>? _ionFocusReference;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the radio.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; } = false;

    /// <summary>
    /// How to pack the label and radio  within a line.<br/><br/>
    /// <see cref="IonJustify.Start"/>: The label and <see cref="IonRadio"/> will appear on the left in LTR
    /// and on the right in RTL.<br/><br/>
    /// <see cref="IonJustify.End"/>: The label and <see cref="IonRadio"/> will appear on the right in LTR
    /// and on the left in RTL.<br/><br/>
    /// <see cref="IonJustify.SpaceBetween"/>: The label and <see cref="IonRadio"/> will appear on opposite
    /// ends of the line with space between the two element.
    /// </summary>
    [Parameter]
    public string Justify { get; set; } = IonJustify.SpaceBetween;

    /// <summary>
    /// Where to place the label relative to the radio.<br/><br/>
    /// <see cref="IonLabelPlacement.Start"/>: The label will appear to the left of the <see cref="IonRadio"/> in LTR
    /// and to the right in RTL.<br/><br/>
    /// <see cref="IonLabelPlacement.End"/>: The label will appear to the right of the <see cref="IonRadio"/> in LTR
    /// and to the left in RTL.<br/><br/>
    /// <see cref="IonLabelPlacement.Fixed"/>: The label has the same behavior as "start" except it also
    /// has a fixed width.<br/><br/>
    /// Long text will be truncated with ellipses ("...").
    /// </summary>
    [Parameter]
    public string LabelPlacement { get; set; } = IonLabelPlacement.Start;

    /// <summary>
    /// Set the legacy property to true to forcibly use the legacy form control markup.
    /// Ionic will only opt components in to the modern form markup when they are using either the aria-label attribute
    /// or the default slot that contains the label text.
    /// As a result, the legacy property should only be used as an escape hatch when you want to avoid this automatic
    /// opt-in behavior.
    /// <br/><br/>
    /// Note that this property will be removed in an upcoming major release of Ionic,
    /// and all form components will be opted-in to using the modern form markup.
    /// </summary>
    [Parameter, Obsolete("Note that this property will be removed in an upcoming major release of Ionic")]
    public bool? Legacy { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// the value of the radio.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonRadio"/> loses focus.
    /// </summary>
    [Parameter]
    public EventCallback IonBlur { get; set; }

    /// <summary>
    /// Emitted when the <see cref="IonRadio"/> has focus.
    /// </summary>
    [Parameter]
    public EventCallback IonFocus { get; set; }

    public IonRadio()
    {
        _ionBlurReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonBlur.InvokeAsync();
        }));

        _ionFocusReference = DotNetObjectReference.Create<IonicEventCallback>(new(async () =>
        {
            await IonFocus.InvokeAsync();
        }));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await JsRuntime.InvokeVoidAsync("IonicSharp.attachListeners", new[]
        {
            new { Event = "ionBlur", Ref = _ionBlurReference },
            new { Event = "ionFocus", Ref = _ionFocusReference },
        }, _self);
    }
}