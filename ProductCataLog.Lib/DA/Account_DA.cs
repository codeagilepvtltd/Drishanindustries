using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Data;
using System.Text;

namespace ProductCataLog.Lib.DA
{
    internal class Account_DA
    {
        private StringBuilder sqlQuery;
        private DataSet resultSet;
        public DataSet Check_Login(string UserId, string Password)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varUserID", "varPassword" };
            object[] objParamValue = { UserId, Password };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Check_Login, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet Select_MenuMasterList(string chrMenuType)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "chrMenuType" };
            object[] objParamValue = { chrMenuType };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_MenuMasterList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet Update_Password(ChangePasswordViewModel changePasswordViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varPassword", "varNewPassword" };
            object[] objParamValue = { changePasswordViewModel.intGlCode, changePasswordViewModel.Password, changePasswordViewModel.NewPassword };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Update_ChangePassword, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet InsertUpdate_LoginMaster(LoginMasterViewModel loginMasterViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varUserName", "varMobileNo", "varEmailID", "varPassword", "chrLock", "chrActive", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { loginMasterViewModel.login_Master.intGlCode,loginMasterViewModel.login_Master.varUserName,loginMasterViewModel.login_Master.varMobileNo,
            loginMasterViewModel.login_Master.varEmailID,loginMasterViewModel.login_Master.varPassword,loginMasterViewModel.login_Master.chrLock,loginMasterViewModel.login_Master.chrActive,
            loginMasterViewModel.login_Master.ref_EntryBy,loginMasterViewModel.login_Master.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Login_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
        public DataSet GetLoginMasterList(int AddId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { AddId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_LoginMasterList, objParamName, objParamValue);

            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_LoginDetails(int intGlCode, int ref_EntryBy, string varSystemIP, string varSystemName, string chrFlag)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "ref_EntryBy", "varSystemIP", "varSystemName", "chrFlag" };
            object[] objParamValue = { intGlCode, ref_EntryBy, varSystemIP, varSystemName, chrFlag };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_LoginDetails, objParamName, objParamValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultSet;
        }

    }
}
