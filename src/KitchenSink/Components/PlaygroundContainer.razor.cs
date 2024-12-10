namespace IonicTest.Components;

public partial class PlaygroundContainer : ComponentBase
{
    [Parameter]
    //[Obsolete("", true)]
    public string? Title { get; set; }

    [Parameter]
    //[Obsolete("", true)]
    public string? SubTitle { get; set; }

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;
}