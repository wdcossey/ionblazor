using FluentAssertions;
using IonBlazor.UnitTests.TestHelpers;

namespace IonBlazor.UnitTests.Components;

public class IonAccordionTests: BunitContext
{
    [Fact]
    public async Task IonAccordionRendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>();

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WhenDisabled_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Disabled, true));

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WhenReadonly_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Readonly, true));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithToggleIcon_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.ToggleIcon, "icon"));

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonAccordionToggleIconSlot.Start)]
    [InlineData(IonAccordionToggleIconSlot.End)]
    public async Task WithToggleIconSlot_RendersCorrectly(string slot)
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.ToggleIconSlot, slot));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithValue_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Value, "value"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "accordion" }
            }));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(builder => builder.AddChildContent("<p>Child content</p>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithParent_RendersCorrectly()
    {
        // Arrange
        IonTestComponent testComponent = IonTestComponent.Create();

        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Parent, testComponent));

        // Assert
        cut.Instance.Parent.Should().Be(testComponent);
    }

}