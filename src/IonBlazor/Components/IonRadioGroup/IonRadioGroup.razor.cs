namespace IonBlazor.Components;

public sealed partial class IonRadioGroup : IonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<__ionChangeEventArgs>> _ionChangeReference;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the radios can be deselected.
    /// </summary>
    [Parameter]
    public bool AllowEmptySelection { get; set; }

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// the value of the radio group.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Emitted when the value has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonRadioGroupIonChangeEventArgs> IonChange { get; set; }

    public IonRadioGroup()
    {
        _ionChangeReference = IonicEventCallback<__ionChangeEventArgs>.Create(async args =>
        {
            await IonChange.InvokeAsync(args.Detail with { Sender = this });
        });

    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionChange", _ionChangeReference));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionChangeReference.Dispose();
        await base.DisposeAsync();
    }

    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    internal sealed record __ionChangeEventArgs
    {
        [JsonPropertyName("detail")] public IonRadioGroupIonChangeEventArgs Detail { get; set; } = null!;
    }

    // ReSharper enable InconsistentNaming
    // ReSharper enable ClassNeverInstantiated.Global
}