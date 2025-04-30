namespace IonicTest.Pages.Samples.Searchbar;

public partial class Searchbar
{
    private IonSearchbar _ionSearchbarSetFocusRef = null!;
    private string _focusMessage = null!;

    private async Task SetFocusAsync()
    {
        await _ionSearchbarSetFocusRef.SetFocusAsync();
    }
}
