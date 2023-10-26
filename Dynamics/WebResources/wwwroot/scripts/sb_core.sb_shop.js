if (SBCore == null) { var SBCore = {}; }
if (SBCore.SBShop == null) { SBCore.SBShop = {}; }

SBCore.SBShop.UI = (function () {

    /************************************************************************************
    * Variables
    ************************************************************************************/
    var formContext = {};

    var layout = {
        Fields: {}
    };

    /************************************************************************************
    * Page events
    ************************************************************************************/

    function onLoad(context) {
        try {
            formContext = context.getFormContext();

        } catch (e) {
            console.log(e);
            showErrorMessage(e.message);
        }
    };

    function onSave(context) {
        try {
            formContext = context.getFormContext();

        } catch (e) {
            console.log(e);
            showErrorMessage(e.message);
        }
    };

    function navigateTo(pageInput, target, position, width) {
        Xrm.Navigation.navigateTo(pageInput,
            {
                target: target,
                position: position,
                width: { value: width, unit: "%" }
            });
    }

    function openConfirmDialog(confirmStrings, confirmOptions, okCallback = () => { }, cancelCallback = () => { }) {
        Xrm.Navigation.openConfirmDialog(confirmStrings, confirmOptions).then(
            function (success) {
                if (success.confirmed)
                    okCallback();
                else
                    cancelCallback();
            });
    }

    function openAlertDialog(text) {
        Xrm.Navigation.openAlertDialog({ text });
    }

    /************************************************************************************
     * Field events
     ************************************************************************************/

    function showFieldError(fieldName, message, guid) {
        SBCore.SBShop.Form.GetControl(fieldName).setNotification(message, guid);
    }

    function clearFieldError(fieldName, guid) {
        SBCore.SBShop.Form.GetControl(fieldName).clearNotification(guid);
    }

    /************************************************************************************
    * Helpers
    ************************************************************************************/

    function showErrorMessage(message) {

        var guid = Date.now().toString();

        formContext.ui.setFormHtmlNotification(message, "ERROR", guid);

        setTimeout(clearMessage, 5000, guid);
    }

    function clearMessage(guid) {
        try {

            formContext.ui.clearFormNotification(guid);

        } catch (e) {
            Xrm.Navigation.openAlertDialog({ confirmButtonLabel: "Ok", text: e.message });
        }
    }

    return {
        ShowFieldError: showFieldError,
        ClearFieldError: clearFieldError,

        onLoad: onLoad,
        onSave: onSave,
        NavigateTo: navigateTo,
        OpenConfirmDialog: openConfirmDialog,
        OpenAlertDialog: openAlertDialog,
    };
})();

SBCore.SBShop.Form = (function () {

    /************************************************************************************
    * Variables
    ************************************************************************************/
    var formContext = {};

    var layout = {
        EntityNames: {
            Contact: "contact",
            LoyaltyCard: "sb_loyaltycard"
        },
        SaveModes: {
            SaveAndClose: "saveandclose",
            SaveAndNew: "saveandnew"
        }
    };

    /************************************************************************************
    * Page events
    ************************************************************************************/

    function onLoad(context) {
        try {
            formContext = context.getFormContext();

        } catch (e) {
            console.log(e);
            showErrorMessage(e.message);
        }
    };

    function onSave(context) {
        try {
            formContext = context.getFormContext();

        } catch (e) {
            console.log(e);
            showErrorMessage(e.message);
        }
    };

    async function executeWebApi(func) {
        if (typeof Xrm === "undefined") Xrm = parent.Xrm;

        const response = await Xrm.WebApi.online.execute(func);

        if (response.ok) {
            console.log("Status: %s %s", response.status, response.statusText);
            const json = await response.json();
            const res = await JSON.parse(json.Response);
            return res;
        }
        else {
            console.error(response.error.message);
        }
    }

    /************************************************************************************
     * Field events
     ************************************************************************************/

    function getValue(field) {
        return field.getValue();
    }

    function setValue(field, value) {
        field.setValue(value);
    }

    function addPreSearch(field, func) {
        field.addPreSearch(func);
    }

    function addCustomFilter(field, filter, entity) {
        field.addCustomFilter(filter, entity);
    }

    /************************************************************************************
    * Helpers
    ************************************************************************************/

    function getAttribute(attributeName, context) {
        /// <summary> Returns attribute. </summary>
        /// <param name="attributeName" type="string"> Attribute name. </param>
        /// <returns type="Object"> Return the attribute. </returns>

        formContext = context;

        var attribute;

        if (formContext != null) {
            attribute = formContext.getAttribute(attributeName);
        } else {
            attribute = Xrm.Page.getAttribute(attributeName);
        }

        if (attribute == null) {
            throw new Error("Data Field " + attributeName + " not found.");
        }

        return attribute;
    }

    function getControl(controlName, context) {
        /// <summary> Returns control. </summary>
        /// <param name="controlName" type="string"> Control name. </param>
        /// <returns type="Object"> Return the control. </returns>

        formContext = context;

        var control;

        if (formContext != null) {
            control = formContext.getControl(controlName);
        } else {
            control = Xrm.Page.getControl(controlName);
        }

        if (control == null) {
            throw new Error("Data Field " + controlName + " not found.");
        }

        return control;
    }
    function getGlobalContext() {
        if (typeof Xrm === "undefined") Xrm = parent.Xrm;
        return Xrm.Utility.getGlobalContext();
    }
    function getEventArgs(context) {
        formContext = context;

        return formContext.getEventArgs();
    }

    function save(context) {
        formContext = context;

        if (formContext != null) {
            formContext.data.entity.save();
        } else {
            Xrm.Page.data.entity.save();
        }
    }

    function saveWithSaveMode(saveMode, context) {
        formContext = context;

        if (formContext != null) {
            formContext.data.entity.save(saveMode);
        } else {
            Xrm.Page.data.entity.save(saveMode);
        }
    }

    return {
        GetAttribute: getAttribute,
        GetControl: getControl,
        GetGlobalContext: getGlobalContext,
        GetEventArgs: getEventArgs,
        Save: save,
        SaveWithSaveMode: saveWithSaveMode,
        GetValue: getValue,
        SetValue: setValue,
        AddPreSearch: addPreSearch,
        AddCustomFilter: addCustomFilter,

        OnLoad: onLoad,
        OnSave: onSave,
        ExecuteWebApi: executeWebApi,

        EntityNames: layout.EntityNames,
        SaveModes: layout.SaveModes
    };
})();