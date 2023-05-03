using Microsoft.AspNetCore.Components.Rendering;

namespace IonicTest.Pages.Samples.IonList;

public partial class IonListSample
{
    private readonly IEnumerable<ListOfItem> _itemsSource = new ListOfItem[]
    {
        new() { Title = "Pokémon Yellow" },
        new() { Title = "Mega Man X" },
        new() { Title = "The Legend of Zelda" },
        new() { Title = "Pac-Man" },
        new() { Title = "Super Mario World" },
    };

    public class ListOfItem
    {
        public string Title { get; set; }
    }
}