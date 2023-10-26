if (SBContact == null) { var SBContact = {}; }
if (SBContact.SBShop == null) { SBContact.SBShop = {}; }

SBContact.SBShop.Ribbon = (function (window, document) {
    
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

    function onRibbonClick() {
        SBContact.SBShop.Core.OpenExportToExcelModal();
    }

    function onSendExcelFileActionCall() {
        var viewName = document.getElementById("viewName").value;
        var logicalName = document.getElementById("logicalName").value;

        if (!viewName || !logicalName) {
            return;
        }

        SBContact.SBShop.Core.SendExcelFileActionCall(viewName, logicalName);
    }

    async function onLogicalNameChange() {
        var logicalNameDiv = document.getElementById("logicalName");
        var viewName = document.getElementById("viewName");
        var variantsDiv = document.getElementById("logicalNameVariants");

        if (logicalName.value.length >= 3) {
            var res = await SBContact.SBShop.Core.CallRetrieveLogicalNamesAction(logicalNameDiv.value);

            if (!!res.length) {
                variantsDiv.style.display = "flex";
                variantsDiv.innerHTML = "";
                for (var elem of res) {
                    const variant = document.createElement("div");
                    variant.classList.add("variant-elem");
                    variant.innerHTML = elem;

                    variant.onclick = function () {
                        logicalNameDiv.value = variant.innerText;
                        clearElem(variantsDiv);
                        viewName.disabled = false;
                    }

                    variantsDiv.appendChild(variant);
                }
            } else {
                clearElem(variantsDiv);
                viewName.disabled = true;
            }

        }
        else {
            clearElem(variantsDiv);
            viewName.disabled = true;
        }
    }

    async function onViewNameChange() {
        var viewNameDiv = document.getElementById("viewName");
        var logicalName = document.getElementById("logicalName").value;
        var button = document.getElementById("button");
        var variantsDiv = document.getElementById("viewNameVariants");

        if (viewNameDiv.value.length >= 3) {
            var res = await SBContact.SBShop.Core.CallRetrieveViewNamesAction(logicalName, viewNameDiv.value);

            if (!!res.length) {
                variantsDiv.style.display = "flex";
                variantsDiv.innerHTML = "";
                for (var elem of res) {
                    const variant = document.createElement("div");
                    variant.classList.add("variant-elem");
                    variant.innerHTML = elem;

                    variant.onclick = function () {
                        viewNameDiv.value = variant.innerText;
                        clearElem(variantsDiv);
                        button.disabled = false;
                    }

                    variantsDiv.appendChild(variant);
                }
            } else {
                clearElem(variantsDiv);
                button.disabled = true;
            }

        }
        else {
            clearElem(variantsDiv);
            button.disabled = true;
        }
    }

    /************************************************************************************
    * Helpers
    ************************************************************************************/

    function clearElem(elem) {
        elem.style.display = "none";
        elem.innerHTML = "";
    }

    return {
        OnRibbonClick: onRibbonClick,
        OnSendExcelFileActionCall: onSendExcelFileActionCall,
        OnLogicalNameChange: onLogicalNameChange,
        OnViewNameChange: onViewNameChange,
    };
})(window, document);