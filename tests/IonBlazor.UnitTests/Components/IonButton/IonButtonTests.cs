namespace IonBlazor.UnitTests.Components;

public class IonButtonTests : IonTestContext
{
    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonButtonRendersCorrectly()
    {
        var cut = Render<IonButton>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonButton>(parameters => parameters
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

        var cut = Render<IonButton>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonButton>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonButtonExpand.Block)]
    [InlineData(IonButtonExpand.Full)]
    public async Task WithExpand_RendersCorrectly(string expand)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"expand={expand}");

        var cut = Render<IonButton>(parameters => parameters
            .Add(p => p.Expand, expand));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonButtonFill.Clear)]
    [InlineData(IonButtonFill.Outline)]
    [InlineData(IonButtonFill.Solid)]
    public async Task WithFill_RendersCorrectly(string fill)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"fill={fill}");

        var cut = Render<IonButton>(parameters => parameters
            .Add(p => p.Fill, fill));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithStrong_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonButton>(parameters => parameters
            .Add(p => p.Strong, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonButton>(parameters => parameters
            .AddChildContent("Click me"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonButton>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "button" }
            }));

        await Verify(cut.Markup);
    }
}
