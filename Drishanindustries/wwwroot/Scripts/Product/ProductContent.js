

$(document).ready(function () {

});

function ValidateData() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlProduct = $("#ddlGetProductList").dxSelectBox('instance').option('value');
    if (ddlProduct == undefined || ddlProduct == null || ddlProduct == '' || ddlProduct == '0') {
        PopUpMessage('Please Select Product.', "fa fa-exclamation-circle popup_icon");
        $("#ddlGetProductList").focus();
        return false;
    }
    $("#fk_ProductID").val(ddlProduct);

    var ddlContentType = $("#ddlContentTypeList").dxSelectBox('instance').option('value');
    if (ddlContentType == undefined || ddlContentType == null || ddlContentType == '' || ddlContentType == '0') {
        PopUpMessage('Please Select Content type.', "fa fa-exclamation-circle popup_icon");
        $("#ddlContentTypeList").focus();
        return false;
    }
    $("#fk_ContentTypeID").val(ddlContentType);

    var selectedItem = $("#ddlContentTypeList").dxSelectBox('instance').option('selectedItem');

    if (selectedItem.varContentType == "Gallery") {
        if ($("#txtImagetitle").val() == "") {
            PopUpMessage("Please Enter Content Title.", "fa fa-exclamation-circle popup_icon");
            $("#txtImagetitle").focus();
            return false;
        }
        
        $("#ref_GalleryUrl").val($("#txtImageUrl").val());
        $("#ref_ContentShortDesc").val($("#txtImageShortDescription").val());
        $("#ref_ContentDesc").val($("#txtImageDescription").val());
        $("#ref_ContentName").val($("#txtImagetitle").val());
    }

    if (selectedItem.varContentType == "Video") {
        if ($("#txtVideoTitle").val() == "") {
            PopUpMessage("Please Enter Content Title.", "fa fa-exclamation-circle popup_icon");
            $("#txtVideoTitle").focus();
            return false;
        }
       
        $("#ref_GalleryUrl").val($("#txtVideoUrl").val());
        $("#ref_ContentShortDesc").val($("#txtVideoShortDescription").val());
        $("#ref_ContentDesc").val($("#txtVideoDescription").val());
        $("#ref_ContentName").val($("#txtVideoTitle").val());
    }

    if (selectedItem.varContentType == "Document") {
        if ($("#txtDocuementTitle").val() == "") {
            PopUpMessage("Please Enter Content Title.", "fa fa-exclamation-circle popup_icon");
            $("#txtDocuementTitle").focus();
            return false;
        }
       
        $("#ref_GalleryUrl").val($("#txtDocumentUrl").val());
        $("#ref_ContentShortDesc").val($("#txtDocumentShortDescription").val());
        $("#ref_ContentDesc").val($("#txtDocumentDescription").val());
        $("#ref_ContentName").val($("#txtDocumentTitle").val());
    }

    //setTimeout(function () {
    //    $.ajax({
    //        type: "POST",
    //        data: $('#frmGalleryMaster').serialize(),
    //        timeout: 15000, // adjust the limit. currently its 15 seconds
    //        url: configuration.onLoad() + "Product/Save_Gallery",
    //        success: function (response) {

    //            if (response.Unauthorized == "401") {
    //                window.location.href = configuration.onLoad() + 'Home';
    //            }
    //            else if (response.Table[0].intStatus == 0) {
    //                PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
    //            }
    //            else {
    //                PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
    //                resetValidation();
    //                $("#grdGalleryDetials").dxDataGrid('instance').refresh();
    //            }
    //        },
    //        error: function (error) {
    //            PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
    //        }
    //    });
    //    //$('#loading').fadeOut();
    //}, 1000);
}
function ExportExcel() {
    $("#grdGalleryDetials").dxDataGrid("instance").exportToExcel(false);
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
    $("#txtImageDescription").summernote('code', '');
    $("#txtVideoDescription").summernote('code', '');
    $("#txtDocumentDescription").summernote('code', '');
    $("#ref_ContentId").val('0');
    $("#fk_ProductID").val('0');
    $("#fk_ContentTypeID").val('0');
    $("#ref_GalleryId").val('0');
    $("#Action").val('Insert');
    $("#chkStatus").prop('checked', false);
    $("#chrActive").val(false);
    $("#grdGalleryDetials").dxDataGrid('instance').refresh();
    $("#grdGalleryDetials").dxDataGrid('instance').clearFilter();

    var my_images = document.getElementById("my_images");
    my_images.style.display = "none";

    var my_Docs = document.getElementById("my_Docs");
    my_Docs.style.display = "none";

}
function editdata(e) {

    $("#ddlContentTypeList").dxSelectBox("getDataSource").reload();
    $("#ddlGetProductList").dxSelectBox("getDataSource").reload();
    setTimeout(function () {
        var ddlContentTypeList = $("#ddlContentTypeList").dxSelectBox('instance');
        ddlContentTypeList.option('value', parseInt(e.row.data.fk_ContentTypeID));

        var ddlGetProductList = $("#ddlGetProductList").dxSelectBox('instance');
        ddlGetProductList.option('value', parseInt(e.row.data.fk_ProductID));

    }, 1000);
    
    $("#chkStatus").prop('checked', e.row.data.charActive == 'Active' ? true : false);
    $("#Action").val('Update');

    $("#ref_ContentId").val(e.row.data.fk_ContentID);
    $("#fk_ProductID").val(e.row.data.fk_ProductID);
    $("#fk_ContentTypeID").val(e.row.data.fk_ContentTypeID);
    $("#ref_GalleryId").val(e.row.data.intGICode);
    $("#varGalleryPath").val(e.row.data.varGalleryPath);
    $("#ref_GalleryUrl").val(e.row.data.varGalleryURL);
    $("#ref_ContentShortDesc").val(e.row.data.varShortDescription);
    $("#ref_ContentDesc").val(e.row.data.varContentDescription);
    $("#ref_ContentName").val(e.row.data.varTitle);

    var my_Docs = document.getElementById("my_Docs");
    var my_images = document.getElementById("my_images");
    var my_images1 = document.getElementById("my_images1");

    my_Docs.style.display = "none";
    my_images.style.display = "none";

    if (e.row.data.varGalleryType == "Gallery") {

        $("#txtImagetitle").val($("#ref_ContentName").val());
        $("#txtImageShortDescription").val($("#ref_ContentShortDesc").val());
        $("#txtImageDescription").summernote('code',$("#ref_ContentDesc").val());
        alert(e.row.data.varGalleryPath);
        my_images.style.display = "block";
        my_images.href = e.row.data.varGalleryPath;
        my_images1.src = e.row.data.varGalleryPath;
    }

    else if (e.row.data.varGalleryType == "Video") {
        $("#txtVideoShortDescription").val($("#ref_ContentShortDesc").val());
        $("#txtVideoDescription").summernote('code', $("#ref_ContentDesc").val());
        $("#txtVideoTitle").val($("#ref_ContentName").val());
        $("#txtVideoUrl").val(e.row.data.varGalleryPath)
    }

    else if (e.row.data.varGalleryType == "Document") {
        $("#txtDocumentShortDescription").val($("#ref_ContentShortDesc").val());
        $("#txtDocumentDescription").summernote('code', $("#ref_ContentDesc").val());
        $("#txtDocumentTitle").val($("#ref_ContentName").val());
        
        my_Docs.style.display = "block";
        my_Docs.href = e.row.data.varGalleryPath;

    }

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