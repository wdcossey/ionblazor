import { dotNetCallbackMethod } from './common.js';

export async function create(id, message, duration, htmlAttributes, didDismissHandler, didPresentHandler) {

    const loading = document.createElement('ion-loading');

    if (id) {
        loading.setAttribute("id", id);
    }

    loading.message = message;

    if (duration) {
        loading.duration = duration;
    }

    loading.htmlAttributes = htmlAttributes;

    document.body.appendChild(loading);

    if (didDismissHandler) {
        loading.addEventListener('didDismiss', (ev) => {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });

        });
    }

    if (didPresentHandler) {
        loading.addEventListener('didPresent', (ev) => {
            didPresentHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });
        });
    }

    return id ?? loading.id;
}

export async function present(id) {
    const loading = getElement(id);
    if (loading) {
        await loading.present();
    }
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

export function remove(id) {
    const element = getElement(id);
    element?.dismiss();
    return element?.remove();
}

function getElement(id) {
    return document.querySelector(`ion-loading#${id}`);
}