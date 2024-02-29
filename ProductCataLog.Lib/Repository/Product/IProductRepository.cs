using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace ProductCataLog.Lib.Repository.Product
{
    public interface IProductRepository
    {
        #region Category
        List<Category_Master> GetCategoryList(int intGlCode = 0);

        DataSet InsertUpdate_category(CategoryMasterViewModel categoryViewModel);

        #endregion
    }
}
