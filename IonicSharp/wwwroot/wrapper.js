window.IonicSharp = {
   
    /*addCustomEventListener: function (element, ref) {
        element.addEventListener('ionInput', (ev) => {
            console.log(ev);
            ref.invokeMethodAsync(dotNetCallbackMethod, ev.target.value);
        });
    },

    addIonEventListener: function (type, element, ref) {
        element.addEventListener(type, (ev) => {
            ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: ev.target.tagName, detail: ev.detail });
        });
    },*/

    attachListener: function (type, element, ref) {
        element.addEventListener(type, (ev) => {
            ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: ev.target.tagName, detail: ev.detail });
        });
    },

    attachListeners: function (configs, element) {
        configs.forEach(function (config) {
            IonicSharp.attachListener(config.event, element, config.ref)
        });
    },

    IonAccordionGroup: {
        setValue: function (element, value) {
            element.value = value;
        }
    },

    IonActionSheet: {
        addButtons: function (element, buttons, callback) {
            buttons.forEach(function (button, index) {
                button.handler = () => {
                    callback.invokeMethodAsync(dotNetCallbackMethod, {index});
                }
            });
            element.buttons = buttons;
        },

        dismiss: function (element, data, role) {
            return element.dismiss(data, role);
        },

        onDidDismiss: function (element) {
            element.onDidDismiss();
        },

        onWillDismiss: function (element) {
            element.onWillDismiss();
        },

        present: function (element) {
            element.present();
        },
        
    },
    
    IonAlert: {
        addButtons: function (element, buttons, callback) {
            buttons.forEach(function (button, index) {
                button.handler = () => { callback.invokeMethodAsync(dotNetCallbackMethod, { index }); }
            });
            element.buttons = buttons;
        },
        
        addInputs: function (element, inputs) {
            /*buttons.forEach(function (button, index) {
                button.handler = () => { callback.invokeMethodAsync(dotNetCallbackMethod, { index }); }
            });*/
            console.log(inputs)
            element.inputs = inputs;
        },

        dismiss: function (element, data, role) {
            return element.dismiss(data, role);
        },

        onDidDismiss: function (element) {
            element.onDidDismiss();
        },

        onWillDismiss: function (element) {
            element.onWillDismiss();
        },

        present: function (element) {
            element.present();
        },
    },
    
    IonBreadCrumbs: {
        attachIonCollapsedClickListener: function (type, element, ref) {
            element.addEventListener(type, (ev) => {
                ref.invokeMethodAsync(dotNetCallbackMethod, {
                    tagName: ev.target.tagName,
                    detail: ev.detail.collapsedBreadcrumbs.map(obj => {
                        return { href: obj.href, textContent: obj.textContent }
                    })
                });
            });
        }
    },

    IonContent : {
        getScrollElement: function (element) {
            let result = element.getScrollElement();
            
            return result;
        },

        scrollByPoint: function (element, x, y, duration) {
            element.scrollByPoint(x, y, duration);
        },

        scrollToBottom: function (element, duration) {
            element.scrollToBottom(duration);
        },

        scrollToPoint: function (element, x, y, duration) {
            element.scrollToPoint(x, y, duration);
        },

        scrollToTop: function (element, duration) {
            element.scrollToTop(duration);
        },
        
    },
    
    IonDateTime: {
        isDateEnabled: function (element, callback) {
                        
            //element.isDateEnabled = async (dateIsoString) => {
            //    const isEnabled = await callback.invokeMethodAsync('OnCallbackEvent', {dateIsoString});
            //    console.log(`${dateIsoString}: ${isEnabled}`);
            //    return isEnabled;
            //}
        
            element.isDateEnabled = (dateIsoString) => {
                const date = new Date(dateIsoString);
                const utcDay = date.getUTCDay();
            
                /**
                 * Date will be enabled if it is not
                 * Sunday or Saturday
                 */
                return utcDay !== 0 && utcDay !== 6;
            }
        },
        
        setValue: function (element, value) {
            element.value = value;
        },
        
        cancel: function (element, closeOverlay) {
            element.cancel(closeOverlay);
        },
        
        confirm: function (element, closeOverlay) {
            element.confirm(closeOverlay);
        },
        
        reset: function (element, startDate) {
            element.reset(startDate);
        }
    },

    IonInfiniteScroll: {
        complete: function (elm) {
            elm.complete();
        }
    },

    IonList : {
        closeSlidingItems : function (element) {
            return element.closeSlidingItems();
        }
    },
    
    IonModal : {
        isOpen : function (element, value) {
            return element.isOpen = value;
        },
        
        dismiss : function (element, data, role) {
            return element.dismiss(data, role);
        },

        canDismiss : function (element, value) {
            element.canDismiss = value;
        },

        getCurrentBreakpoint : function (element, value) {
            
        }
    },

    IonPicker : {

        withColumns : function (element, columns) {
            element.columns = columns;
        },
        
        withButtons : function (element, buttons, callback) {
            buttons.forEach(function (button, index) {
                button.handler = (value) => {
                    callback.invokeMethodAsync(dotNetCallbackMethod, {value, index});
                }
            });
            element.buttons = buttons;
        }
    },

    IonPickerController : {
        openPicker : async function(columns, buttons) {
            //const picker = await pickerController.create({
            //    columns: columns,
            //    buttons: buttons,
            //});
            //await picker.present();
        }
    },

    IonReorderGroup : {
        complete : function (element, listOrReorder) {
            element.complete(listOrReorder);
        }
    },
    
    IonRippleEffect: {
        addRipple : function (element, x, y) {
            element.addRipple(x, y);
        }
    },

    IonToast : {
        withButtons : function (element, buttons, callback) {
            
            if (buttons == null)
                return;
            
            buttons.forEach(function (button, index) {
                button.handler = (value) => {
                    callback.invokeMethodAsync(dotNetCallbackMethod, {value, index});
                }
            });
            element.buttons = buttons;
        }
    },

    IonToastController : {
        present : async function (message, position, duration = 1500, icon = null) {
            const alert = document.createElement('ion-toast');
            alert.duration = duration;
            alert.message = message;
            alert.buttons = ['OK'];
            alert.position = position;
            alert.icon = icon

            document.body.appendChild(alert);
            await alert.present();
        }
    },
}

const dotNetCallbackMethod = 'OnCallbackEvent';