
const dotNetCallbackMethod = 'OnCallbackEvent';

export function attachListener(type, element, ref) {
    element.addEventListener(type, (ev) => {
        ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: ev.target.tagName, detail: ev.detail });
    });
}

export function attachListeners(configs, element) {
    configs.forEach(function (config) {
        attachListener(config.event, element, config.ref)
    });
}