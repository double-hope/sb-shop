if (SBContact == null) { var SBContact = {}; }
if (SBContact.SBShop == null) { SBContact.SBShop = {}; }

SBContact.SBShop.Core = (function () {

    /************************************************************************************
    * Variables
    ************************************************************************************/
    var formContext;

    var layout = {
        Fields: {
            MobilePhone: "mobilephone"
        },
        SaveMode: {
            Save: 1,
            SaveAndClose: 2,
            SaveAndNew: 59,
            Autosave: 70
        },
        IsValidationNeeded: false,
        ActionNames: {
            DublicatedPhoneActionName: "new_SearchDublicatedPhone",
            SendExcelActionName: "new_SendExcelFlow",
            RetrieveLogicalNamesActionName: "new_RetrieveLogicalNames",
            RetrieveViewNamesActionName: "new_RetrieveViewNames"
        },
        WebResourceNames: {
            ExportToExcel: "sb_exporttoexcel.sb_shop.html"
        }
    };

    /************************************************************************************
    * Page events
    ************************************************************************************/

    async function callSearchDublicatedPhoneAction(context) {
        formContext = context;

        if (SBCore.SBShop.Form.GetEventArgs(formContext).isDefaultPrevented()) {
            return;
        }

        var saveMode = SBCore.SBShop.Form.GetEventArgs(formContext).getSaveMode();

        if (saveMode !== layout.SaveMode.Save &&
            saveMode !== layout.SaveMode.SaveAndClose &&
            saveMode !== layout.SaveMode.SaveAndNew &&
            saveMode !== layout.SaveMode.Autosave) {
            return;
        }

        if (!layout.IsValidationNeeded) {
            layout.IsValidationNeeded = true;
            return;
        }

        const mobilephone = SBCore.SBShop.Form.GetAttribute(layout.Fields.MobilePhone);

        if (mobilephone == null) {
            return;
        }

        SBCore.SBShop.Form.GetEventArgs(formContext).preventDefault();

        const props = {
            PhoneNumber: SBCore.SBShop.Form.GetValue(mobilephone)
        }
        var dublicatedPhoneFunc = new SBContact.SBShop.Core.ConfigureAction(props, layout.ActionNames.DublicatedPhoneActionName);

        const res = await SBCore.SBShop.Form.ExecuteWebApi(dublicatedPhoneFunc);
        const value = await JSON.parse(res.Value);

        if (value.DublicatedUserId) {
            if (saveMode !== layout.SaveMode.Autosave) {

                const callback = () => {
                    var pageInput = {
                        pageType: "entityrecord",
                        entityName: SBCore.SBShop.Form.EntityNames.Contact,
                        entityId: res.DublicatedUserId,
                    };
                    SBCore.SBShop.UI.NavigateTo(pageInput, 2, 1, 50);
                }

                var confirmStrings = { text: `Account with mobile phone ${SBCore.SBShop.Form.GetValue(mobilephone)} already exists! Do you wanna see it?`, title: "Attention" };
                var confirmOptions = { height: 200, width: 450 };
                SBCore.SBShop.UI.OpenConfirmDialog(confirmStrings, confirmOptions, callback);
            }
        }
        else {
            layout.IsValidationNeeded = false;

            if (saveMode === layout.SaveMode.Save || saveMode === layout.SaveMode.Autosave) {
                SBCore.SBShop.Form.Save();
            } else if (saveMode === layout.SaveMode.SaveAndClose) {
                SBCore.SBShop.Form.SaveWithSaveMode(SBCore.SBShop.Form.SaveModes.SaveAndClose);
            } else {
                SBCore.SBShop.Form.SaveWithSaveMode(SBCore.SBShop.Form.SaveModes.SaveAndNew);
            }
        }
    }

    function sendExcelFileActionCall(viewName, logicalName) {
        const props = {
            EntityName: logicalName,
            ViewName: viewName
        }

        var exportFunc = new SBContact.SBShop.Core.ConfigureAction(props, layout.ActionNames.SendExcelActionName);

        SBCore.SBShop.Form.ExecuteWebApi(exportFunc);
    }

    async function callRetrieveLogicalNamesAction(logicalName) {
        const props = {
            LogicalNamePrefix: logicalName
        }

        var retrieveFunc = new SBContact.SBShop.Core.ConfigureAction(props, layout.ActionNames.RetrieveLogicalNamesActionName);

        const res = await SBCore.SBShop.Form.ExecuteWebApi(retrieveFunc);
        const value = await JSON.parse(res.Value);
        return value.LogicalNames;
    }

    async function callRetrieveViewNamesAction(logicalName, viewName) {
        const props = {
            LogicalName: logicalName,
            ViewNamePrefix: viewName
        }
        var retrieveFunc = new SBContact.SBShop.Core.ConfigureAction(props, layout.ActionNames.RetrieveViewNamesActionName);

        const res = await SBCore.SBShop.Form.ExecuteWebApi(retrieveFunc);
        const value = await JSON.parse(res.Value);
        return value.ViewNames;
    }

    /************************************************************************************
     * Field events
     ************************************************************************************/

    function validatePhoneNumber(context) {
        layout.IsValidationNeeded = true;
        formContext = context;
        const mobilePhone = SBCore.SBShop.Form.GetAttribute(layout.Fields.MobilePhone);
        const fieldErrorGuid = "not_valid_id";

        if (!mobilePhone.getValue()) {
            SBCore.SBShop.UI.ClearFieldError(layout.Fields.MobilePhone, fieldErrorGuid);
            return;
        }

        const phoneRegex = new RegExp(/^\d{10}(\d{2})?$/);

        if (phoneRegex.test(mobilePhone.getValue())) {
            SBCore.SBShop.UI.ClearFieldError(layout.Fields.MobilePhone, fieldErrorGuid);

            switch (SBCore.SBShop.Form.GetValue(mobilePhone).length) {
                case 10: {
                    SBCore.SBShop.Form.SetValue(mobilePhone, `+38 (${SBCore.SBShop.Form.GetValue(mobilePhone).substring(0, 3)}) ` +
                        `${SBCore.SBShop.Form.GetValue(mobilePhone).substring(3, 6)} ` +
                        `${SBCore.SBShop.Form.GetValue(mobilePhone).substring(6, 8)} ` +
                        `${SBCore.SBShop.Form.GetValue(mobilePhone).substring(8, 10)}`);
                    break;
                }
                case 12: {
                    SBCore.SBShop.Form.SetValue(mobilePhone, `+${SBCore.SBShop.Form.GetValue(mobilePhone).substring(0, 2)} ` +
                        `(${SBCore.SBShop.Form.GetValue(mobilePhone).substring(2, 5)}) ` +
                        `${SBCore.SBShop.Form.GetValue(mobilePhone).substring(5, 8)} ` +
                        `${SBCore.SBShop.Form.GetValue(mobilePhone).substring(8, 10)} ` +
                        `${SBCore.SBShop.Form.GetValue(mobilePhone).substring(10, 12)}`);
                    break;
                }
            }
        }
        else {
            SBCore.SBShop.UI.ShowFieldError(layout.Fields.MobilePhone, "not valid!", fieldErrorGuid);
        }
    }

    function openExportToExcelModal() {
        var pageInput = {
            pageType: "webresource",
            webresourceName: layout.WebResourceNames.ExportToExcel,
        };

        SBCore.SBShop.UI.NavigateTo(pageInput, 2, 1, 50);
    }


    /************************************************************************************
    * Helpers
    ************************************************************************************/

    function configureAction(props, actionName) {
        const underscoreIndex = actionName.indexOf("_") + 1;

        const action = {
            ActionName: actionName.substring(underscoreIndex),
            Parameters: JSON.stringify(props),
        };

        action.getMetadata = function () {
            return {
                boundParameter: null,
                parameterTypes: {
                    ActionName: { typeName: "Edm.String", structuralProperty: 1 },
                    Parameters: { typeName: "Edm.String", structuralProperty: 1 }
                },
                operationType: 0,
                operationName: actionName,
            }
        };

        return action;
    }

    return {
        CallSearchDublicatedPhoneAction: callSearchDublicatedPhoneAction,
        ConfigureAction: configureAction,
        SendExcelFileActionCall: sendExcelFileActionCall,
        CallRetrieveLogicalNamesAction: callRetrieveLogicalNamesAction,
        CallRetrieveViewNamesAction: callRetrieveViewNamesAction,
        ValidatePhoneNumber: validatePhoneNumber,
        OpenExportToExcelModal: openExportToExcelModal
    };
})();