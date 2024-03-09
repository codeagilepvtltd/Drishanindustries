

$(document).ready(function () {

});
function ExportExcel() {
    $("#grdNewsDetials").dxDataGrid("instance").exportToExcel(false);
}

function ValidateData() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    if ($("#txtImagetitle").val() == "") {
        PopUpMessage("Please Enter News Title.", "fa fa-exclamation-circle popup_icon");
        $("#txtImagetitle").focus();
        return false;
    }
    if ($("#txtNewsShortDescription").val() == "") {
        PopUpMessage("Please Enter Short Description.", "fa fa-exclamation-circle popup_icon");
        $("#txtNewsShortDescription").focus();
        return false;
    }

    if ($("#txtNewsDescription").val() == "") {
        PopUpMessage("Please Enter Description.", "fa fa-exclamation-circle popup_icon");
        $("#txtNewsDescription").focus();
        return false;
    }    
}

function resetValidation() {    
    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $('input:text').val('');
    $('input:file').val('');
    $('textarea').val('');
    $("#ref_ContentTypeId").val('0');
    $("#ref_ContentId").val('0');
    $("#ref_GalleryId").val('0');
    $("#Action").val('Insert');
    //$("#txtMetaDescription").val('');
    $("#chkStatus").prop('checked', false);
    $("#txtNewsDescription").summernote('code', '');
    $("#chrActive").val(true);
    $("#grdNewsDetials").dxDataGrid('instance').refresh();
    $("#grdNewsDetials").dxDataGrid('instance').clearFilter();
    var my_images = document.getElementById("my_images");
    my_images.style.display = "none";

}

function editdata(e)
{
    debugger;
    $("#ref_ContentTypeId").val(e.row.data.CTM_intGlCode);
    $("#ref_ContentId").val(e.row.data.CM_intGlCode);
    $("#varGalleryType").val(e.row.data.varGalleryType);
    $("#ref_GalleryId").val(e.row.data.intGICode);
    $("#varGalleryPath").val(e.row.data.varGalleryPath);
    $("#Action").val('Update');
    $("#txtImagetitle").val(e.row.data.CM_varTitle);
    $("#txtNewsShortDescription").val(e.row.data.CM_varShortDescription);
    $("#txtNewsDescription").summernote('code', e.row.data.CM_varContent);
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
