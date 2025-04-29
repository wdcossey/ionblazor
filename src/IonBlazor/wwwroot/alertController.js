import { dotNetCallbackMethod } from './common.js';

export async function presentAlert(header, subHeader, message, buttons, inputs, buttonHandler, didDismissHandler, htmlAttributes) {
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

    if (inputs) {
        alert.inputs = inputs;
    }

    document.body.appendChild(alert);

    alert.addEventListener('didDismiss', (ev) => {
        if (didDismissHandler) {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, { tagName: ev.target.tagName, detail: ev.detail });
        }

        setTimeout(function(){ alert.remove() }, 2000);
    });

    await alert.present();
}