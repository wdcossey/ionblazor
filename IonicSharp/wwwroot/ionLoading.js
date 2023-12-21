export function dismiss(element, data, role) {
    return element.dismiss(data, role);
}

export async function present(element) {
    await element.present();
}

export function setMessage(element, message) {
    element.message = message;
}

export async function presentWithMessage(element, message) {
    element.message = message;
    await present(element);
}