using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.ViewModels;
using System.Data;
using System.Text;

namespace ProductCataLog.Lib.DA
{
    internal class Product_DA
    {
        private StringBuilder sqlQuery;
        private DataSet resultSet;

        public DataSet GetCategoryList(int CategoryId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { CategoryId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_CategoryList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_Category(CategoryMasterViewModel categoryViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varCatergoryCode", "varCatergoryName", "ref_ParentID", "chrActive", "MetaKeyword", "MetaDescription", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { categoryViewModel.category_Master.intGlCode, categoryViewModel.category_Master.varCatergoryCode, categoryViewModel.category_Master.varCatergoryName,
                      categoryViewModel.category_Master.ref_ParentID, categoryViewModel.category_Master.chrActive, categoryViewModel.category_Master.MetaKeyword,
                 categoryViewModel.category_Master.MetaDescription, categoryViewModel.category_Master.ref_EntryBy,
                categoryViewModel.category_Master.ref_UpdateBy };

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Category_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetProductList(int ProductId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { ProductId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ProductList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_Product(ProductMasterViewModel productViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "ref_ProductPriceID", "ref_CategoryId", "varProductCode", "varProductName",
                "varShortDescription","decOriginalPrice","decDisplayPrice","varLongDescription", "chrActive", "MetaKeyword", "MetaDescription", "ref_EntryBy", "ref_UpdateBy" };
            object[] objParamValue = { productViewModel.product_master.intGiCode,productViewModel.product_master.ProductPriceID,productViewModel.product_master.ref_CategoryId,
                      productViewModel.product_master.varProductCode,productViewModel.product_master.varProductName,productViewModel.product_master.varShortDescription,
                productViewModel.product_master.decOriginalPrice,productViewModel.product_master.decDisplayPrice,productViewModel.product_master.varLongDescription
            ,productViewModel.product_master.chrActive,productViewModel.product_master.MetaKeyword,
                productViewModel.product_master.MetaDescription,productViewModel.product_master.ref_EntryBy,productViewModel.product_master.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Product_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetContentTypeMasterList(int intGICOde = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGICOde" };
            object[] objParamValue = { intGICOde };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ContentTypeMasterList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
    }
}
