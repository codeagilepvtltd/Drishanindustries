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
                        MetaDescription = row.Field<string>("MetaDescription"),
                        MetaKeyword = row.Field<string>("MetaKeyword"),
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
                        MetaDescription = row.Field<string>("MetaDescription"),
                        MetaKeyword = row.Field<string>("MetaKeyword"),
                        decDisplayPrice = row.Field<decimal>("decDisplayPrice"),
                        decOriginalPrice = row.Field<decimal>("decOriginalPrice"),
                        ref_CategoryId = row.Field<Int64>("ref_CategoryId"),
                        ProductPriceID = row.Field<int>("ProductPriceID"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate"),
                        RankNumber = row.Field<long>("RankNumber"),
                        ShowOnHomePage = row.Field<bool>("ShowOnHomePage")

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

        #region ProductImage/Video
        public List<ContentType_Master> GetContentTypeMasterList(int intGlCode = 0,string varPurpose = "")
        {
            Product_DA ProductDA = new Product_DA();
            List<ContentType_Master> contentType_Masters = new List<ContentType_Master>();

            try
            {
                DataSet dsResult = ProductDA.GetContentTypeMasterList(intGlCode, varPurpose);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    contentType_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new ContentType_Master()
                    {
                        intGICOde = row.Field<int>("intGICOde"),
                        varContentType = row.Field<string>("varContentType"),
                        varContentDescription = row.Field<string>("varContentDescription"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return contentType_Masters;
        }

        public List<Gallery_Mapping> GetGalleryMappingList(string PurposeCode)
        {
            Product_DA ProductDA = new Product_DA();
            List<Gallery_Mapping> gallery_Masters = new List<Gallery_Mapping>();

            try
            {
                DataSet dsResult = ProductDA.GetGalleryMappingList(PurposeCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    gallery_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new Gallery_Mapping()
                    {
                        intGICode = row.Field<Int64>("GM_intGlCode"),
                        fk_ContentID = row.Field<Int64>("CM_intGlCode"),
                        fk_ContentTypeID = row.Field<int>("CTM_intGlCode"),
                        fk_ProductID = row.Field<int>("PM_intGlCode"),
                        varGalleryType = row.Field<string>("GM_varGalleryType"),
                        varGalleryName = row.Field<string>("GM_varGalleryName"),
                        varGalleryPath = row.Field<string>("GM_varGalleryPath"),
                        varGalleryURL = row.Field<string>("GM_varGalleryURL"),
                        varTitle = row.Field<string>("GM_varTitle"),
                        varShortDescription = row.Field<string>("GM_varShortDescription"),
                        varContentDescription = row.Field<string>("CM_varContent"),
                        varContent = row.Field<string>("GM_varContent"),
                        varProductName = row.Field<string>("PM_varProductName"),
                        charActive = row.Field<string>("GM_chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return gallery_Masters;
        }

        public DataSet InsertUpdate_GalleryMapping(ProductContentTypeMasterViewModel ContentViewModel)
        {
            Product_DA ProductDA = new Product_DA();
            try
            {
                return ProductDA.InsertUpdate_GalleryMapping(ContentViewModel);

            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
