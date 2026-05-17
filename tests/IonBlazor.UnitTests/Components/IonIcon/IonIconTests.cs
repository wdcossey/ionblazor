namespace IonBlazor.UnitTests.Components;

public class IonIconTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonIconRendersCorrectly()
    {
        var cut = Render<IonIcon>();
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

        var cut = Render<IonIcon>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithName_RendersCorrectly()
    {
        var cut = Render<IonIcon>(parameters => parameters
            .Add(p => p.Name, "add"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonIconSize.Small)]
    [InlineData(IonIconSize.Large)]
    [InlineData(IonIconSize.Default)]
    public async Task WithSize_RendersCorrectly(string size)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"size={size}");

        var cut = Render<IonIcon>(parameters => parameters
            .Add(p => p.Size, size));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonIcon>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "icon" }
            }));

        await Verify(cut.Markup);
    }
}
