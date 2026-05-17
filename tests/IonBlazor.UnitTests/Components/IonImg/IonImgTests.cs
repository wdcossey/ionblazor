namespace IonBlazor.UnitTests.Components;

public class IonImgTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonImgRendersCorrectly()
    {
        var cut = Render<IonImg>(parameters => parameters
            .Add(p => p.Src, "image.png"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAlt_RendersCorrectly()
    {
        var cut = Render<IonImg>(parameters => parameters
            .Add(p => p.Src, "image.png")
            .Add(p => p.Alt, "A descriptive alt text"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonImg>(parameters => parameters
            .Add(p => p.Src, "image.png")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "img" }
            }));

        await Verify(cut.Markup);
    }
}
