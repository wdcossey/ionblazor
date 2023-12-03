namespace IonicTest.Components;

public partial class PreviewContainer : ComponentBase
{
    [Parameter]
    public string? Title { get; set; }
    
    [Parameter]
    public string? SubTitle { get; set; }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;
}