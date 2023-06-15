namespace IonicSharp.Components;

public partial class IonImg : IonComponent
{
    /// <summary>
    /// This attribute defines the alternative text describing the image.
    /// Users will see this text displayed if the image URL is wrong,
    /// the image is not in one of the supported formats, or if the image is not yet downloaded.
    /// </summary>
    [Parameter] public string? Alt { get; set; }
    
    /// <summary>
    /// The image URL. This attribute is mandatory for the &lt;img&gt; element.
    /// </summary>
    [Parameter, EditorRequired] public string? Src { get; set; }
}