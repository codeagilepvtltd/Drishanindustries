$(document).ready(function () {

});

function ValidateData() {

    var ddlCategory = $("#ddlCategoryList").dxSelectBox('instance').option('value');
    $("#ref_ParentID").val(ddlCategory);

    var chrActive = $("#chkchrActive").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    if ($("#txtCategoryCode").val() == "") {
        PopUpMessage("Please Enter Category Code.", "fa fa-exclamation-circle popup_icon");
        $("#txtCategoryCode").focus();
        return false;
    }

    if ($("#txtCategoryName").val() == "") {
        PopUpMessage("Please Enter Category Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtCategoryName").focus();
        return false;
    }
    if ($("#txtRankNumber").val() == "") {
        PopUpMessage("Please Enter Rank Number.", "fa fa-exclamation-circle popup_icon");
        $("#txtRankNumber").focus();
        return false;
    }


    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmCategoryDetail').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Product/Save_Category",
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
                    $("#grdCategoryDetials").dxDataGrid('instance').refresh();
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
    $("#grdCategoryDetials").dxDataGrid("instance").exportToExcel(false);
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
    $("#intGlCode").val('0');
    $("#Action").val('Insert');
    $("#txtMetaDescription").val('');

    $("#chkchrActive").prop('checked', false);

    $("#chrActive").val(true);
    $("#grdCategoryDetials").dxDataGrid('instance').refresh();
    $("#grdCategoryDetials").dxDataGrid('instance').clearFilter();

}


function editdata(e) {

    $("#ddlCategoryList").dxSelectBox("getDataSource").reload();

    setTimeout(function () {

        var ddlCategoryList = $("#ddlCategoryList").dxSelectBox('instance');
        ddlCategoryList.option('value', parseInt(e.row.data.ref_ParentID));

    }, 100);
    $("#intGlCode").val(e.row.data.intGlCode);
    $("#Action").val('Update');
    $("#txtCategoryName").val(e.row.data.varCatergoryName);
    $("#txtCategoryCode").val(e.row.data.varCatergoryCode);
    $("#txtMetaKeyword").val(e.row.data.MetaKeyword);
    debugger;
    $("#txtRankNumber").val(e.row.data.RankNumber);
    $("#txtMetaDescription").val(e.row.data.MetaDescription);
    $("#ref_ParentID").val(e.row.data.ref_ParentID);
    $("#chkchrActive").prop('checked', e.row.data.chrActive == 'Active' ? true : false);
    $("#txtCategoryName").focus();
}