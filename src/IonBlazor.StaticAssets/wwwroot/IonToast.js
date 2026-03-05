import { dotNetCallbackMethod } from './common.js';
export function dismiss(element) {
    return element.dismiss();
}
export function withButtons(element, buttons, callback) {
    if (buttons == null)
        return;
    buttons.forEach(function (button, index) {
        button.handler = () => {
            callback.invokeMethodAsync(dotNetCallbackMethod, { index });
        };
    });
    element.buttons = buttons;
}
