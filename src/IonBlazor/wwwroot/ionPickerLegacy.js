import { dotNetCallbackMethod } from './common.js';

export function withColumns(element, columns) {
    element.columns = columns;
}

export function withButtons(element, buttons, callback) {
    buttons.forEach(function (button, index) {
        button.handler = (value) => {
            callback.invokeMethodAsync(dotNetCallbackMethod, {value, index});
        }
    });
    element.buttons = buttons;
}

export function dismiss(element, data, role) {
    return element.dismiss(data, role);
}

export function getColumn(element, name) {
    return element.getColumn(name);
}

export function present(element) {
    return element.present();
}