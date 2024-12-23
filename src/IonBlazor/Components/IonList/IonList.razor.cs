namespace IonBlazor.Components;

public partial class IonList : IonContentComponent, IIonModeComponent
{
    protected ElementReference _self;
    private Func<ValueTask<bool>> _closeSlidingItemsWrapper = null!;

    protected override string JsImportName => nameof(IonList);

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// If <b>true</b>, the list will have margin around it and rounded corners.
    /// </summary>
    [Parameter]
    public bool? Inset { get; set; }

    /// <summary>
    /// How the bottom border should be displayed on all items.
    /// </summary>
    [Parameter]
    public string? Lines { get; set; } = IonListLines.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If ion-item-sliding are used inside the list, this method closes any open sliding item.
    /// Returns true if an actual ion-item-sliding is closed.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> CloseSlidingItemsAsync() => _closeSlidingItemsWrapper();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _closeSlidingItemsWrapper = () => JsComponent.InvokeAsync<bool>("closeSlidingItems", _self);
    }

    public override async ValueTask DisposeAsync()
    {
        _closeSlidingItemsWrapper = null!;
        await base.DisposeAsync();
    }
}