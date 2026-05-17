namespace IonBlazor.UnitTests.Components;

public class IonReorderTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonReorderRendersCorrectly()
    {
        var cut = Render<IonReorder>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonReorder>(parameters => parameters
            .AddChildContent("<ion-icon name=\"menu\"></ion-icon>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonReorder>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "reorder" }
            }));

        await Verify(cut.Markup);
    }
}
