import { dotNetCallbackMethod } from './common.js';

export async function presentToast(header, message, position, duration = 1500,
                                   icon = null, positionAnchor= null, buttons = null,
                                   buttonHandler = null, translucent = null, animated = null,
                                   htmlAttributes = null, didDismissHandler = null) {
    const toast = document.createElement('ion-toast');
    toast.duration = duration;
    toast.header = header;
    toast.message = message;
    toast.position = position;
    toast.icon = icon;
    toast.translucent = translucent;
    toast.animated = animated;
    toast.positionAnchor = positionAnchor;
    toast.htmlAttributes = htmlAttributes;

    if (buttons) {
        buttons.forEach(function (button, index) {
            button.handler = () => {
                buttonHandler.invokeMethodAsync(dotNetCallbackMethod, {index});
            }
        });
        toast.buttons = buttons;
    }

    document.body.appendChild(toast);

    toast.addEventListener('didDismiss', (ev) => {
        if (didDismissHandler) {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });
        }

        toast.remove();
    });

    await toast.present();
}