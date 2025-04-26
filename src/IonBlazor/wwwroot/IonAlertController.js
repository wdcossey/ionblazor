import { dotNetCallbackMethod } from './common.js';

export async function presentAlert(options, buttons, inputs, buttonHandler, didDismissHandler) {
    const alert = document.createElement('ion-alert');

    alert.header = options.header;
    alert.subHeader = options.subHeader;
    alert.message = options.message;
    alert.cssClass = options.cssClass;
    alert.backdropDismiss = options.backdropDismiss;
    alert.translucent = options.translucent;
    alert.animated = options.animated;
    alert.htmlAttributes = options.htmlAttributes;

    alert.mode = options.mode;
    alert.keyboardClose = options.keyboardClose;
    if (options.id){
        alert.id = options.id;
    }

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

    return alert.id;
}