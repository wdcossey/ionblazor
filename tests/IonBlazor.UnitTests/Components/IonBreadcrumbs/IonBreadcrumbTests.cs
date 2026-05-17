namespace IonBlazor.UnitTests.Components;

public class IonBreadcrumbTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonBreadcrumbRendersCorrectly()
    {
        var cut = Render<IonBreadcrumb>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonBreadcrumb>(parameters => parameters
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

        var cut = Render<IonBreadcrumb>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithActive_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonBreadcrumb>(parameters => parameters
            .Add(p => p.Active, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonBreadcrumb>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithSeparator_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonBreadcrumb>(parameters => parameters
            .Add(p => p.Separator, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithHref_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumb>(parameters => parameters
            .Add(p => p.Href, "/home"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumb>(parameters => parameters
            .AddChildContent("Home"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumb>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "breadcrumb" }
            }));

        await Verify(cut.Markup);
    }
}
