using System.Dynamic;

namespace IonicSharp.Components;

public partial class IonBreadcrumbs : IonComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionCollapsedClickReference = null;

    [Parameter] public RenderFragment? ChildContent { get; set; }
    

    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }

    /// <summary>
    /// The number of breadcrumbs to show after the collapsed indicator.
    /// If itemsBeforeCollapse + itemsAfterCollapse is greater than maxItems,
    /// the breadcrumbs will not be collapsed.
    /// </summary>
    [Parameter] public int? ItemsAfterCollapse { get; set; }
    
    /// <summary>
    /// The number of breadcrumbs to show before the collapsed indicator.
    /// If itemsBeforeCollapse + itemsAfterCollapse is greater than maxItems,
    /// the breadcrumbs will not be collapsed.
    /// </summary>
    [Parameter] public int? ItemsBeforeCollapse { get; set; }
    
    /// <summary>
    /// The maximum number of breadcrumbs to show before collapsing.
    /// </summary>
    [Parameter] public int? MaxItems { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// Emitted when the collapsed indicator is clicked on.
    /// </summary>
    [Parameter] public EventCallback<IDictionary<string, string?>> IonCollapsedClick { get; set; }

    public IonBreadcrumbs()
    {
        
        _ionCollapsedClickReference = DotNetObjectReference.Create(new IonicEventCallback<JsonObject?>(async args =>
        {
            IDictionary<string, string?> value = new Dictionary<string, string?>();
            
            foreach (var node in args?["detail"]?.AsArray())
            {
                var href = node?["href"]?.GetValue<string>();
                var textContent = node?["textContent"]?.GetValue<string>()!;
                
                value.Add(textContent, href);
            }
            await IonCollapsedClick.InvokeAsync(value);
        }));
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await JsRuntime.InvokeVoidAsync("attachIonBreadcrumbsIonCollapsedClickListener", "ionCollapsedClick", _self, _ionCollapsedClickReference);
    }

}
