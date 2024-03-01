$(document).ready(function () {

});

function ValidateData() {

    var chrstatus = $("#chkStatus").prop('checked');
    $("#chkStatus").val(chrstatus == false ? 'InActive' : 'Active');

    var chrLock = $("#chkLock").prop('checked');
    $("#chkLock").val(chrLock == false ? 'No' : 'Yes');

    if ($("#txtUserName").val() == "") {
        PopUpMessage("Please Enter User Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtUserName").focus();
        return false;
    }

    if ($("#txtpassword").val() == "") {
        PopUpMessage("Please Enter Password.", "fa fa-exclamation-circle popup_icon");
        $("#txtpassword").focus();
        return false;
    }

    if ($("#txtMobileNo").val() == "") {
        PopUpMessage("Please Enter Mobile Number.", "fa fa-exclamation-circle popup_icon");
        $("#txtMobileNo").focus();
        return false;
    }

    var mobileNumber = $("#txtMobileNo").val();
    var mobileNumberPattern = /^[0-9]{10}$/;

    if (!mobileNumberPattern.test(mobileNumber)) {
        PopUpMessage("Please enter a 10 digit mobile number.", "fa fa-exclamation-circle popup_icon");
        return false;
    }

    if ($("#txtEmailid").val() == "") {
        PopUpMessage("Please Enter Email Id.", "fa fa-exclamation-circle popup_icon");
        $("#txtEmailid").focus();
        return false;
    }   

    var emailId = $("#txtEmailid").val();
    var emailIdPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    if (!emailIdPattern.test(emailId)) {
        PopUpMessage("Please enter a valid Email Id.", "fa fa-exclamation-circle popup_icon");
        return false;
    }

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmUsers').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Account/Save_LoginMaster",
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
                    $("#grdUsersList").dxDataGrid('instance').refresh();                    
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
    $("#grdUsersList").dxDataGrid("instance").exportToExcel(false);
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
    $('input:password').val('');
    $('#txtMobileNo').val('');
    $("#intGlCode").val('0');
    $("#Action").val('Insert');

    $("#chkStatus").prop('checked', false);
    $("#chkLock").prop('checked', false);
    $("#chrActive").val(true);
    $("#chrLock").val(false);
    $("#grdUsersList").dxDataGrid('instance').refresh();
    $("#grdUsersList").dxDataGrid('instance').clearFilter();

}

function editdata(e) {

    $("#intGlCode").val(e.row.data.intGlCode);
    $("#Action").val('Update');
    $("#txtUserName").val(e.row.data.varUserName);
    $("#txtpassword").val(e.row.data.varPassword);
    $("#txtMobileNo").val(e.row.data.varMobileNo);
    $("#txtEmailid").val(e.row.data.varEmailID);
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Active' ? true : false);
    $("#chkLock").prop('checked', e.row.data.chrActive == 'Yes' ? true : false);
}