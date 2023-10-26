if (SBLoyaltyCard == null) { var SBLoyaltyCard = {}; }
if (SBLoyaltyCard.SBShop == null) { SBLoyaltyCard.SBShop = {}; }

SBLoyaltyCard.SBShop.Form = (function (window, document) {
    
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

    /************************************************************************************
     * Field events
     ************************************************************************************/
    function onClientClicked(executionContext) {
        var formContext = executionContext;

        SBLoyaltyCard.SBShop.Core.GetContactsToFilter(formContext);
    }

    /************************************************************************************
    * Helpers
    ************************************************************************************/


    return {
        OnClientClicked: onClientClicked,
    };
})(window, document);