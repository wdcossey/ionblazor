namespace IonBlazor.UnitTests.Components;

public class IonSkeletonTextTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSkeletonTextRendersCorrectly()
    {
        var cut = Render<IonSkeletonText>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithAnimated_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSkeletonText>(parameters => parameters
            .Add(p => p.Animated, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSkeletonText>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "skeleton-text" }
            }));

        await Verify(cut.Markup);
    }
}
