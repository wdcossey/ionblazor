import { dotNetCallbackMethod } from './common.js';

export function addButtons(element, buttons, callback) {
    buttons.forEach(function (button, index) {
        button.handler = () => {
            callback.invokeMethodAsync(dotNetCallbackMethod, {index});
        }
    });
    element.buttons = buttons;
}

export function addInputs(element, inputs) {
    /*buttons.forEach(function (button, index) {
        button.handler = () => { callback.invokeMethodAsync(dotNetCallbackMethod, { index }); }
    });
    console.log(inputs)*/
    element.inputs = inputs;
}

export function dismiss(element, data, role) {
    return element.dismiss(data, role);
}

export function onDidDismiss(element) {
    //element.onDidDismiss();
}

export function onWillDismiss(element) {
    //element.onWillDismiss();
}

export function present(element) {
    element.present();
}