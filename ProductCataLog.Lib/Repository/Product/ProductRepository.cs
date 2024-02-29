using ProductCataLog.Lib.DA;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProductCataLog.Lib.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        #region Category
        public List<Category_Master> GetCategoryList(int intGlCode = 0)
        {
            Product_DA ProductDA = new Product_DA();
            List<Category_Master> category_Masters = new List<Category_Master>();

            try
            {
                DataSet dsResult = ProductDA.GetCategoryList(intGlCode);

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
            Product_DA ProductDA = new Product_DA();
            try
            {
                return ProductDA.InsertUpdate_Category(categoryViewModel);

            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
