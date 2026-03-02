namespace IonBlazor.UnitTests.Components;

public class IonBackdropTests : IonTestContext
{
    [Fact]
    public async Task IonBackdropRendersCorrectly()
    {
        // Act
        var cut = Render<IonBackdrop>();

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithStopPropagation_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonBackdrop>(parameters => parameters
            .Add(p => p.StopPropagation, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithTappable_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonBackdrop>(parameters => parameters
            .Add(p => p.Tappable, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithVisible_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        // Act
        var cut = Render<IonBackdrop>(parameters => parameters
            .Add(p => p.Visible, value));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonBackdrop>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "backdrop" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
