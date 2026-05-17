namespace IonBlazor.UnitTests.Components;

public class IonInputPasswordToggleTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonInputPasswordToggleRendersCorrectly()
    {
        var cut = Render<IonInputPasswordToggle>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonInputPasswordToggle>(parameters => parameters
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

        var cut = Render<IonInputPasswordToggle>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithHideIcon_RendersCorrectly()
    {
        var cut = Render<IonInputPasswordToggle>(parameters => parameters
            .Add(p => p.HideIcon, "eye-off-outline"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithShowIcon_RendersCorrectly()
    {
        var cut = Render<IonInputPasswordToggle>(parameters => parameters
            .Add(p => p.ShowIcon, "eye-outline"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonInputPasswordToggle>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "password-toggle" }
            }));

        await Verify(cut.Markup);
    }
}
