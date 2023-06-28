namespace IonicSharp.Components;

public partial class IonButtons : IonComponent, IIonContentComponent
{
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, buttons will disappear when its parent toolbar has fully collapsed if the toolbar is not the
    /// first toolbar. If the toolbar is the first toolbar, the buttons will be hidden and will only be shown once
    /// all toolbars have fully collapsed.
    /// <br/><br/>
    /// Only applies in ios mode with collapse set to true on ion-header.
    /// <br/><br/>
    /// Typically used for Collapsible Large Titles
    /// </summary>
    [Parameter]
    public bool Collapse { get; set; }

}