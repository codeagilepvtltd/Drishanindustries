$(document).ready(function () {

});

function ValidateData() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlConfigType = $("#ddlConfigType").dxSelectBox('instance').option('value');
    if (ddlConfigType == undefined || ddlConfigType == null || ddlConfigType == '' || ddlConfigType == '0') {
        PopUpMessage('Please Select Config Type.', "fa fa-exclamation-circle popup_icon");
        $("#ddlConfigType").focus();
        return false;
    }
    $("#ref_ConfigurationID").val(ddlConfigType);

    if ($("#txtConfigName").val() == "") {
        PopUpMessage("Please Enter Config Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtConfigName").focus();
        return false;
    }

    if ($("#txtValue1").val() == "") {
        PopUpMessage("Please Enter Value 1.", "fa fa-exclamation-circle popup_icon");
        $("#txtValue1").focus();
        return false;
    }


    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmConfigDetail').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Utility/Save_ConfigDetails",

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
                    $("#grdConfigDetials").dxDataGrid('instance').refresh();
                    $("#ddlConfigType").dxSelectBox('getDataSource').reload();
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
    $("#grdConfigDetials").dxDataGrid("instance").exportToExcel(false);
}

function resetValidation() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    $("#ddlConfigType").dxSelectBox('instance').option('value', "0");
    $('input:text').val('');
    $('textarea').val('');
    $("#intGICode").val('0');
    $("#Action").val('Insert');
    $("#chkStatus").prop('checked', false);
    $("#chrActive").val(true);
    $("#grdConfigDetials").dxDataGrid('instance').refresh();
    $("#grdConfigDetials").dxDataGrid('instance').clearFilter();

}

function editdata(e) {

    $("#ddlConfigType").dxSelectBox("getDataSource").reload();
    setTimeout(function () {

        var ddlConfigType = $("#ddlConfigType").dxSelectBox('instance');
        ddlConfigType.option('value', parseInt(e.row.data.ref_ConfigurationID));

    }, 100);
    $("#intGICode").val(e.row.data.intGICode);
    $("#Action").val('Update');
    $("#txtConfigName").val(e.row.data.varName);
    $("#txtValue1").val(e.row.data.varValue1);
    $("#txtValue2").val(e.row.data.varValue2);
    $("#txtValue3").val(e.row.data.varValue3);
    $("#ref_ConfigurationID").val(e.row.data.ref_ConfigurationID);
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Active' ? true : false);
}

