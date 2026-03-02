namespace IonBlazor.UnitTests.Components;

public class IonItemOptionsTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonItemOptionsRendersCorrectly()
    {
        var cut = Render<IonItemOptions>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonItemOptionsSide.Start)]
    [InlineData(IonItemOptionsSide.End)]
    public async Task WithSide_RendersCorrectly(string side)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"side={side}");

        var cut = Render<IonItemOptions>(parameters => parameters
            .Add(p => p.Side, side));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonItemOptions>(parameters => parameters
            .AddChildContent("<ion-item-option>Delete</ion-item-option>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonItemOptions>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "item-options" }
            }));

        await Verify(cut.Markup);
    }
}
