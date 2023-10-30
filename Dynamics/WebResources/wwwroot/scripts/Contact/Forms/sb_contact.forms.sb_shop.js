if (SBContact == null) { var SBContact = {}; }
if (SBContact.SBShop == null) { SBContact.SBShop = {}; }

SBContact.SBShop.Form = (function (window, document) {
    
	/************************************************************************************
    * Variables
    ************************************************************************************/
    var formContext;

    var layout = {
        Fields: {}
    };

    /************************************************************************************
    * Page events
    ************************************************************************************/

    function onSave(executionContext) {
        var formContext = executionContext;

        SBContact.SBShop.Core.CallSearchDublicatedPhoneAction(formContext);
    }

    /************************************************************************************
     * Field events
     ************************************************************************************/

    function onPhoneNumberChanged(executionContext) {
        var formContext = executionContext;

        SBContact.SBShop.Core.ValidatePhoneNumber(formContext);
    }

    /************************************************************************************
    * Helpers
    ************************************************************************************/

    return {
        OnSave: onSave,
        OnPhoneNumberChanged: onPhoneNumberChanged
    };
})(window, document);