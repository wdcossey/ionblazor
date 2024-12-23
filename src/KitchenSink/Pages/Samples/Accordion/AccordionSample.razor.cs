namespace IonicTest.Pages.Samples.Accordion;

public partial class AccordionSample
{
    private IonAccordionGroup _toggleAccordionGroup = null!;

    private readonly string[] _values = ["first", "second", "third"];

    private string _listenerOutContent =string.Empty;

    private async Task ToggleAccordionAsync()
    {
        var value = await _toggleAccordionGroup.Value;
        await _toggleAccordionGroup.SetValueAsync(value.Contains("second") ? null : [ "second" ]);
    }

    private void AccordionOnChangeCallback(IonAccordionGroupIonChangeEventArgs args)
    {
        var collapsedItems = _values.Where(value => args.Value?.Contains(value) is not true);
        var selectedValue = args.Value;

        _listenerOutContent =
            $$"""

            Expanded: {{(selectedValue?.Any() is not true ? "None" : string.Join(",", selectedValue))}}
            Collapsed: {{string.Join(",", collapsedItems)}}

            """;
    }
}