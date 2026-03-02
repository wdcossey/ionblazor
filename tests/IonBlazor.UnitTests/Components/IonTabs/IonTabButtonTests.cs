namespace IonBlazor.UnitTests.Components;

public class IonTabButtonTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonTabButtonRendersCorrectly()
    {
        var cut = Render<IonTabButton>(parameters => parameters
            .Add(p => p.Tab, "home"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonTabButton>(parameters => parameters
            .Add(p => p.Tab, "home")
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

        var cut = Render<IonTabButton>(parameters => parameters
            .Add(p => p.Tab, "home")
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithHref_RendersCorrectly()
    {
        var cut = Render<IonTabButton>(parameters => parameters
            .Add(p => p.Tab, "home")
            .Add(p => p.Href, "/home"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonTabButton>(parameters => parameters
            .Add(p => p.Tab, "home")
            .AddChildContent("<ion-label>Home</ion-label>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonTabButton>(parameters => parameters
            .Add(p => p.Tab, "home")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "tab-button" }
            }));

        await Verify(cut.Markup);
    }
}
