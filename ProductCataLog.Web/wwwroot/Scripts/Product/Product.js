$(document).ready(function () {
   
});

function ValidateData() {
    
    var chrActive = $("#chkchrActive").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlCategory = $("#ddlCategoryList").dxSelectBox('instance').option('value');
    if (ddlCategory == undefined || ddlCategory == null || ddlCategory == '' || ddlCategory == '0') {
        PopUpMessage('Please Select Category.', "fa fa-exclamation-circle popup_icon");
        $("#ddlCategoryList").focus();
        return false;
    }
    $("#ref_CategoryId").val(ddlCategory);

    if ($("#txtProductCode").val() == "") {
        PopUpMessage("Please Enter Product Code.", "fa fa-exclamation-circle popup_icon");
        $("#txtProductCode").focus();
        return false;
    }

    if ($("#txtProductName").val() == "") {
        PopUpMessage("Please Enter Product Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtProductName").focus();
        return false;
    }

    if ($("#txtProductName").val() == "") {
        PopUpMessage("Please Enter Product Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtProductName").focus();
        return false;
    }

    if ($("#txtProductShortDescription").val() == "") {
        PopUpMessage("Please Enter Short Description.", "fa fa-exclamation-circle popup_icon");
        $("#txtProductShortDescription").focus();
        return false;
    }

    if ($("#txtDescription").val() == "") {
        PopUpMessage("Please Enter Contant.", "fa fa-exclamation-circle popup_icon");
        $("#txtDescription").focus();
        return false;
    }
    if ($("#txtRankNumber").val() == "") {
        PopUpMessage("Please Enter Rank.", "fa fa-exclamation-circle popup_icon");
        $("#txtRankNumber").focus();
        return false;
    }

    if ($("#txtPRoductPrice").val() == "") {
        PopUpMessage("Please Enter Price.", "fa fa-exclamation-circle popup_icon");
        $("#txtPRoductPrice").focus();
        return false;
    }

    if ($("#txtDisplayPrice").val() == "") {
        PopUpMessage("Please Enter Display Price.", "fa fa-exclamation-circle popup_icon");
        $("#txtDisplayPrice").focus();
        return false;
    }


    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmProductDetail').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Product/Save_Product",
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
    $("#grdProductDetials").dxDataGrid("instance").exportToExcel(false);
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
    $("#txtRankNumber").val('');
    $("#chkchrActive").prop('checked', false);
    $("#chkShowOnHomePage").prop('checked', false);
    $("#txtDescription").summernote('code', '');
    $("#chrActive").val(true);
    $("#grdProductDetials").dxDataGrid('instance').refresh();
    $("#grdProductDetials").dxDataGrid('instance').clearFilter();
    $("#txtProductName").focus();
}

function editproductdata(e) {

    $("#ddlCategoryList").dxSelectBox("getDataSource").reload();

    setTimeout(function () {

        var ddlCategoryList = $("#ddlCategoryList").dxSelectBox('instance');
        ddlCategoryList.option('value', parseInt(e.row.data.ref_CategoryId));

    }, 100);
    $("#intGiCode").val(e.row.data.intGiCode);
    $("#Action").val('Update');
    $("#txtProductName").val(e.row.data.varProductName);
    $("#txtProductCode").val(e.row.data.varProductCode);
    $("#txtProductShortDescription").val(e.row.data.varShortDescription);
    $("#txtDescription").summernote('code', e.row.data.varLongDescription);
    $("#txtPRoductPrice").val(e.row.data.decOriginalPrice);
    $("#txtDisplayPrice").val(e.row.data.decDisplayPrice);
    $("#txtMetaKeyword").val(e.row.data.MetaKeyword);
    $("#txtRankNumber").val(e.row.data.RankNumber);
    $("#txtMetaDescription").val(e.row.data.MetaDescription);
    $("#ref_CategoryId").val(e.row.data.ref_CategoryId);
    $("#ProductPriceID").val(e.row.data.ProductPriceID);
    $("#chkchrActive").prop('checked', e.row.data.chrActive == 'Active' ? true : false);
    $("#chkShowOnHomePage").prop('checked', e.row.data.ShowOnHomePage);
}

