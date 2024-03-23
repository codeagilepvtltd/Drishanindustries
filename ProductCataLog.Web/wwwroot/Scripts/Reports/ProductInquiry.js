$(document).ready(function () {

});

function ValidateData() {

    var ddlLookupType = $("#ddlLookupTypeList").dxSelectBox('instance').option('value');
    if (ddlLookupType == undefined || ddlLookupType == null || ddlLookupType == '' || ddlLookupType == '0') {
        PopUpMessage('Please Select Status.', "fa fa-exclamation-circle popup_icon");
        $("#ddlLookupTypeList").focus();
        return false;
    }
    $("#fk_LookupType_DetailsId").val(ddlLookupType);

    var quotesGrid = $("#grdProductInquiry").dxDataGrid("instance");
    quotesGrid.filter(["fk_LookupType_DetailsId", "=", ddlLookupType]);
}
function ExportExcel() {
    $("#grdProductInquiry").dxDataGrid("instance").exportToExcel(false);
}

function resetValidation() {
    $("#ddlLookupTypeList").dxSelectBox('instance').option('value', "0");
    $("#grdProductInquiry").dxDataGrid('instance').refresh();
    $("#grdProductInquiry").dxDataGrid('instance').clearFilter();
}
