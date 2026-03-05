import { dotNetCallbackMethod } from './common.js';
export async function presentToast(options, buttons, buttonHandler = null, didDismissHandler = null) {
    const toast = document.createElement('ion-toast');
    if (options.duration !== undefined)
        toast.duration = options.duration;
    toast.header = options.header;
    toast.message = options.message;
    if (options.position !== undefined)
        toast.position = options.position;
    toast.icon = options.icon;
    toast.translucent = options.translucent ?? false;
    toast.animated = options.animated ?? true;
    toast.positionAnchor = options.positionAnchor;
    toast.htmlAttributes = options.htmlAttributes;
    if (options.color) {
        toast.color = options.color ?? undefined;
    }
    if (options.id) {
        toast.id = options.id;
    }
    if (buttons && buttonHandler) {
        buttons.forEach(function (button, index) {
            button.handler = () => {
                buttonHandler.invokeMethodAsync(dotNetCallbackMethod, { index });
            };
        });
        toast.buttons = buttons;
    }
    document.body.appendChild(toast);
    toast.addEventListener('didDismiss', (ev) => {
        if (didDismissHandler) {
            const target = ev.target;
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: target.tagName,
                detail: ev.detail,
                htmlAttributes: target.htmlAttributes,
                id: target.id,
            });
        }
        setTimeout(function () { toast.remove(); }, 2000);
    });
    await toast.present();
    return toast.id;
}
