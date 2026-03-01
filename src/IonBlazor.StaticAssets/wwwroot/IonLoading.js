export function dismiss(element, data, role) {
    return element.dismiss(data, role);
}

export async function present(element) {
    await element.present();
}

export function updateMessage(element, message) {
    element.message = message;
}