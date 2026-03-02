namespace IonBlazor.UnitTests.Components;

public class IonFabTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonFabRendersCorrectly()
    {
        var cut = Render<IonFab>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithActivated_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonFab>(parameters => parameters
            .Add(p => p.Activated, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithEdge_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonFab>(parameters => parameters
            .Add(p => p.Edge, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonFabHorizontalAlignment.Start)]
    [InlineData(IonFabHorizontalAlignment.Center)]
    [InlineData(IonFabHorizontalAlignment.End)]
    public async Task WithHorizontal_RendersCorrectly(string horizontal)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"horizontal={horizontal}");

        var cut = Render<IonFab>(parameters => parameters
            .Add(p => p.Horizontal, horizontal));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonFabVerticalAlignment.Top)]
    [InlineData(IonFabVerticalAlignment.Center)]
    [InlineData(IonFabVerticalAlignment.Bottom)]
    public async Task WithVertical_RendersCorrectly(string vertical)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"vertical={vertical}");

        var cut = Render<IonFab>(parameters => parameters
            .Add(p => p.Vertical, vertical));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonFab>(parameters => parameters
            .AddChildContent("<ion-fab-button>+</ion-fab-button>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonFab>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "fab" }
            }));

        await Verify(cut.Markup);
    }
}
