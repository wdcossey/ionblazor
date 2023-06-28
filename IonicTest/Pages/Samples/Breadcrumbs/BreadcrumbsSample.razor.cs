namespace IonicTest.Pages.Samples.Breadcrumbs;

public partial class BreadcrumbsSample
{
    IonBreadcrumbs _breadcrumbsPopover = null!;
    IonPopover _popover;
    RenderFragment _popoverContent;

    private void IonCollapsedClickPopover(IDictionary<string, string?> args)
    {
        _popoverContent = builder =>
        {
            var listHtml = string.Empty;

            var i = 0;
            foreach (var (k, v) in args)
            {
                listHtml += 
                    $$"""
                    <ion-item {{(i == args.Count - 1 ? "lines=\"none\"" : string.Empty)}} href="{{v}}">
                        <ion-label>{{k}}</ion-label>
                    </ion-item>
                    """;
                i++;

                /*builder.OpenComponent<IonItem>(0);
                  builder.AddAttribute(1, nameof(IonItem.Href), v);
                  builder.OpenComponent<IonLabel>(2);
                  builder.AddContent(3, k);
                  builder.CloseComponent();
                  builder.CloseComponent();
                */
            }
            
            builder.AddMarkupContent(0, listHtml);
        };

        //_popoverList.innerHTML = listHTML;
        //_popover.event = e;
        _popover.SetIsOpen(true);
    }


    private void PopoverDidDismiss()
    {
        _popover.SetIsOpen(false);
    }
}