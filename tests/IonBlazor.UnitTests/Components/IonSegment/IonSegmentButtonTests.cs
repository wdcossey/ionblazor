namespace IonBlazor.UnitTests.Components;

public class IonSegmentButtonTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSegmentButtonRendersCorrectly()
    {
        var cut = Render<IonSegmentButton>(parameters => parameters
            .Add(p => p.Value, "segment-a"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonSegmentButton>(parameters => parameters
            .Add(p => p.Value, "segment-a")
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSegmentButton>(parameters => parameters
            .Add(p => p.Value, "segment-a")
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonSegmentButton>(parameters => parameters
            .Add(p => p.Value, "segment-a")
            .AddChildContent("<ion-label>Option A</ion-label>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSegmentButton>(parameters => parameters
            .Add(p => p.Value, "segment-a")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "segment-button" }
            }));

        await Verify(cut.Markup);
    }
}
