namespace IonBlazor.UnitTests.Components;

public class IonSelectOptionTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSelectOptionRendersCorrectly()
    {
        var cut = Render<IonSelectOption>(parameters => parameters
            .Add(p => p.Value, "option-a"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonSelectOption>(parameters => parameters
            .Add(p => p.Value, "option-a")
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonSelectOption>(parameters => parameters
            .Add(p => p.Value, "option-a")
            .AddChildContent("Option A"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSelectOption>(parameters => parameters
            .Add(p => p.Value, "option-a")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "select-option" }
            }));

        await Verify(cut.Markup);
    }
}
