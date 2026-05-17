namespace IonBlazor.UnitTests.Components;

public class IonLabelTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonLabelRendersCorrectly()
    {
        var cut = Render<IonLabel>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonLabel>(parameters => parameters
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

        var cut = Render<IonLabel>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonLabelPosition.Fixed)]
    [InlineData(IonLabelPosition.Floating)]
    [InlineData(IonLabelPosition.Stacked)]
    public async Task WithPosition_RendersCorrectly(string position)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"position={position}");

        var cut = Render<IonLabel>(parameters => parameters
            .Add(p => p.Position, position));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonLabel>(parameters => parameters
            .AddChildContent("Label text"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonLabel>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "label" }
            }));

        await Verify(cut.Markup);
    }
}
