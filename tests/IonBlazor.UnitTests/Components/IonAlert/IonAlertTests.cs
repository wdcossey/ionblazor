namespace IonBlazor.UnitTests.Components;

public class IonAlertTests: BunitContext
{
    public IonAlertTests()
    {
        JSInterop
            .SetupModule("./_content/IonBlazor/common.js")
            .SetupVoid("attachListeners", _ => true);

        JSInterop
            .SetupModule("./_content/IonBlazor/ionAlert.js")
            .SetupVoid("setValue", _ => true);
    }

    [Fact]
    public async Task IonAlertRendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>();

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public async Task WithAnimated_RendersCorrectly(bool isAnimated, string expected)
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Animated, isAnimated));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAlert>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "accordion" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}