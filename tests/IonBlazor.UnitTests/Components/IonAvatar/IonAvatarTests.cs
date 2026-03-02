namespace IonBlazor.UnitTests.Components;

public class IonAvatarTests : IonTestContext
{
    [Fact]
    public async Task IonAvatarRendersCorrectly()
    {
        // Act
        var cut = Render<IonAvatar>();

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAvatar>(parameters => parameters
            .AddChildContent("<ion-img src=\"avatar.png\"></ion-img>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAvatar>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "avatar" }
            }));

        // Assert
        await Verify(cut.Markup);
    }
}
