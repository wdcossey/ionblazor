namespace IonBlazor.UnitTests.Components;

public class IonFabListTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonFabListRendersCorrectly()
    {
        var cut = Render<IonFabList>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithActivated_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonFabList>(parameters => parameters
            .Add(p => p.Activated, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonFabListSide.Start)]
    [InlineData(IonFabListSide.End)]
    [InlineData(IonFabListSide.Top)]
    [InlineData(IonFabListSide.Bottom)]
    public async Task WithSide_RendersCorrectly(string side)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"side={side}");

        var cut = Render<IonFabList>(parameters => parameters
            .Add(p => p.Side, side));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonFabList>(parameters => parameters
            .AddChildContent("<ion-fab-button>+</ion-fab-button>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonFabList>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "fab-list" }
            }));

        await Verify(cut.Markup);
    }
}
