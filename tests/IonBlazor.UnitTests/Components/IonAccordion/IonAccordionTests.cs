namespace IonBlazor.UnitTests.Components;

public class IonAccordionTests: BunitContext
{
    [Fact]
    public void IonAccordionRendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>();

        // Assert
        cut.MarkupMatches("<ion-accordion></ion-accordion>");
    }

    [Fact]
    public void WhenDisabled_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Disabled, true));

        // Assert
        cut.MarkupMatches("<ion-accordion disabled=\"true\"></ion-accordion>");
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public void WithMode_RendersCorrectly(string mode)
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        cut.MarkupMatches($"<ion-accordion mode=\"{mode}\"></ion-accordion>");
    }

    [Fact]
    public void WhenReadonly_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Readonly, true));

        // Assert
        cut.MarkupMatches("<ion-accordion readonly=\"true\"></ion-accordion>");
    }

    [Fact]
    public void WithToggleIcon_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.ToggleIcon, "icon"));

        // Assert
        cut.MarkupMatches("<ion-accordion toggle-icon=\"icon\"></ion-accordion>");
    }

    [Theory]
    [InlineData(IonAccordionToggleIconSlot.Start)]
    [InlineData(IonAccordionToggleIconSlot.End)]
    public void WithToggleIconSlot_RendersCorrectly(string slot)
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.ToggleIconSlot, slot));

        // Assert
        cut.MarkupMatches($"<ion-accordion toggle-icon-slot=\"{slot}\"></ion-accordion>");
    }

    [Fact]
    public void WithValue_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Value, "value"));

        // Assert
        cut.MarkupMatches("<ion-accordion value=\"value\"></ion-accordion>");
    }

    [Fact]
    public void WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "accordion" }
            }));

        // Assert
        cut.MarkupMatches("<ion-accordion id=\"accordion\"></ion-accordion>");
    }

    [Fact]
    public void WithClass_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(parameters => parameters
            .Add(p => p.Class, "class"));

        // Assert
        cut.MarkupMatches("<ion-accordion class=\"class\"></ion-accordion>");
    }

    [Fact]
    public void WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordion>(builder => builder.AddChildContent("<p>Child content</p>"));

        // Assert
        cut.MarkupMatches("<ion-accordion><p>Child content</p></ion-accordion>");
    }


}