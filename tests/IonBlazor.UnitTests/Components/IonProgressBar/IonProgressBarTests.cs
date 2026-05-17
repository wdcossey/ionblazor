namespace IonBlazor.UnitTests.Components;

public class IonProgressBarTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonProgressBarRendersCorrectly()
    {
        var cut = Render<IonProgressBar>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonColor.Primary)]
    [InlineData(IonColor.Secondary)]
    [InlineData(IonColor.Danger)]
    public async Task WithColor_RendersCorrectly(string color)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"color={color}");

        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithReversed_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Reversed, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonProgressBarType.Determinate)]
    [InlineData(IonProgressBarType.Indeterminate)]
    public async Task WithType_RendersCorrectly(string type)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"type={type}");

        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Type, type));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithValue_RendersCorrectly()
    {
        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Value, 0.5d));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithBuffer_RendersCorrectly()
    {
        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Buffer, 0.75d));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonProgressBar>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "progress-bar" }
            }));

        await Verify(cut.Markup);
    }
}
