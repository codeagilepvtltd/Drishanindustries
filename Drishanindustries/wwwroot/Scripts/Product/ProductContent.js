

$(document).ready(function () {

});

function ValidateData() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlProduct = $("#ddlGetProductList").dxSelectBox('instance').option('value');
    if (ddlProduct == undefined || ddlProduct == null || ddlProduct == '' || ddlProduct == '0') {
        PopUpMessage('Please Select Category.', "fa fa-exclamation-circle popup_icon");
        $("#ddlGetProductList").focus();
        return false;
    }
    $("#fk_ProductID").val(ddlProduct);

    var ddlContentType = $("#ddlContentTypeList").dxSelectBox('instance').option('value');
    if (ddlContentType == undefined || ddlContentType == null || ddlContentType == '' || ddlContentType == '0') {
        PopUpMessage('Please Select Category.', "fa fa-exclamation-circle popup_icon");
        $("#ddlContentTypeList").focus();
        return false;
    }
    $("#fk_ContentTypeID").val(ddlContentType);

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmGalleryMaster').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Product/Save_Gallery",
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
                    $("#grdProductDetials").dxDataGrid('instance').refresh();
                    $("#ddlCategoryList").dxSelectBox('getDataSource').reload();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}
function ExportExcel() {
    $("#frmGalleryMaster").dxDataGrid("instance").exportToExcel(false);
}

function resetValidation() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $("#ddlCategoryList").dxSelectBox('instance').option('value', "0");
    $('input:text').val('');
    $('textarea').val('');
    $("#intGiCode").val('0');
    $("#Action").val('Insert');
    $("#txtMetaDescription").val('');
    $("#chkStatus").prop('checked', false);
    $("#txtDescription").summernote('code', '');
    $("#chrActive").val(true);
    $("#grdProductDetials").dxDataGrid('instance').refresh();
    $("#grdProductDetials").dxDataGrid('instance').clearFilter();

}

function editdata(e) {

    $("#ddlCategoryList").dxSelectBox("getDataSource").reload();

    setTimeout(function () {

        var ddlCategoryList = $("#ddlCategoryList").dxSelectBox('instance');
        ddlCategoryList.option('value', parseInt(e.row.data.ref_CategoryId));

    }, 100);
    $("#intGiCode").val(e.row.data.intGiCode);
    $("#Action").val('Update');
    $("#txtImagetitle").val(e.row.data.varProductName);
    $("#txtProductCode").val(e.row.data.varProductCode);
    $("#txtProductShortDescription").val(e.row.data.varShortDescription);
    $("#txtDescription").summernote('code', e.row.data.varLongDescription);
    $("#txtPRoductPrice").val(e.row.data.decOriginalPrice);
    $("#txtDisplayPrice").val(e.row.data.decDisplayPrice);
    $("#txtMetaKeyword").val(e.row.data.MetaKeyword);
    $("#txtMetaDescription").val(e.row.data.MetaDescription);
    $("#ref_CategoryId").val(e.row.data.ref_CategoryId);
    $("#ProductPriceID").val(e.row.data.ProductPriceID);
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Active' ? true : false);

}

