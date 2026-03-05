import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

interface ActionSheetOptions {
    header?: string;
    subHeader?: string;
    cssClass?: string | string[];
    backdropDismiss?: boolean;
    translucent?: boolean;
    animated?: boolean;
    mode?: 'ios' | 'md';
    keyboardClose?: boolean;
    htmlAttributes?: Record<string, unknown>;
    id?: string;
}

export async function present(
    options: ActionSheetOptions,
    buttons: ActionSheetButton[],
    buttonHandler: DotNetObjectReference,
    didDismissHandler: DotNetObjectReference | null
): Promise<string> {
    const actionSheet = document.createElement('ion-action-sheet') as HTMLIonActionSheetElement;

    actionSheet.header = options.header;
    actionSheet.subHeader = options.subHeader;
    actionSheet.cssClass = options.cssClass ?? '';
    actionSheet.backdropDismiss = options.backdropDismiss ?? true;
    actionSheet.translucent = options.translucent ?? false;
    actionSheet.animated = options.animated ?? true;
    actionSheet.mode = options.mode;
    actionSheet.keyboardClose = options.keyboardClose ?? true;
    actionSheet.htmlAttributes = options.htmlAttributes;

    if (options.id) {
        actionSheet.id = options.id;
    }

    buttons.forEach(function (button, index) {
        button.handler = () => {
            buttonHandler.invokeMethodAsync(dotNetCallbackMethod, { index });
        };
    });
    actionSheet.buttons = buttons;

    document.body.appendChild(actionSheet);

    actionSheet.addEventListener('didDismiss', (ev: Event) => {
        if (didDismissHandler) {
            const customEv = ev as CustomEvent;
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, { tagName: (ev.target as HTMLElement).tagName, detail: customEv.detail });
        }

        setTimeout(function () { actionSheet.remove(); }, 2000);
    });

    await actionSheet.present();

    return actionSheet.id;
}
