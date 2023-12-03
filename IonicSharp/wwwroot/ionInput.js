export function counterFormatter(element, callback) {
    element.counterFormatter = async (inputLength, maxLength)=> {
        return await callback.invokeMethodAsync('OnCallbackEvent', {inputLength, maxLength});
    };
}

export function counterFormat(element, format) {
    
    if (format == null)
        return;
    
    element.counterFormatter = (inputLength, maxLength) => {
        return eval(format);
    };
}

export function setValue(element, value) {
    element.value = value;
    return element.value;
}

export function setFocus(element) {
    element.setFocus();
}