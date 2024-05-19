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