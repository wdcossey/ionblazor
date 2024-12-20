import { dotNetCallbackMethod } from './common.js';

export function isOpen(element, value) {
    return element.isOpen = value;
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
    element.present();
}

export function setCurrentBreakpoint(element, value) {
    element.setCurrentBreakpoint(value);
}

export function enterAnimation(element, value) {
    element.enterAnimation = eval(value);
}