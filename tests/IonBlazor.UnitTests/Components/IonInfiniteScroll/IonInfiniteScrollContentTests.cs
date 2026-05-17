namespace IonBlazor.UnitTests.Components;

public class IonInfiniteScrollContentTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonInfiniteScrollContentRendersCorrectly()
    {
        var cut = Render<IonInfiniteScrollContent>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithLoadingText_RendersCorrectly()
    {
        var cut = Render<IonInfiniteScrollContent>(parameters => parameters
            .Add(p => p.LoadingText, "Loading more..."));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonInfiniteScrollContent>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "infinite-scroll-content" }
            }));

        await Verify(cut.Markup);
    }
}
