namespace IonicSharp.Components;

public partial class IonRefresherContent: IonControl
{
    /// <summary>
    /// A static icon or a spinner to display when you begin to pull down.
    /// A spinner name can be provided to gradually show tick marks when pulling down on iOS devices.
    /// </summary>
    [Parameter] public string? PullingIcon { get; set; }
    
    /// <summary>
    /// The text you want to display when you begin to pull down. pullingText can accept either
    /// plaintext or HTML as a string. To display characters normally reserved for HTML, they must be escaped.
    /// For example &lt;Ionic&gt; would become &amp;lt;Ionic&amp;gt;
    /// <br/><br/>
    /// For more information: Security Documentation
    /// <br/><br/>
    /// Content is parsed as plaintext by default. innerHTMLTemplatesEnabled must be set to true in the
    /// Ionic config before custom HTML can be used.
    /// </summary>
    [Parameter] public string? PullingText { get; set; }
    
    /// <summary>
    /// An animated SVG spinner that shows when refreshing begins<br/>
    /// See <see cref="IonSpinnerName"/>
    /// </summary>
    [Parameter] public string? RefreshingSpinner { get; set; }
    
    /// <summary>
    /// The text you want to display when performing a refresh. refreshingText can accept either
    /// plaintext or HTML as a string. To display characters normally reserved for HTML, they must be escaped.
    /// For example &lt;Ionic&gt; would become &amp;lt;Ionic&amp;gt;
    /// <br/><br/>
    /// For more information: Security Documentation
    /// <br/><br/>
    /// Content is parsed as plaintext by default. innerHTMLTemplatesEnabled must be set to true in the
    /// Ionic config before custom HTML can be used.
    /// </summary>
    [Parameter] public string? RefreshingText { get; set; }
    
    
}