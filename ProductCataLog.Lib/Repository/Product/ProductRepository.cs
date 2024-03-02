﻿using ProductCataLog.Lib.DA;
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

        #region Product
        public List<Product_Master> GetProductList(int intGlCode = 0)
        {
            Product_DA ProductDA = new Product_DA();
            List<Product_Master> product_Masters = new List<Product_Master>();

            try
            {
                DataSet dsResult = ProductDA.GetProductList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    product_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new Product_Master()
                    {
                        intGiCode = row.Field<int>("intGiCode"),
                        varProductCode = row.Field<string>("varProductCode"),
                        varProductName = row.Field<string>("varProductName"),
                        varCatergoryCode = row.Field<string>("varCatergoryCode"),
                        varCatergoryName = row.Field<string>("varCatergoryName"),
                        varLongDescription = row.Field<string>("varLongDescription"),
                        varShortDescription = row.Field<string>("varShortDescription"),
                        decDisplayPrice = row.Field<decimal>("decDisplayPrice"),
                        decOriginalPrice = row.Field<decimal>("decOriginalPrice"),
                        ref_CategoryId = row.Field<Int64>("ref_CategoryId"),
                        ProductPriceID = row.Field<int>("ProductPriceID"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return product_Masters;
        }

        public DataSet InsertUpdate_product(ProductMasterViewModel productViewModel)
        {
            Product_DA ProductDA = new Product_DA();
            try
            {
                return ProductDA.InsertUpdate_Product(productViewModel);

            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
