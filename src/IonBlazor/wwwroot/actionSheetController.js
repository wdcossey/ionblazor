import { dotNetCallbackMethod } from './common.js';

export async function presentActionSheet(header, buttons, buttonHandler) {
    const actionSheet = document.createElement('ion-action-sheet');
    actionSheet.header = header;

    buttons.forEach(function (button, index) {
        button.handler = () => {
            buttonHandler.invokeMethodAsync(dotNetCallbackMethod, {index});
        }
    });
    actionSheet.buttons = buttons;

    document.body.appendChild(actionSheet);

    actionSheet.addEventListener('didDismiss', () => {
        actionSheet.remove();
    });

    await actionSheet.present();
}