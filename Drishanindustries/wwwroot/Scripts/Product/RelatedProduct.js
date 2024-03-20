$(document).ready(function () {

});

function ReloadRelatedProductddl() {
    $("#ddlGetRelatedProductList").dxSelectBox("getDataSource").reload();
    $("#ddlGetRelatedProductList").dxSelectBox('instance').option('value', "0");
}

function dxSelectBox_OnOpenedstateDDl(ev) {
    var list = ev.component._list;
    list.option('useNativeScrolling', true);
    list._scrollView.option('useNative', true);
    list.reload();
}


function resetValidation() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $("#ddlGetProductList").dxSelectBox('instance').option('value', "0");
    $("#ddlGetRelatedProductList").dxSelectBox('instance').option('value', "0");
    $("#ProductId").val('');
    
    $("#grdRelatedProduct").dxDataGrid('instance').refresh();
    $("#grdRelatedProduct").dxDataGrid('instance').clearFilter();
}

function ValidateData() {

    var ddlGetProduct = $("#ddlGetProductList").dxSelectBox('instance').option('value');
    if (ddlGetProduct == undefined || ddlGetProduct == null || ddlGetProduct == '' || ddlGetProduct == '0') {
        PopUpMessage('Please Select Product.', "fa fa-exclamation-circle popup_icon");
        $("#ddlGetProductList").focus();
        return false;
    }
    $("#ref_OriginalProductID").val(ddlGetProduct);

    var ddlGetRelatedProduct = $("#ddlGetRelatedProductList").dxSelectBox('instance').option('value');
    if (ddlGetRelatedProduct == undefined || ddlGetRelatedProduct == null || ddlGetRelatedProduct == '' || ddlGetRelatedProduct == '0') {
        PopUpMessage('Please Select Related Product.', "fa fa-exclamation-circle popup_icon");
        $("#ddlGetRelatedProductList").focus();
        return false;
    }
    $("#ref_RelatedProductID").val(ddlGetRelatedProduct);

    $("#charActive").val("Y");
    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmRelatedProduct').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Product/Save_RelatedProduct",
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
                    $("#grdRelatedProduct").dxDataGrid('instance').refresh();
                    $("#ddlGetProductList").dxSelectBox('getDataSource').reload();
                    $("#ddlGetRelatedProductList").dxSelectBox('getDataSource').reload();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
    }, 1000);
}
function ExportExcel() {
    $("#grdRelatedProduct").dxDataGrid("instance").exportToExcel(false);
}

function deletedata(e) {
    $("#intGICode").val(e.row.data.intGICode);
    $("#charActive").val("N");
    $("#ref_OriginalProductID").val(e.row.data.ref_OriginalProductID);
    $("#ref_RelatedProductID").val(e.row.data.ref_RelatedProductID);

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmRelatedProduct').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Product/Save_RelatedProduct",
            success: function (response) {

                if (response.Unauthorized == "401") {
                    window.location.href = configuration.onLoad() + 'Home';
                }
                else if (response.Table[0].intStatus == 0) {
                    alert("1");
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else {
                    alert("2");
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");                    
                    $("#grdRelatedProduct").dxDataGrid('instance').refresh();                   
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
    }, 1000);
}