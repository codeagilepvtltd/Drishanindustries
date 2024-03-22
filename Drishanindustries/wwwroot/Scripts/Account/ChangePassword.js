$(document).ready(function () {

});

function ValidateData()
{

    if ($("#txtUserName").val() == "")
    {
        PopUpMessage("Please Enter User Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtUserName").focus();
        return false;
    }

    if ($("#txtOldPassword").val() == "")
    {
        PopUpMessage("Please Enter Old Password.", "fa fa-exclamation-circle popup_icon");
        $("#txtOldPassword").focus();
        return false;
    }

    if ($("#txtNewPassword").val() == "")
    {
        PopUpMessage("Please Enter New password.", "fa fa-exclamation-circle popup_icon");
        $("#txtNewPassword").focus();
        return false;
    }

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmChangePassword').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Account/ChangePassword",
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

    $('#txtOldPassword').val('');
    $('#txtNewPassword').val('');
 
}