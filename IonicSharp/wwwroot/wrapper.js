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

    IonInfiniteScroll: {
        complete: function (elm) {
            elm.complete();
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
        
        canDismissCallback : function (element, callback) {
            element.canDismiss = (args) => callback.invokeMethodAsync(dotNetCallbackMethod, args);
        },

        getCurrentBreakpoint : function (element, value) {
            
        },

        breakpoints : function (element, value) {
            element.breakpoints = value;
        },

        initialBreakpoint : function (element, value) {
            element.initialBreakpoint = value;
        },

        setCurrentBreakpoint : function (element, value) {
            element.setCurrentBreakpoint(value);
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

    IonTab : {
        setActive : function (element) {
            element.setActive();
        }
    },

    IonTabs : {
        getSelected : function (element) {
            return element.getSelected();
        },
        
        getTab : function (element, tab) {
            return element.getTab(tab);
        },

        select : function (element, tab) {
            return element.select(tab);
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
    
}

const dotNetCallbackMethod = 'OnCallbackEvent';