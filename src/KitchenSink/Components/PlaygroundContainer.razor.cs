namespace IonicTest.Components;

public partial class PlaygroundContainer : ComponentBase
{
    private PlaygroundConsole? _console;

    [Parameter]
    [Obsolete("", true)]
    public string? Title { get; set; }

    [Parameter]
    [Obsolete("", true)]
    public string? SubTitle { get; set; }

    private PlaygroundConsole? Console
    {
        get => _console;
        set
        {
            _console = value;
            StateHasChanged();
        }
    }

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; init; } = null!;

    [Parameter] public bool EnableConsole { get; init; } = false;
}