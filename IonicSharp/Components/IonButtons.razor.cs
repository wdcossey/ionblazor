namespace IonicSharp.Components;

public partial class IonButtons: IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Buttons can be positioned inside of the toolbar using a named slot.<br/>
    /// See <see cref="IonButtonsSlot"/> for a description of each slot.
    /// </summary>
    [Parameter] public string? Slot { get; set; } = IonButtonsSlot.Default;
    
    /// <summary>
    /// If <b>true</b>, buttons will disappear when its parent toolbar has fully collapsed if the toolbar is not the
    /// first toolbar. If the toolbar is the first toolbar, the buttons will be hidden and will only be shown once
    /// all toolbars have fully collapsed.
    /// <br/><br/>
    /// Only applies in ios mode with collapse set to true on ion-header.
    /// <br/><br/>
    /// Typically used for Collapsible Large Titles
    /// </summary>
    [Parameter] public bool Collapse { get; set; }
    
}

public static class IonButtonsSlot
{
    public const string? Default = null;
    
    /// <summary>
    /// Positions to the <b>left</b> of the content in LTR, and to the <b>right</b> in RTL.
    /// </summary>
    public const string Start = "start";
    
    /// <summary>
    /// Positions to the <b>right</b> of the content in LTR, and to the <b>left</b> in RTL.
    /// </summary>
    public const string End = "end";
    
    /// <summary>
    /// Positions element to the <b>left</b> of the content in <b>ios</b> mode, and directly to the <b>right</b> in <b>md</b> mode.
    /// </summary>
    public const string Secondary = "secondary";
    
    /// <summary>
    /// Positions element to the <b>right</b> of the content in <b>ios</b> mode, and to the far <b>right</b> in <b>md</b> mode.
    /// </summary>
    public const string Primary = "primary";
}