namespace IonBlazor.Components;

public sealed partial class IonFabList : IonContentComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the fab list will show all fab buttons in the list.
    /// Default: <b>false</b>
    /// </summary>
    [Parameter]
    public bool Activated { get; set; }

    /// <summary>
    /// The side the fab list will show on relative to the main fab button.
    /// Default: <see cref="IonFabListSide.Bottom"/>
    /// </summary>
    [Parameter]
    public string? Side { get; set; } = IonFabListSide.Default;
}