using Microsoft.JSInterop;
using NSubstitute;

namespace IonBlazor.UnitTests.Components;

public class IonAccordionGroupTests: BunitContext
{

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
    public void IonAccordionGroupRendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>();

        // Assert
        cut.MarkupMatches("<ion-accordion-group></ion-accordion-group>");
    }

    [Fact]
    public void WhenAnimated_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Animated, true));

        // Assert
        cut.MarkupMatches("<ion-accordion-group animated></ion-accordion-group>");
    }

    [Fact]
    public void WhenDisabled_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Disabled, true));

        // Assert
        cut.MarkupMatches("<ion-accordion-group disabled></ion-accordion-group>");
    }

    [Theory]
    [InlineData(IonAccordionGroupExpand.Compact)]
    [InlineData(IonAccordionGroupExpand.Inset)]
    public void WithExpand_RendersCorrectly(string expand)
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Expand, expand));

        // Assert
        cut.MarkupMatches($"<ion-accordion-group expand=\"{expand}\"></ion-accordion-group>");
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public void WithMode_RendersCorrectly(string mode)
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Mode, mode));

        // Assert
        cut.MarkupMatches($"<ion-accordion-group  mode=\"{mode}\"></ion-accordion-group >");
    }

    [Fact]
    public void WhenMultiple_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Multiple, true));

        // Assert
        cut.MarkupMatches("<ion-accordion-group multiple></ion-accordion-group>");
    }

    [Fact]
    public void WhenReadonly_RendersCorrectly()
    {
        // Act
        var cut = Render<IonAccordionGroup>(parameters => parameters
            .Add(p => p.Readonly, true));

        // Assert
        cut.MarkupMatches("<ion-accordion-group readonly></ion-accordion-group>");
    }

    [Fact]
    public void WithChildContent_RendersCorrectly()
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
        cut.MarkupMatches(
            """
            <ion-accordion-group>
                <ion-accordion value="first"></ion-accordion>
                <ion-accordion value="second"></ion-accordion>
                <ion-accordion value="third"></ion-accordion>
            </ion-accordion-group>
            """);
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