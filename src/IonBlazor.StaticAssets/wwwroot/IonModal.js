import { dotNetCallbackMethod } from './common.js';
export function isOpen(element, value) {
    return (element.isOpen = value);
}
export function dismiss(element, data, role) {
    return element.dismiss(data, role);
}
export function canDismiss(element, value) {
    element.canDismiss = value;
}
export function canDismissCallback(element, callback) {
    element.canDismiss = (args) => callback.invokeMethodAsync(dotNetCallbackMethod, args);
}
export function getCurrentBreakpoint(element) {
    return element.getCurrentBreakpoint();
}
export function breakpoints(element, value) {
    element.breakpoints = value;
}
export function initialBreakpoint(element, value) {
    element.initialBreakpoint = value;
}
export function present(element) {
    return element.present();
}
export async function setCurrentBreakpoint(element, value) {
    await element.setCurrentBreakpoint(value);
}
export function enterAnimation(element, value) {
    // eslint-disable-next-line no-eval
    element.enterAnimation = eval(value);
}
