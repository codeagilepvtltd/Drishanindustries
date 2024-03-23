

$(document).ready(function () {

});
function ExportExcel() {
    $("#grdBlogsDetials").dxDataGrid("instance").exportToExcel(false);
}

function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();
    debugger;
    for (var i = 0; i !== files.length; i++) {
        formData.append("files", files[i]);
    }
}
function ValidateData() {

    debugger;
    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlContentType = $("#ddlContentTypeList").dxSelectBox('instance').option('value');
    var ddlContentTypeText = $("#ddlContentTypeList").dxSelectBox('instance').option('text');
    if (ddlContentType == undefined || ddlContentType == null || ddlContentType == '' || ddlContentType == '0') {
        PopUpMessage('Please Select Content type.', "fa fa-exclamation-circle popup_icon");
        $("#ddlContentTypeList").focus();
        return false;
    }
    $("#fk_ContentTypeID").val(ddlContentType);
    $("#varGalleryType").val(ddlContentTypeText);

    if ($("#txtImagetitle").val() == "") {
        PopUpMessage("Please Enter Title.", "fa fa-exclamation-circle popup_icon");
        $("#txtImagetitle").focus();
        return false;
    }
    if ($("#txtBlogShortDescription").val() == "") {
        PopUpMessage("Please Enter Short Description.", "fa fa-exclamation-circle popup_icon");
        $("#txtBlogShortDescription").focus();
        return false;
    }

    if ($("#txtBlogDescription").val() == "") {
        PopUpMessage("Please Enter Description.", "fa fa-exclamation-circle popup_icon");
        $("#txtBlogDescription").focus();
        return false;
    }
    //setTimeout(function () {
    //    $.ajax({
    //        type: "POST",
    //        data: $('#frmBlogsDetail').serialize(),
    //        timeout: 15000, // adjust the limit. currently its 15 seconds
    //        url: configuration.onLoad() + "Utility/Save_Gallery",
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
    //            }
    //        },
    //        error: function (error) {
    //            PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
    //        }
    //    });
    //    //$('#loading').fadeOut();
    //}, 1000);
}

function resetValidation() {    
    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $("#ddlContentTypeList").dxSelectBox('instance').option('value', "0");
    $('input:text').val('');
    $('input:file').val('');
    $('textarea').val('');
    $("#ref_ContentTypeId").val('0');
    $("#ref_ContentId").val('0');
    $("#ref_GalleryId").val('0');
    $("#Action").val('Insert');
    $("#fk_ContentTypeID").val('0');
    //$("#txtMetaDescription").val('');
    $("#chkStatus").prop('checked', false);
    $("#txtBlogDescription").summernote('code', '');
    $("#chrActive").val(true);
    $("#grdBlogsDetials").dxDataGrid('instance').refresh();
    $("#grdBlogsDetials").dxDataGrid('instance').clearFilter();
    var my_images = document.getElementById("my_images");
    my_images.style.display = "none";

}

function editdata(e)
{
    $("#ddlContentTypeList").dxSelectBox("getDataSource").reload();

    setTimeout(function () {
        var ddlContentTypeList = $("#ddlContentTypeList").dxSelectBox('instance');
        ddlContentTypeList.option('value', parseInt(e.row.data.fk_ContentTypeID));
    }, 1000);

    $("#ref_ContentTypeId").val(e.row.data.CTM_intGlCode);
    $("#ref_ContentId").val(e.row.data.CM_intGlCode);
    $("#varGalleryType").val(e.row.data.varGalleryType);
    $("#ref_GalleryId").val(e.row.data.intGICode);
    $("#varGalleryPath").val(e.row.data.varGalleryPath);
    $("#fk_ContentTypeID").val(e.row.data.fk_ContentTypeID);
    $("#Action").val('Update');
    $("#txtImagetitle").val(e.row.data.CM_varTitle);
    $("#txtBlogShortDescription").val(e.row.data.CM_varShortDescription);
    $("#txtBlogDescription").summernote('code', e.row.data.CM_varContent);
    $("#chkStatus").prop('checked', e.row.data.charActive == 'Active' ? true : false);

    var my_images = document.getElementById("my_images");
    var my_images1 = document.getElementById("my_images1");

    if (e.row.data.varGalleryPath != '')
    {
        my_images.style.display = "block";
        my_images.href = e.row.data.varGalleryPath;
        my_images1.src = e.row.data.varGalleryPath;
    }
    else {
        my_images.style.display = "none";
        my_images.href = "";
        my_images1.src = "";
    }
    
   

}
