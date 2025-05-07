namespace IonicTest.Pages.Samples.Breadcrumbs;

public partial class BreadcrumbsSample
{
    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    private IonBreadcrumbs _breadcrumbsPopover = null!;
    private IonPopover _popover = null!;
    private RenderFragment _popoverContent = null!;

    private async Task IonCollapsedClickPopover(IDictionary<string, string?> args)
    {
        _popoverContent = builder =>
        {
            var i = 0;
            foreach (var (href, textContent) in args)
            {
                builder.OpenRegion(i);
                builder.OpenComponent<IonItem>(0);
                builder.AddAttribute(1, nameof(IonItem.Href), $"{NavigationManager.Uri}/{href}");
                builder.AddAttribute(2, nameof(IonItem.Lines), i == args.Count - 1 ? IonItemLines.None : null);
                builder.AddAttribute(3, nameof(IonItem.ChildContent), (RenderFragment)(childBuilder =>
                {
                    childBuilder.OpenComponent<IonLabel>(0);
                    childBuilder.AddAttribute(1, nameof(IonLabel.ChildContent), (RenderFragment)(labelContent =>
                    {
                        labelContent.AddContent(0, textContent);
                    }));
                    childBuilder.CloseComponent();
                }));
                builder.AddAttribute(4, "onclick", EventCallback.Factory.Create(this, DismissPopover));

                builder.CloseComponent();
                builder.CloseRegion();

                i++;
            }
        };

        await _popover.PresentAsync();
    }


    private void DismissPopover()
    {
        _ = _popover.DismissAsync().AsTask();
    }
}