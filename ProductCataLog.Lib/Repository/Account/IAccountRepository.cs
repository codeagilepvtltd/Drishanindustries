using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace ProductCataLog.Lib.Repository.Account
{
    public interface IAccountRepository
    {
        #region Login
        AccountLoginViewModel CheckAuthentication(AccountLoginViewModel accountLoginViewModel);

        #endregion

        #region Category
        List<Category_Master> GetCategoryList(int intGlCode = 0);

        DataSet InsertUpdate_category(CategoryMasterViewModel categoryViewModel);
        #endregion


    }
}
