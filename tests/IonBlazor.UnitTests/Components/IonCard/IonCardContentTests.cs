namespace IonBlazor.UnitTests.Components;

public class IonCardContentTests : IonTestContext
{
    [Fact]
    public async Task IonCardContentRendersCorrectly()
    {
        // Act
        var cut = Render<IonCardContent>();

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        // Act
        var cut = Render<IonCardContent>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCardContent>(parameters => parameters
            .AddChildContent("<p>Card content</p>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCardContent>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "card-content" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
