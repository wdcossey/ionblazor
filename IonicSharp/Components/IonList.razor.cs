namespace IonicSharp.Components;

public partial class IonList : IonComponent, IIonModeComponent, IIonContentComponent
{
    protected ElementReference Self;

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
    public async ValueTask<bool> CloseSlidingItemsAsync()
    {
        return await JsRuntime.InvokeAsync<bool>("IonicSharp.IonList.closeSlidingItems", Self);
    }
}


public static class IonListLines
{
    public const string? Default = null;
    public const string Full = "full";
    public const string Inset = "inset";
    public const string None = "none";
}