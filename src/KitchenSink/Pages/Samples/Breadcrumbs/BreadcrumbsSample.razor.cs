namespace IonicTest.Pages.Samples.Breadcrumbs;

public partial class BreadcrumbsSample
{
    IonBreadcrumbs _breadcrumbsPopover = null!;
    IonPopover _popover = null!;
    RenderFragment _popoverContent = null!;

    private async Task IonCollapsedClickPopover(IDictionary<string, string?> args)
    {
        _popoverContent = builder =>
        {
            //var listHtml = string.Empty;

            var i = 0;
            foreach (var (href, textContent) in args)
            {
                /*listHtml +=
                    $$"""
                    <ion-item {{(i == args.Count - 1 ? "lines=\"none\"" : string.Empty)}} href="{{href}}">
                        <ion-label>{{textContent}}</ion-label>
                    </ion-item>
                    """;*/

                builder.OpenRegion(i);
                builder.OpenComponent<IonItem>(0);
                builder.AddAttribute(1, nameof(IonItem.Href), href);
                builder.AddAttribute(2, "lines", i == args.Count - 1 ? "none" : null);
                builder.AddAttribute(3, nameof(IonItem.ChildContent), (RenderFragment)(childBuilder =>
                {
                    childBuilder.OpenComponent<IonLabel>(0);
                    childBuilder.AddAttribute(1, nameof(IonLabel.ChildContent), (RenderFragment)(labelContent =>
                    {
                        labelContent.AddContent(0, textContent);
                    }));
                    childBuilder.CloseComponent();
                }));

                builder.CloseComponent();
                builder.CloseRegion();

                i++;
            }

            //builder.AddMarkupContent(0, listHtml);
        };

        //_popover.event = e;
        await _popover.PresentAsync();
    }


    private void PopoverDidDismiss()
    {
        _popover.IsOpen = true;// SetIsOpen(false);
    }
}