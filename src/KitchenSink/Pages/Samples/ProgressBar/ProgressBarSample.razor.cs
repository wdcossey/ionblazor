namespace IonicTest.Pages.Samples.ProgressBar;

public partial class ProgressBarSample
{
    private Timer _determinateTimer;
    private double _progress = 0;
    
    private double _bufferBuffer = 0.06;
    private double _bufferProgress = 0;
    private Timer _bufferTimer;
    private bool _bufferState = false;

    
    private IonProgressBar _determinateProgressBar = null!;
    private IonProgressBar _bufferProgressBar = null!;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        
        if (!firstRender)
            return;
        
        _determinateTimer = new Timer(_ =>
        {
            if (_progress > 1)
            {
                _progress = 0.0;
                _determinateProgressBar.SetValue(_progress);
                _determinateTimer.Change(1000, 50);
                return;
            }

            _determinateProgressBar.SetValue(_progress += 0.01);

            if (_progress > 1)
            {
                _determinateTimer.Change(1000, 50);
            }
        }, null, 50, 50);
        
        _bufferTimer = new Timer(state =>
        {
            if (_bufferState)
            {
                _bufferState = false;
                //_bufferTimer.Change(1000, 1000);
                return;
            }
            
            _bufferProgressBar.SetBuffer(_bufferBuffer += 0.06);
            _bufferProgressBar.SetValue(_bufferProgress += 0.06);
            
            if (_bufferProgress > 1)
            {
                _bufferProgressBar.SetBuffer(_bufferBuffer = 0.06);
                _bufferProgressBar.SetValue(_bufferProgress = 0);
                _bufferState = true;
                _bufferTimer.Change(1000, 1000);
            }
            
            //if (_progress > 1)
            //{
            //    _progress = 0.0;
            //    _determinateProgressBar.SetValue(_progress);
            //    _bufferTimer.Change(1000, 50);
            //    return;
            //}
//
            //_determinateProgressBar.SetValue(_progress += 0.01);
//
            //if (_progress > 1)
            //{
            //    _bufferTimer.Change(1000, 50);
            //}
        }, _bufferState, 1000, 1000);
        
        
    }
}