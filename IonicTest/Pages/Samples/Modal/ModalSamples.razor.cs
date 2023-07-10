namespace IonicTest.Pages.Samples.Modal;

public partial class ModalSamples
{
    private IonModal _inlineModalsRef;
    private IonModal _usingIsOpenRef;
    private IonModal _modalWithBooleanDismissRef;
    private IonCheckbox _checkboxDismissRef;

    private string _inlineModalsInputValue;
    private string _message = "This modal example uses triggers to automatically open a modal when the button is clicked.";

    [Inject] private IJSRuntime JsRuntime { get; set; } 
    
    private async Task InlineModalsCancelAsync()
    {
        await _inlineModalsRef.DismissAsync(null, "cancel");
    }

    private async Task InlineModalsConfirmAsync()
    {
        await _inlineModalsRef.DismissAsync(_inlineModalsInputValue, "confirm");
    }
    
    private void InlineModalsWillDismiss(IonModalWillDismissEventArgs args)
    {
        if (args.Role?.Equals("confirm") is true)
        {
            _message = $"Hello {args.Data}!";
        }
        /*
         if (ev.detail.role === 'confirm') {
      const message = document.querySelector('#message');
      message.textContent = `Hello ${ev.detail.data}!`;
    }
         */
        //throw new NotImplementedException();
    }

    private async Task ControllerModalOpenModal()
    {
        
    }
}