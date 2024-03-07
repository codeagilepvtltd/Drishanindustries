

$(document).ready(function () {

});

function ValidateData() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    if ($("#txtImagetitle").val() == "") {
        PopUpMessage("Please Enter Blog Title.", "fa fa-exclamation-circle popup_icon");
        $("#txtImagetitle").focus();
        return false;
    }

    if ($("#txtBlogDescription").val() == "") {
        PopUpMessage("Please Enter Description.", "fa fa-exclamation-circle popup_icon");
        $("#txtBlogDescription").focus();
        return false;
    }
    $("#ref_GalleryUrl").val($("#txtBlogUrl").val());
    $("#ref_ContentTypeId").val('4');
    
    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmBlogsDetail').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Utility/Save_Gallery",
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
    $("#grdBlogsDetials").dxDataGrid("instance").exportToExcel(false);
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
    $('textarea').val('');
    $("#ref_ContentTypeId").val('0');
    $("#ref_ContentId").val('0');    
    $("#ref_GalleryId").val('0');
    $("#Action").val('Insert');
    //$("#txtMetaDescription").val('');
    $("#chkStatus").prop('checked', false);
    $("#txtBlogDescription").summernote('code', '');
    $("#chrActive").val(true);
    $("#grdBlogsDetials").dxDataGrid('instance').refresh();
    $("#grdBlogsDetials").dxDataGrid('instance').clearFilter();

}

function editdata(e) {
    
    $("#ref_ContentTypeId").val(e.row.data.CTM_intGlCode);
    $("#ref_ContentId").val(e.row.data.CM_intGlCode);
    $("#ref_GalleryId").val(e.row.data.PM_intGlCode);
    $("#Action").val('Update');
    $("#txtImagetitle").val(e.row.data.CM_varTitle);
    $("#txtBlogShortDescription").val(e.row.data.CM_varShortDescription);
    $("#txtBlogDescription").val(e.row.data.CM_varContent);
    $("#txtBlogUrl").summernote('code', e.row.data.varGalleryURL);    
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Active' ? true : false);

}

