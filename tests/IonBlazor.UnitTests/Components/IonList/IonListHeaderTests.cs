namespace IonBlazor.UnitTests.Components;

public class IonListHeaderTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonListHeaderRendersCorrectly()
    {
        var cut = Render<IonListHeader>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonListHeader>(parameters => parameters
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

        var cut = Render<IonListHeader>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonListHeaderLines.Full)]
    [InlineData(IonListHeaderLines.Inset)]
    [InlineData(IonListHeaderLines.None)]
    public async Task WithLines_RendersCorrectly(string lines)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"lines={lines}");

        var cut = Render<IonListHeader>(parameters => parameters
            .Add(p => p.Lines, lines));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonListHeader>(parameters => parameters
            .AddChildContent("<ion-label>List Header</ion-label>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonListHeader>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "list-header" }
            }));

        await Verify(cut.Markup);
    }
}
