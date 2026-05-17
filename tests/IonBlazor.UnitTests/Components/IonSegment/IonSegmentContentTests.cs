namespace IonBlazor.UnitTests.Components;

public class IonSegmentContentTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSegmentContentRendersCorrectly()
    {
        var cut = Render<IonSegmentContent>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonSegmentContent>(parameters => parameters
            .AddChildContent("<p>Segment content</p>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSegmentContent>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "segment-content" }
            }));

        await Verify(cut.Markup);
    }
}
