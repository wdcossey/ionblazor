namespace IonicTest.Pages.Samples.SearchBar;

public partial class SearchBar
{
    private IonSearchBar ionSearchBarInputElementRef = null!;
    private IonSearchBar ionSearchBarSetFocusRef = null!;
    private string? inputElementMessage;
    private string focusMessage = null!;

    private async Task GetInputElementAsync()
    {
        var element = await ionSearchBarInputElementRef.GetInputElementAsync(); 
        inputElementMessage = element?.ToString();
    }

    private async Task SetFocusAsync()
    {
        await ionSearchBarSetFocusRef.SetFocusAsync();
    }
}