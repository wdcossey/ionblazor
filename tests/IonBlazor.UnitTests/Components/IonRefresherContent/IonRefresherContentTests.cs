namespace IonBlazor.UnitTests.Components;

public class IonRefresherContentTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonRefresherContentRendersCorrectly()
    {
        var cut = Render<IonRefresherContent>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithPullingText_RendersCorrectly()
    {
        var cut = Render<IonRefresherContent>(parameters => parameters
            .Add(p => p.PullingText, "Pull to refresh"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithRefreshingText_RendersCorrectly()
    {
        var cut = Render<IonRefresherContent>(parameters => parameters
            .Add(p => p.RefreshingText, "Refreshing..."));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonRefresherContent>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "refresher-content" }
            }));

        await Verify(cut.Markup);
    }
}
