if (SBLoyaltyCard == null) { var SBLoyaltyCard = {}; }
if (SBLoyaltyCard.SBShop == null) { SBLoyaltyCard.SBShop = {}; }

SBLoyaltyCard.SBShop.Core = (function () {

    /************************************************************************************
    * Variables
    ************************************************************************************/
    var formContext;

    var layout = {
        Fields: {
            Client: "sb_clientid"
        }
    };

    /************************************************************************************
    * Page events
    ************************************************************************************/

    /************************************************************************************
     * Field events
     ************************************************************************************/
    function getContactsToFilter(context) {
        formContext = context;

        const client = SBCore.SBShop.Form.GetControl(layout.Fields.Client);
        SBCore.SBShop.Form.AddPreSearch(client, filterContactsHasEmail);
    }

    function filterContactsHasEmail() {
        var contactsEmailFilter = `<filter type="and">
                                        <condition attribute="emailaddress1" operator="not-null"/>
                                    </filter>`;
        const client = SBCore.SBShop.Form.GetControl(layout.Fields.Client);
        SBCore.SBShop.Form.AddCustomFilter(client, contactsEmailFilter, SBCore.SBShop.Form.EntityNames.Contact);
    }

    /************************************************************************************
    * Helpers
    ************************************************************************************/

    return {
        GetContactsToFilter: getContactsToFilter
    };
})();