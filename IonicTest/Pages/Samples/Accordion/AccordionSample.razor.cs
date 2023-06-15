namespace IonicTest.Pages.Samples.Accordion;

public partial class AccordionSample
{
    private IonAccordionGroup _toggleAccordionGroup = null!;
    
    private readonly string[] _values = {"first", "second", "third"};
    
    private string _listenerOutContent =string.Empty;

    private async Task ToggleAccordionAsync()
    {
        await _toggleAccordionGroup.SetValue(_toggleAccordionGroup.Value?.Contains("second") is true ? null : new [] { "second"});
    }
    
    private void AccordionOnChangeCallback(AccordionGroupIonChangeEventArgs args)
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