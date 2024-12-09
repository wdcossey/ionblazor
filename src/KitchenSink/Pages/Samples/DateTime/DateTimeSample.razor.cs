namespace IonicTest.Pages.Samples.DateTime;

public partial class DateTimeSample
{
    private IonDateTime _multipleDateSelectionRef = null!;
    private IonDateTime _customButtonElementsRef = null!;

    private bool AdvancedDateConstraintsIsEnabled(string dateString)
    {
        var date = System.DateTime.Parse(dateString);
        var utcDay = date.DayOfWeek;

        /*
         Date will be enabled if it is not
         Sunday or Saturday
        */
        var result = utcDay is not (DayOfWeek.Sunday or DayOfWeek.Saturday);
        return result;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await _multipleDateSelectionRef.SetValue("2022-06-03", "2022-06-13", "2022-06-29");
    }
}