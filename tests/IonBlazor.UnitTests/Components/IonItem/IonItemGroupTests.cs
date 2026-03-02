namespace IonBlazor.UnitTests.Components;

public class IonItemGroupTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonItemGroupRendersCorrectly()
    {
        var cut = Render<IonItemGroup>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonItemGroup>(parameters => parameters
            .AddChildContent("<ion-item><ion-label>Item</ion-label></ion-item>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonItemGroup>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "item-group" }
            }));

        await Verify(cut.Markup);
    }
}
