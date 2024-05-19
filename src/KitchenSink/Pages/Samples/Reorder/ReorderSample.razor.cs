namespace IonicTest.Pages.Samples.Reorder;

public partial class ReorderSample
{
    private void OnIonItemReorder(IonReorderGroupIonItemReorderEventArgs args)
    {
        // The `from` and `to` properties contain the index of the item
        // when the drag started and ended, respectively
        Console.WriteLine($"Dragged from index {args.From} to {args.To}");

        // Finish the reorder and position the item in the DOM based on
        // where the gesture ended. This method can also be called directly
        // by the reorder group
        args.Sender?.CompleteAsync(reorder: true);
    }
}