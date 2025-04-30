import { dotNetCallbackMethod } from './common.js';

export async function presentToast(options, buttons, buttonHandler = null, didDismissHandler = null) {
    const toast = document.createElement('ion-toast');
    toast.duration = options.duration;
    toast.header = options.header;
    toast.message = options.message;
    toast.position = options.position;
    toast.icon = options.icon;
    toast.translucent = options.translucent;
    toast.animated = options.animated;
    toast.positionAnchor = options.positionAnchor;
    toast.htmlAttributes = options.htmlAttributes;

    if (options.color) {
        toast.color = options.color ?? undefined;
    }
    if (options.id){
        toast.id = options.id;
    }

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

        setTimeout(function(){ toast.remove() }, 2000);
    });

    await toast.present();

    return toast.id;
}