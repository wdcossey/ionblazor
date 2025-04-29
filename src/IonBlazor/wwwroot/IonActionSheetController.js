import { dotNetCallbackMethod } from './common.js';

export async function present(options, buttons, buttonHandler) {
    const actionSheet = document.createElement('ion-action-sheet');

    actionSheet.header = options.header;
    actionSheet.subHeader = options.subHeader;
    actionSheet.cssClass = options.cssClass;
    actionSheet.backdropDismiss = options.backdropDismiss;
    actionSheet.translucent = options.translucent;
    actionSheet.animated = options.animated;
    actionSheet.mode = options.mode;
    actionSheet.keyboardClose = options.keyboardClose;
    actionSheet.htmlAttributes = options.htmlAttributes;

    if (options.id){
        actionSheet.id = options.id;
    }

    buttons.forEach(function (button, index) {
        button.handler = () => {
            buttonHandler.invokeMethodAsync(dotNetCallbackMethod, {index});
        }
    });
    actionSheet.buttons = buttons;

    document.body.appendChild(actionSheet);

    actionSheet.addEventListener('didDismiss', () => {
        setTimeout(function(){ actionSheet.remove() }, 2000);
    });

    await actionSheet.present();

    return actionSheet.id;
}