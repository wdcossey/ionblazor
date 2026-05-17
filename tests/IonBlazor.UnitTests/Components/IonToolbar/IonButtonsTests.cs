namespace IonBlazor.UnitTests.Components;

public class IonButtonsTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonButtonsRendersCorrectly()
    {
        var cut = Render<IonButtons>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithCollapse_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonButtons>(parameters => parameters
            .Add(p => p.Collapse, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonButtons>(parameters => parameters
            .AddChildContent("<ion-back-button></ion-back-button>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonButtons>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "buttons" }
            }));

        await Verify(cut.Markup);
    }
}
