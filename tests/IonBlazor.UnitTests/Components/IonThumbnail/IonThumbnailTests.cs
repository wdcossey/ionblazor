namespace IonBlazor.UnitTests.Components;

public class IonThumbnailTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonThumbnailRendersCorrectly()
    {
        var cut = Render<IonThumbnail>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonThumbnail>(parameters => parameters
            .AddChildContent("<ion-img src=\"thumbnail.png\"></ion-img>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonThumbnail>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "thumbnail" }
            }));

        await Verify(cut.Markup);
    }
}
