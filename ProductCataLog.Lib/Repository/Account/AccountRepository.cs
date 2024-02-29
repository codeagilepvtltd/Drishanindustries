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
        public DataSet InsertUpdate_LoginMaster(LoginMasterViewModel loginMasterViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_LoginMaster(loginMasterViewModel);
            }
            catch
            {
                throw;
            }
        }

        public LoginMasterViewModel GetLoginMasterlist(int AddId = 0)
        {
            LoginMasterViewModel loginmasterViewModel = new LoginMasterViewModel();
            Account_DA accountDA = new Account_DA();

            try
            {
                loginmasterViewModel.login_Masters = new List<Login_Master>();
                DataSet dsResult = accountDA.GetLoginMasterList(AddId);
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    loginmasterViewModel.login_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new Login_Master()
                    {
                        intGlCode = row.Field<int>("intGlCode"),
                        varUserName = row.Field<string>("varUserName"),
                        varMobileNo = row.Field<string>("varMobileNo"),
                        varEmailID = row.Field<string>("varEmailID"),
                        varPassword = row.Field<string>("varPassword"),
                        chrLock = row.Field<string>("chrLock"),                        
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime?>("dtEntryDate"),
                        ref_EntryBy = row.Field<long?>("ref_EntryBy"),
                        dtUpdatedDate = row.Field<DateTime?>("dtUpdatedDate"),
                        ref_UpdateBy = row.Field<long?>("ref_UpdateBy")

                    }).ToList();

                }
                return loginmasterViewModel;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Category
        public List<Category_Master> GetCategoryList(int intGlCode = 0)
        {
            Account_DA accountDA = new Account_DA();
            List<Category_Master> category_Masters = new List<Category_Master>();

            try
            {
                DataSet dsResult = accountDA.GetCategoryList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    category_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new Category_Master()
                    {
                        intGlCode = row.Field<int>("intGICode"),
                        varCatergoryCode = row.Field<string>("varCatergoryCode"),
                        varCatergoryName = row.Field<string>("varCatergoryName"),
                        ParentCatergoryName = row.Field<string>("ParentCategory"),
                        ref_ParentID = row.Field<int>("ParentCategoryID"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return category_Masters;
        }

        public DataSet InsertUpdate_category(CategoryMasterViewModel categoryViewModel)
        {
            Account_DA accountDA = new Account_DA();
            try
            {
                return accountDA.InsertUpdate_Category(categoryViewModel);

            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
