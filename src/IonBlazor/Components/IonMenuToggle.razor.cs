namespace IonBlazor.Components;

public partial class IonMenuToggle : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
    
    /// <summary>
    /// Automatically hides the content when the corresponding menu is not active.
    /// <br/><br/>
    /// By default, it's <b>true</b>. Change it to <b>false</b> in order to keep <see cref="IonMenuToggle"/> always
    /// visible regardless the state of the menu.
    /// </summary>
    [Parameter]
    public bool? AutoHide { get; set; }
    
    /// <summary>
    /// Optional property that maps to a Menu's <see cref="IonMenu.MenuId"/> prop. Can also be <b>start</b> or <b>end</b> for the menu
    /// side. This is used to find the correct menu to toggle.
    /// <br/><br/>
    /// If this property is not used, <see cref="IonMenuToggle"/> will toggle the first menu that is active.
    /// </summary>
    [Parameter]
    public string? Menu { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}