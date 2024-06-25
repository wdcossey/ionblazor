namespace IonicTest.Pages.Samples.Searchbar;

public partial class Searchbar
{
    private IonSearchbar _ionSearchbarInputElementRef = null!;
    private IonSearchbar _ionSearchbarSetFocusRef = null!;
    private string? inputElementMessage;
    private string focusMessage = null!;

    private async Task SetFocusAsync()
    {
        await _ionSearchbarSetFocusRef.SetFocusAsync();
    }
}
