namespace IonBlazor.Components;

public sealed partial class IonRadio : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionFocusReference;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; }

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
        _ionBlurReference = IonicEventCallback.Create(async () => await IonBlur.InvokeAsync());

        _ionFocusReference = IonicEventCallback.Create(async () => await IonFocus.InvokeAsync());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionBlur", _ionBlurReference),
            IonEvent.Set("ionFocus", _ionFocusReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBlurReference.Dispose();
        _ionFocusReference.Dispose();
        await base.DisposeAsync();
    }
}