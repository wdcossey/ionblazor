using System.Runtime.CompilerServices;
using Microsoft.JSInterop;
using NSubstitute;

namespace IonBlazor.UnitTests.Components;

public class IonAccordionGroupTests: BunitContext
{
    public static class StaticSettingsUsage
    {
        [ModuleInitializer]
        public static void Initialize() =>
            VerifierSettings.ScrubLinesWithReplace(
                replaceLine: line =>
                {
                    if (!line.Contains(" blazor:elementReference="))
                    {
                        return line;
                    }

                    var index = line.IndexOf(" blazor:elementReference=", StringComparison.Ordinal);
                    return line.Remove(index, 25 + 2 + 36);
                });
    }

    public IonAccordionGroupTests()
    {
        JSInterop
            .SetupModule("./_content/IonBlazor/common.js")
            .SetupVoid("attachListeners", _ => true);

        JSInterop
            .SetupModule("./_content/IonBlazor/ionAccordionGroup.js")
            .SetupVoid("setValue", _ => true);
    }

    [Fact]
    public async Task IonAccordionGroupRendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>();

        // Assert
        await Verify(cut.Markup)
            .IgnoreParameters("blazor:elementReference");
    }

    [Fact]
    public async Task WhenAnimated_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Animated, true));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WhenDisabled_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Disabled, true));

        // Assert
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonAccordionGroupExpand.Compact)]
    [InlineData(IonAccordionGroupExpand.Inset)]
    public async Task WithExpand_RendersCorrectly(string expand)
    {
        VerifySettings settings = new ();
        settings.UseTextForParameters($"expand={expand}");

        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Expand, expand));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new ();
        settings.UseTextForParameters($"mode={mode}");

        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WhenMultiple_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Multiple, true));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WhenReadonly_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Readonly, true));

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .AddChildContent(
                """
                <ion-accordion value="first"></ion-accordion>
                <ion-accordion value="second"></ion-accordion>
                <ion-accordion value="third"></ion-accordion>
                """)
            );

        // Assert
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task SetValueAsync_WithNull_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        IJSObjectReference? jsComponent = Substitute.For<IJSObjectReference>();

        var cut = Render<IonAccordionGroup>();

        cut.Instance.JsComponent = new Lazy<Task<IJSObjectReference>>(() => Task.FromResult(jsComponent));

        // Act
        await cut.Instance.SetValueAsync(null);

        // Assert
        await jsComponent.Received(1).InvokeVoidAsync("setValue", Arg.Is<object[]>(result => result.Length == 2 && result[0].Equals(cut.Instance.IonElement) && result[1] == null!));
    }

    [Fact]
    public async Task SetValueAsync_WithSingleValue_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        IJSObjectReference? jsComponent = Substitute.For<IJSObjectReference>();

        var cut = Render<IonAccordionGroup>();

        cut.Instance.JsComponent = new Lazy<Task<IJSObjectReference>>(() => Task.FromResult(jsComponent));

        // Act
        await cut.Instance.SetValueAsync("second");

        // Assert
        await jsComponent.Received(1).InvokeVoidAsync("setValue", Arg.Is<object[]>(result => result.Length == 2 && result[0].Equals(cut.Instance.IonElement) && result[1].Equals("second")));
    }

    [Fact]
    public async Task SetValueAsync_WithMultipleValues_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        IJSObjectReference? jsComponent = Substitute.For<IJSObjectReference>();

        var cut = Render<IonAccordionGroup>();

        cut.Instance.JsComponent = new Lazy<Task<IJSObjectReference>>(() => Task.FromResult(jsComponent));

        // Act
        await cut.Instance.SetValueAsync("first", "second");

        // Assert
        await jsComponent.Received(1).InvokeVoidAsync("setValue", Arg.Is<object[]>(result => result.Length == 2 && result[0].Equals(cut.Instance.IonElement) && result[1] is string[] && result[1] is string[]));
    }

    [Fact]
    public async Task GetValueAsync_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        IJSObjectReference? jsComponent = Substitute.For<IJSObjectReference>();

        var cut = Render<IonAccordionGroup>();

        cut.Instance.JsComponent = new Lazy<Task<IJSObjectReference>>(() => Task.FromResult(jsComponent));

        // Act
        await cut.Instance.GetValueAsync();

        // Assert
        await jsComponent.Received(1).InvokeAsync<IEnumerable<string>>("getValue", Arg.Is<object[]>(result => result.Length == 1 && result[0].Equals(cut.Instance.IonElement)));
    }

    [Fact]
    public async Task Value_InvokesJsMethod_WhenCalled()
    {
        // Arrange
        IJSObjectReference? jsComponent = Substitute.For<IJSObjectReference>();

        var cut = Render<IonAccordionGroup>();

        cut.Instance.JsComponent = new Lazy<Task<IJSObjectReference>>(() => Task.FromResult(jsComponent));

        // Act
        _ = await cut.Instance.Value;

        // Assert
        await jsComponent.Received(1).InvokeAsync<IEnumerable<string>>("getValue", Arg.Is<object[]>(result => result.Length == 1 && result[0].Equals(cut.Instance.IonElement)));
    }
}