namespace IonicTest.Components;

public partial class PlaygroundContainer : ComponentBase
{
    [Parameter]
    public bool? FillWidth { get; init; }

    private PlaygroundConsole? Console
    {
        get;
        set
        {
            field = value;
            StateHasChanged();
        }
    }

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; init; } = null!;

    [Parameter] public bool EnableConsole { get; init; } = false;
}