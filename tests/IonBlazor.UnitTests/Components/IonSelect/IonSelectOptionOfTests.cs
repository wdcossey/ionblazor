namespace IonBlazor.UnitTests.Components;

public class IonSelectOptionOfTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSelectOptionOfRendersCorrectly()
    {
        var cut = Render<IonSelectOptionOf>(parameters => parameters
            .Add(p => p.Value, 1));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSelectOptionOf>(parameters => parameters
            .Add(p => p.Value, 1)
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonSelectOptionOf>(parameters => parameters
            .Add(p => p.Value, 1)
            .AddChildContent("Option 1"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSelectOptionOf>(parameters => parameters
            .Add(p => p.Value, 1)
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "select-option" }
            }));

        await Verify(cut.Markup);
    }
}
