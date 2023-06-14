namespace IonicSharp.Components;

public partial class IonFabList : IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If <b>true</b>, the fab list will show all fab buttons in the list.
    /// Default: <b>false</b>
    /// </summary>
    [Parameter] public bool Activated { get; set; }

    /// <summary>
    /// The side the fab list will show on relative to the main fab button.
    /// Default: <see cref="IonSide.Bottom"/>
    /// </summary>
    [Parameter]
    public string Side { get; set; } = IonSide.Bottom;
}

public static class IonSide
{
    public const string Bottom = "bottom";
    public const string End = "end";
    public const string Start = "start";
    public const string Top = "top";
}
