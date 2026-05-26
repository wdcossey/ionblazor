import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

interface AlertOptions {
    header?: string;
    subHeader?: string;
    message?: string;
    cssClass?: string | string[];
    backdropDismiss?: boolean;
    translucent?: boolean;
    animated?: boolean;
    htmlAttributes?: Record<string, unknown>;
    mode?: 'ios' | 'md';
    keyboardClose?: boolean;
    id?: string;
}

export async function presentAlert(
    options: AlertOptions,
    buttons: AlertButton[] | null,
    inputs: AlertInput[] | null,
    buttonHandler: DotNetObjectReference,
    didDismissHandler: DotNetObjectReference | null
): Promise<string> {
    const alert = document.createElement('ion-alert') as HTMLIonAlertElement;

    alert.header = options.header;
    alert.subHeader = options.subHeader;
    alert.message = options.message;
    alert.cssClass = options.cssClass ?? '';
    alert.backdropDismiss = options.backdropDismiss ?? true;
    alert.translucent = options.translucent ?? false;
    alert.animated = options.animated ?? true;
    alert.htmlAttributes = options.htmlAttributes;
    alert.mode = options.mode;
    alert.keyboardClose = options.keyboardClose ?? true;

    if (options.id) {
        alert.id = options.id;
    }

    if (buttons) {
        buttons.forEach(function (button, index) {
            button.handler = () => {
                buttonHandler.invokeMethodAsync(dotNetCallbackMethod, { index });
            };
        });
        alert.buttons = buttons;
    }

    if (inputs) {
        alert.inputs = inputs;
    }

    document.body.appendChild(alert);

    alert.addEventListener('didDismiss', (ev: Event) => {
        if (didDismissHandler) {
            const customEv = ev as CustomEvent;
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, { tagName: (ev.target as HTMLElement).tagName, detail: customEv.detail });
        }

        setTimeout(function () { alert.remove(); }, 2000);
    });

    await alert.present();

    return alert.id;
}
