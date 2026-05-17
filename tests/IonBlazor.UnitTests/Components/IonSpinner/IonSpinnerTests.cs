namespace IonBlazor.UnitTests.Components;

public class IonSpinnerTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSpinnerRendersCorrectly()
    {
        var cut = Render<IonSpinner>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonColor.Primary)]
    [InlineData(IonColor.Secondary)]
    [InlineData(IonColor.Danger)]
    public async Task WithColor_RendersCorrectly(string color)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"color={color}");

        var cut = Render<IonSpinner>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonSpinnerName.Circular)]
    [InlineData(IonSpinnerName.Dots)]
    [InlineData(IonSpinnerName.Lines)]
    public async Task WithName_RendersCorrectly(string name)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"name={name}");

        var cut = Render<IonSpinner>(parameters => parameters
            .Add(p => p.Name, name));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithPaused_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSpinner>(parameters => parameters
            .Add(p => p.Paused, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSpinner>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "spinner" }
            }));

        await Verify(cut.Markup);
    }
}
