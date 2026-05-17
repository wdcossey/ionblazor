namespace IonBlazor.UnitTests.Components;

public class IonCardHeaderTests : IonTestContext
{
    [Fact]
    public async Task IonCardHeaderRendersCorrectly()
    {
        // Act
        var cut = Render<IonCardHeader>();

        // Assert
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

        // Act
        var cut = Render<IonCardHeader>(parameters => parameters
            .Add(p => p.Color, color));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        // Act
        var cut = Render<IonCardHeader>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithTranslucent_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonCardHeader>(parameters => parameters
            .Add(p => p.Translucent, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCardHeader>(parameters => parameters
            .AddChildContent("<ion-card-title>Title</ion-card-title>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCardHeader>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "card-header" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
