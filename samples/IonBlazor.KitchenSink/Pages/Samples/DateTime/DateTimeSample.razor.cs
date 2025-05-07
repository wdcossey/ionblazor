namespace IonicTest.Pages.Samples.DateTime;

public partial class DateTimeSample
{
    private IonDateTime _multipleDateSelectionRef = null!;
    private IonDateTime _customButtonElementsRef = null!;

    private string AdvancedDateConstraintsIsEnabled()
    {
        return
            """
            (dateIsoString) => {
                const date = new Date(dateIsoString);
                const utcDay = date.getUTCDay();
            
                /**
                 * Date will be enabled if it is not
                 * Sunday or Saturday
                 */
                return utcDay !== 0 && utcDay !== 6;
            }
            """;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await _multipleDateSelectionRef.SetValue("2022-06-03", "2022-06-13", "2022-06-29");
    }
}