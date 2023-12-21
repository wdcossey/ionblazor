export async function presentAlert(header, subHeader, message, buttons, buttonHandler, htmlAttributes) {
    const alert = document.createElement('ion-alert');
    alert.header = header;
    alert.subHeader = subHeader;
    alert.message = message;
    alert.htmlAttributes = htmlAttributes;

    if (buttons) {
        buttons.forEach(function (button, index) {
            button.handler = () => {
                buttonHandler.invokeMethodAsync(dotNetCallbackMethod, {index});
            }
        });
        alert.buttons = buttons;
    }

    document.body.appendChild(alert);
    
    alert.addEventListener('didDismiss', () => {
        alert.remove();
    });
    
    await alert.present();
}