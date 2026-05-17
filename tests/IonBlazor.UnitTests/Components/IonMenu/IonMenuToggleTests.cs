namespace IonBlazor.UnitTests.Components;

public class IonMenuToggleTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonMenuToggleRendersCorrectly()
    {
        var cut = Render<IonMenuToggle>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithAutoHide_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonMenuToggle>(parameters => parameters
            .Add(p => p.AutoHide, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithMenu_RendersCorrectly()
    {
        var cut = Render<IonMenuToggle>(parameters => parameters
            .Add(p => p.Menu, "main-menu"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonMenuToggle>(parameters => parameters
            .AddChildContent("<ion-button>Toggle</ion-button>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonMenuToggle>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "menu-toggle" }
            }));

        await Verify(cut.Markup);
    }
}
