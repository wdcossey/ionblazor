namespace IonBlazor.UnitTests.Components;

public class IonToolbarTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonToolbarRendersCorrectly()
    {
        var cut = Render<IonToolbar>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonToolbar>(parameters => parameters
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

        var cut = Render<IonToolbar>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonToolbar>(parameters => parameters
            .AddChildContent("<ion-title>My Title</ion-title>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonToolbar>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "toolbar" }
            }));

        await Verify(cut.Markup);
    }
}
