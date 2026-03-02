namespace IonBlazor.UnitTests.Components;

public class IonColTests : IonTestContext
{
    [Fact]
    public async Task IonColRendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>();

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithSize_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>(parameters => parameters
            .Add(p => p.Size, "6"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithOffset_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>(parameters => parameters
            .Add(p => p.Offset, "2"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithPull_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>(parameters => parameters
            .Add(p => p.Pull, "3"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithPush_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>(parameters => parameters
            .Add(p => p.Push, "3"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>(parameters => parameters
            .AddChildContent("<p>Column content</p>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonCol>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "col" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
