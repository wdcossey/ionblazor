namespace IonicTest.Pages.Samples.SkeletonText;

public partial class SkeletonTextSample
{
    private bool _loaded = true;
    
    private void BasicUsageToggle()
    {
        _loaded = !_loaded;
        StateHasChanged();
    }
}