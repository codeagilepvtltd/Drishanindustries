using ProductCataLog.Lib.DA;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProductCataLog.Lib.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        #region Login
        public AccountLoginViewModel CheckAuthentication(AccountLoginViewModel accountLoginViewModel)
        {          
            Account_DA accountDA = new Account_DA();

            try
            {
                DataSet dsResult = accountDA.Check_Login(accountLoginViewModel.LoginMaster.varUserName, accountLoginViewModel.LoginMaster.varPassword);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    accountLoginViewModel.LoginMaster.varUserName = Convert.ToString(dsResult.Tables[0].Rows[0]["varUserName"]);
                    accountLoginViewModel.LoginMaster.varPassword = Convert.ToString(dsResult.Tables[0].Rows[0]["varPassword"]);
                }
                else
                {
                    accountLoginViewModel.LoginMaster.varUserName = "";
                }
            }
            catch
            {
                throw;
            }
            return accountLoginViewModel;
        }
        #endregion

        #region Login Master
        public DataSet InsertUpdate_Login_Master(LoginMasterViewModel loginMasterViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_Login_Master(loginMasterViewModel);
            }
            catch
            {
                throw;
            }
        }

        #endregion


    }
}
