using ProductCataLog.Lib.Models;
using System.Collections.Generic;

namespace ProductCataLog.Lib.ViewModels
{
    public class AccountLoginViewModel
    {
        public AccountLoginViewModel()
        {
            LoginMaster = new Login_Master();

        }
        public Login_Master LoginMaster { get; set; }
    }

    public class LoginMasterViewModel
    {
        public Login_Master login_Master { get; set; }

        public List<Login_Master> login_Masters { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public int intGlCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
