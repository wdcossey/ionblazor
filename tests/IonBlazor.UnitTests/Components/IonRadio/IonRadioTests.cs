namespace IonBlazor.UnitTests.Components;

public class IonRadioTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonRadioRendersCorrectly()
    {
        var cut = Render<IonRadio>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonRadio>(parameters => parameters
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

        var cut = Render<IonRadio>(parameters => parameters
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

        var cut = Render<IonRadio>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonJustify.Start)]
    [InlineData(IonJustify.End)]
    [InlineData(IonJustify.SpaceBetween)]
    public async Task WithJustify_RendersCorrectly(string justify)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"justify={justify}");

        var cut = Render<IonRadio>(parameters => parameters
            .Add(p => p.Justify, justify));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonLabelPlacement.Start)]
    [InlineData(IonLabelPlacement.End)]
    [InlineData(IonLabelPlacement.Fixed)]
    public async Task WithLabelPlacement_RendersCorrectly(string labelPlacement)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"labelPlacement={labelPlacement}");

        var cut = Render<IonRadio>(parameters => parameters
            .Add(p => p.LabelPlacement, labelPlacement));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithValue_RendersCorrectly()
    {
        var cut = Render<IonRadio>(parameters => parameters
            .Add(p => p.Value, "option-1"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonRadio>(parameters => parameters
            .AddChildContent("Radio Label"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonRadio>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "radio" }
            }));

        await Verify(cut.Markup);
    }
}
