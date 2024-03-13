$(document).ready(function () {

});

function editproductinquirydata(e) {

    $("#ddlLookupTypeList").dxSelectBox("getDataSource").reload();
    setTimeout(function () {

        var ddlLookupTypeList = $("#ddlLookupTypeList").dxSelectBox('instance');
        ddlLookupTypeList.option('value', parseInt(e.row.data.fk_LookupType_DetailsId));

    }, 100);
    $("#intGICode").val(e.row.data.intGICode);
    $("#fk_LookupType_DetailsId").val(e.row.data.fk_LookupType_DetailsId);
    $("#Action").val('Update');
    $("#txtContent").val(e.row.data.varContent);
}

function resetValidation() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $("#ddlLookupTypeList").dxSelectBox('instance').option('value', "0");
    $("#intGICode").val('');
    $("#fk_LookupType_DetailsId").val('');
    $("#txtContent").val('');
    $("#grdProductInquiryUpdate").dxDataGrid('instance').refresh();
    $("#grdProductInquiryUpdate").dxDataGrid('instance').clearFilter();
}

function ValidateData() {

    var ddlLookupType = $("#ddlLookupTypeList").dxSelectBox('instance').option('value');
    if (ddlLookupType == undefined || ddlLookupType == null || ddlLookupType == '' || ddlLookupType == '0') {
        PopUpMessage('Please Select Status.', "fa fa-exclamation-circle popup_icon");
        $("#ddlLookupTypeList").focus();
        return false;
    }
    $("#fk_LookupType_DetailsId").val(ddlLookupType);

    if ($("#txtContent").val() == "") {
        PopUpMessage("Please Enter Feedback.", "fa fa-exclamation-circle popup_icon");
        $("#txtContent").focus();
        return false;
    }
    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmProductInquiry').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Product/Save_ProductInquiry",
            success: function (response) {

                if (response.Unauthorized == "401") {
                    window.location.href = configuration.onLoad() + 'Home';
                }
                else if (response.Table[0].intStatus == 0) {
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else {
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
                    resetValidation();
                    $("#grdProductInquiryUpdate").dxDataGrid('instance').refresh();
                    $("#ddlLookupTypeList").dxSelectBox('getDataSource').reload();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
    }, 1000);
}
function ExportExcel() {
    $("#grdProductInquiryUpdate").dxDataGrid("instance").exportToExcel(false);
}

