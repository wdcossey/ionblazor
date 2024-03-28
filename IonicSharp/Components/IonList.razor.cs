using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonList : IonComponent, IIonModeComponent, IIonContentComponent
{
    protected ElementReference Self;
    private Func<ValueTask<bool>> _closeSlidingItemsWrapper = null!;
    
    public override ElementReference IonElement => Self;
    
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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
        
        var ionComponent = await JsRuntime.ImportAsync("ionList");
        _closeSlidingItemsWrapper = () => ionComponent.InvokeAsync<bool>("closeSlidingItems", Self);
    }
}


public static class IonListLines
{
    public const string? Default = null;
    public const string Full = "full";
    public const string Inset = "inset";
    public const string None = "none";
}