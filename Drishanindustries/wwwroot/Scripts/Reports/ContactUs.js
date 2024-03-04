$(document).ready(function () {

});

function ValidateData() {

    var ddlLookupType = $("#ddlLookupTypeList").dxSelectBox('instance').option('value');
    if (ddlLookupType == undefined || ddlLookupType == null || ddlLookupType == '' || ddlLookupType == '0') {
        PopUpMessage('Please Select Category.', "fa fa-exclamation-circle popup_icon");
        $("#ddlLookupTypeList").focus();
        return false;
    }
    $("#fk_LookupType_DetailsId").val(ddlLookupType);

    var quotesGrid = $("#grdContactUsDetials").dxDataGrid("instance");
    quotesGrid.filter(["fk_LookupType_DetailsId", "=", ddlLookupType]);
}
function ExportExcel() {
    $("#grdContactUsDetials").dxDataGrid("instance").exportToExcel(false);
}

function resetValidation() {
    $("#ddlLookupTypeList").dxSelectBox('instance').option('value', "0");
    $("#grdContactUsDetials").dxDataGrid('instance').refresh();
    $("#grdContactUsDetials").dxDataGrid('instance').clearFilter();
}
