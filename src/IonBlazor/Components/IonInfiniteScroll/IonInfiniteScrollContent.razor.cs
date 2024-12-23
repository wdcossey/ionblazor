namespace IonBlazor.Components;

public sealed partial class IonInfiniteScrollContent : IonComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// An animated SVG spinner that shows while loading.
    /// </summary>
    [Parameter]
    public string? LoadingSpinner { get; set; } = LoadingSpinnerType.Default;

    /// <summary>
    /// Optional text to display while loading. <see cref="LoadingText"/> can accept either plaintext or HTML as a
    /// string. To display characters normally reserved for HTML, they must be escaped. For example
    /// <b>&lt;Ionic&gt;</b> would become <b>&amp;lt;Ionic&amp;gt;</b>
    /// <p/>
    /// For more information: <a href="https://ionicframework.com/docs/faq/security">Security Documentation</a>
    /// <p/>
    /// This property accepts custom HTML as a string. Content is parsed as plaintext by default.
    /// <b>innerHTMLTemplatesEnabled</b> must be set to <b>true</b> in the Ionic config before custom HTML can be used.
    /// </summary>
    [Parameter]
    public string? LoadingText { get; set; }
}