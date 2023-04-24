namespace IonicTest.Components;

public partial class IonFab : IonSlotControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If <b>true</b>, both the ion-fab-button and all ion-fab-list inside <see cref="IonFab"/> will become active.
    /// That means ion-fab-button will become a close icon and ion-fab-list will become visible.
    /// </summary>
    [Parameter] public bool Activated { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the fab will display on the edge of the header if vertical is
    /// <see cref="IonVerticalAlignment.Top"/>, and on the edge of the footer if it is
    /// <see cref="IonVerticalAlignment.Bottom"/>.
    /// Should be used with a fixed slot.
    /// </summary>
    [Parameter] public bool Edge { get; set; }
    
    /// <summary>
    /// Where to align the fab horizontally in the viewport.<br/>
    /// See <see cref="IonHorizontalAlignment"/> for options
    /// </summary>
    [Parameter] public string? Horizontal { get; set; } = IonHorizontalAlignment.Default;
    
    /// <summary>
    /// Where to align the fab vertically in the viewport.<br/>
    /// See <see cref="IonVerticalAlignment"/> for options
    /// </summary>
    [Parameter] public string? Vertical { get; set; } = IonVerticalAlignment.Default;

    /// <summary>
    /// Close an active FAB list container.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task CloseAsync()
    {
        throw new NotImplementedException();
    }
}

public static class IonHorizontalAlignment
{
    public const string? Default = null;
    public const string Center = "center";
    public const string End = "end";
    public const string Start = "start";
}

public static class IonVerticalAlignment
{
    public const string? Default = null;
    public const string Bottom = "bottom";
    public const string Center = "center";
    public const string Top = "top";
}