namespace IonicTest.Pages.Samples.Refresher;

public partial class RefresherSample
{
    
    /* Yes some of the methods below are identical, they are purely for demonstration purposes */
    
    private async Task BasicIonRefreshCallback(IonRefresherIonRefreshEventArgs args)
    {
        await Task.Delay(2000);
        await args.Sender.CompleteAsync();
    }
    
    private async Task PullPropertiesIonRefreshCallback(IonRefresherIonRefreshEventArgs args)
    {
        await Task.Delay(2000);
        await args.Sender.CompleteAsync();
    }
    
    private async Task CustomContentIonRefreshCallback(IonRefresherIonRefreshEventArgs args)
    {
        await Task.Delay(2000);
        await args.Sender.CompleteAsync();
    }
    
    private async Task VirtualScrollIonRefreshCallback(IonRefresherIonRefreshEventArgs args)
    {
        await Task.Delay(2000);
        await args.Sender.CompleteAsync();
    }
    
    #region Advanced Usage

    private static readonly string[] Names = { "Burt Bear", "Charlie Cheetah", "Donald Duck", "Eva Eagle", "Ellie Elephant", "Gino Giraffe", "Isabella Iguana", "Karl Kitten", "Lionel Lion", "Molly Mouse", "Paul Puppy", "Rachel Rabbit", "Ted Turtle"};

    private async Task AdvancedIonRefreshCallback(IonRefresherIonRefreshEventArgs args)
    {
        await Task.Delay(2000);
        AddItems(3, true);
        await args.Sender.CompleteAsync();
    }
    
    private string ChooseRandomName()
    {
        var rdm = new Random().Next(0, 100) / 100d;
        return Names[(int)Math.Floor(rdm * Names.Length)];
    }
    
    private void AddItems(int count, bool unread)
    {
        throw new NotImplementedException();
        for (var i = 0; i < count; i++) {
            //list.insertBefore(CreateItem(unread), list.firstChild);
        }
    }

    private void CreateItem(bool unread = false)
    {
        throw new NotImplementedException();
        var name = ChooseRandomName();
        //let item = document.createElement('ion-item');
        //item.button = true;

        // item.innerHTML += `
        //   <ion-icon color="primary" name="${unread ? 'ellipse' : ''}" slot="start"></ion-icon>
        //   <ion-label>
        //     <h2>${name}</h2>
        //     <p>New message from ${name}</p>
        //   </ion-label>
        // `;
        
        //return item;
    }

    #endregion
    
}