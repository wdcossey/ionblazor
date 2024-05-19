namespace IonBlazor.Components;

public partial class IonFabList : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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

public static class IonFabListSide
{
    public const string? Default = null;
    public const string Bottom = "bottom";
    public const string End = "end";
    public const string Start = "start";
    public const string Top = "top";
}
