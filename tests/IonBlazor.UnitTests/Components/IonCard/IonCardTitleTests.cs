namespace IonBlazor.UnitTests.Components;

public class IonCardTitleTests : IonTestContext
{
    [Fact]
    public async Task IonCardTitleRendersCorrectly()
    {
        // Act
        var cut = Render<IonCardTitle>();

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
        var cut = Render<IonCardTitle>(parameters => parameters
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
        var cut = Render<IonCardTitle>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCardTitle>(parameters => parameters
            .AddChildContent("Card Title"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCardTitle>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "card-title" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
