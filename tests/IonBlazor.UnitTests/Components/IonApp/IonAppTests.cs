using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonAppTests : IonTestContext
{
    public IonAppTests()
    {
        SetupComponentModule<IonApp>(module =>
        {
            module.SetupVoid("setFocus", _ => true).SetVoidResult();
        });
    }

    [Fact]
    public async Task IonAppRendersCorrectly()
    {
        // Act
        var cut = Render<IonApp>();

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
        var cut = Render<IonApp>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        cut.Instance.Mode.Should().Be(mode);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonApp>(parameters => parameters
            .AddChildContent("<p>Child content</p>"));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        // Act
        var cut = Render<IonApp>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "app" }
            }));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task SetFocusAsync_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        var cut = Render<IonApp>();

        // Act
        await cut.Instance.SetFocusAsync();

        // Assert
        JSRuntimeInvocation invocation = JSInterop.Invocations["setFocus"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public void Assert_JsImportName()
    {
        // Arrange
        var cut = Render<IonApp>();

        // Assert
        Assert.Equal(nameof(IonApp), cut.Instance.JsImportName);
    }
}