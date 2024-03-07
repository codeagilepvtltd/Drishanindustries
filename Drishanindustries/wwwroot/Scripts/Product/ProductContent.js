

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
    $("#ref_GalleryUrl").val($("#txtImageUrl").val());
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
                    $("#grdGalleryDetials").dxDataGrid('instance').refresh();
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
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $("#ddlGetProductList").dxSelectBox('instance').option('value', "0");
    $("#ddlContentTypeList").dxSelectBox('instance').option('value', "0");
    $('input:text').val('');
    $('input:file').val('');
    $('textarea').val('');
    $("#ref_ContentId").val('0');
    $("#fk_ProductID").val('0');
    $("#fk_ContentTypeID").val('0');
    $("#ref_GalleryId").val('0');
    $("#Action").val('Insert');
    $("#chkStatus").prop('checked', false);
    $("#chrActive").val(true);
    $("#grdGalleryDetials").dxDataGrid('instance').refresh();
    $("#grdGalleryDetials").dxDataGrid('instance').clearFilter();

}

function editdata(e) {

    $("#ddlGetProductList").dxSelectBox("getDataSource").reload();
    $("#ddlContentTypeList").dxSelectBox("getDataSource").reload();
    setTimeout(function () {

        var ddlGetProductList = $("#ddlGetProductList").dxSelectBox('instance');
        ddlGetProductList.option('value', parseInt(e.row.data.fk_ProductID));

        var ddlContentTypeList = $("#ddlContentTypeList").dxSelectBox('instance');
        ddlContentTypeList.option('value', parseInt(e.row.data.fk_ContentTypeID));

    }, 100);

    $("#Action").val('Update');
    $("#txtImagetitle").val(e.row.data.varTitle);
    $("#txtImageShortDescription").val(e.row.data.varShortDescription);
    $("#txtImageDescription").val(e.row.data.varContentDescription);
    $("#ref_GalleryUrl").val(e.row.data.varGalleryURL);

    $("#ref_ContentId").val(e.row.data.fk_ContentID);
    $("#fk_ProductID").val(e.row.data.fk_ProductID);
    $("#fk_ContentTypeID").val(e.row.data.fk_ContentTypeID);
    $("#ref_GalleryId").val(e.row.data.intGICode);


    $("#chkStatus").prop('checked', e.row.data.charActive == 'Active' ? true : false);

}

function GetSelectedContentType(e) {
    var image = document.getElementById("image");
    var documents = document.getElementById("document");
    var video = document.getElementById("video");

    var selectedItem = $("#ddlContentTypeList").dxSelectBox('instance').option('selectedItem');
    $("#ref_GalleryType").val(selectedItem.varContentType);
    if (selectedItem.varContentType == "Video") {
        image.style.display = "none";
        documents.style.display = "none";
        video.style.display = "block";
    }
    else if (selectedItem.varContentType == "Document") {
        image.style.display = "none";
        documents.style.display = "block";
        video.style.display = "none";
    }
    else if (selectedItem.varContentType == "Gallery") {
        image.style.display = "block";
        documents.style.display = "none";
        video.style.display = "none";
    }
}