import { dotNetCallbackMethod } from './common.js';

export async function present(message, duration, htmlAttributes, didDismissHandler, didPresentHandler) {
    const loading = document.createElement('ion-loading');
    loading.message = message;

    if (duration) {
        loading.duration = duration;
    }

    loading.htmlAttributes = htmlAttributes;

    document.body.appendChild(loading);

    loading.addEventListener('didDismiss', (ev) => {
        if (didDismissHandler) {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });
        }

        setTimeout(function(){ loading.remove() }, 2000);
    });

    loading.addEventListener('didPresent', (ev) => {
        if (didPresentHandler) {
            didPresentHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });
        }
    });

    await loading.present();
    return loading.id;
}

export function setMessage(id, message) {
    const element = getElement(id);
    if (element) {
        element.message = message;
    }
}

export function updateDuration(id, duration) {
    const element = getElement(id);
    if (element) {
        element.duration = duration;
    }
}

export function dismiss(id, data, role) {
    const element = getElement(id);
    return element?.dismiss(data, role);
}

function getElement(id) {
    return document.querySelector(`ion-loading#${id}`);
}