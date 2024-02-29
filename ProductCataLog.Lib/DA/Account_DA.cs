using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.ViewModels;
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
            object[] objParamValue = { UserId, Password};

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

        public DataSet InsertUpdate_Login_Master(LoginMasterViewModel loginMasterViewModel)
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
    }
}
