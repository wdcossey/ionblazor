using Microsoft.AspNetCore.Components;

namespace IonicTest.Components;

public partial class IonButton : ComponentBase
{
    [Parameter] public string? ButtonType { get; set; } = "button";
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? Download { get; set; }
    [Parameter] public ButtonExpand? Expand { get; set; }
    [Parameter] public ButtonFill? Fill { get; set; }
    [Parameter] public string? Form { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public ButtonMode? Mode { get; set; }
    [Parameter] public string? Rel { get; set; }
    //[Parameter] public string? routerAnimation { get; set; }
    //[Parameter] public string? routerDirection { get; set; }
    [Parameter] public ButtonShape? Shape { get; set; }
    [Parameter] public ButtonSize? Size { get; set; }
    [Parameter] public bool? Strong { get; set; }
    [Parameter] public string? Target { get; set; }
    [Parameter] public ButtonType Type { get; set; } = Components.ButtonType.Button;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
}

public enum ButtonMode 
{
    // ReSharper disable once InconsistentNaming
    iOS,
    Md
}

public enum ButtonExpand {
    Block,
    Full
}

public enum ButtonFill 
{

    Clear, 
    Default,
    Outline,
    Solid
}

public enum ButtonShape 
{
    Round
}

public enum ButtonSize 
{
    Default,
    Large,
    Small
}

public enum ButtonType 
{
    Button,
    Reset,
    Submit
}