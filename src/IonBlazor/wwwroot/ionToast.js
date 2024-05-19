export function dismiss(element) {
    element.dismiss();
}

export function withButtons(element, buttons, callback) {
    
    if (buttons == null)
        return;

    buttons.forEach(function (button, index) {
        button.handler = (value) => {
            callback.invokeMethodAsync(dotNetCallbackMethod, {value, index});
        }
    });
    
    element.buttons = buttons;
}