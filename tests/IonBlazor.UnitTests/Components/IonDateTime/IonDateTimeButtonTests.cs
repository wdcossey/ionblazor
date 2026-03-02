namespace IonBlazor.UnitTests.Components;

public class IonDateTimeButtonTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonDateTimeButtonRendersCorrectly()
    {
        var cut = Render<IonDateTimeButton>(parameters => parameters
            .Add(p => p.DateTime, "datetime-id"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonDateTimeButton>(parameters => parameters
            .Add(p => p.DateTime, "datetime-id")
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

        var cut = Render<IonDateTimeButton>(parameters => parameters
            .Add(p => p.DateTime, "datetime-id")
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonDateTimeButton>(parameters => parameters
            .Add(p => p.DateTime, "datetime-id")
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonDateTimeButton>(parameters => parameters
            .Add(p => p.DateTime, "datetime-id")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "datetime-button" }
            }));

        await Verify(cut.Markup);
    }
}
