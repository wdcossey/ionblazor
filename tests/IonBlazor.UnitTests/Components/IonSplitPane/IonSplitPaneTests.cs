namespace IonBlazor.UnitTests.Components;

public class IonSplitPaneTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSplitPaneRendersCorrectly()
    {
        var cut = Render<IonSplitPane>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSplitPane>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithContentId_RendersCorrectly()
    {
        var cut = Render<IonSplitPane>(parameters => parameters
            .Add(p => p.ContentId, "main-content"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonSplitPane>(parameters => parameters
            .AddChildContent("<ion-menu content-id=\"main-content\"></ion-menu>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSplitPane>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "split-pane" }
            }));

        await Verify(cut.Markup);
    }
}
