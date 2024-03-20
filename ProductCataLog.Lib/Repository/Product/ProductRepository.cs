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
                        ShowOnHomePage = row.Field<bool>("ShowOnHomePage"),
                        RankNumber = row.Field<long>("RankNumber")

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
        public List<ContentType_Master> GetContentTypeMasterList(string Purpose)
        {
            Product_DA ProductDA = new Product_DA();
            List<ContentType_Master> contentType_Masters = new List<ContentType_Master>();

            try
            {
                DataSet dsResult = ProductDA.GetContentTypeMasterList(Purpose);

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

        public List<Gallery_Mapping> GetGalleryMappingList(string varPurpose)
        {
            Product_DA ProductDA = new Product_DA();
            List<Gallery_Mapping> gallery_Masters = new List<Gallery_Mapping>();

            try
            {
                DataSet dsResult = ProductDA.GetGalleryMappingList(varPurpose);

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

        #region ProductInquery
        public List<Inquiry_Details> GetProductInquiryList(int intGlCode = 0)
        {
            Product_DA ProductDA = new Product_DA();
            List<Inquiry_Details> inquiry_details = new List<Inquiry_Details>();

            try
            {
                DataSet dsResult = ProductDA.GetProductInquiryList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    inquiry_details = dsResult.Tables[0].AsEnumerable().Select(row => new Inquiry_Details()
                    {
                        intGICode = row.Field<Int64>("intGICode"),
                        varProductCode = row.Field<string>("varProductCode"),
                        varProductName = row.Field<string>("varProductName"),
                        varLongDescription = row.Field<string>("varLongDescription"),
                        varShortDescription = row.Field<string>("varShortDescription"),
                        varEmail = row.Field<string>("varEmail"),
                        varContactNo = row.Field<string>("varContactNo"),
                        LookUp_varValue = row.Field<string>("LookUp_varValue"),
                        varInquiryNo = row.Field<string>("varInquiryNo"),
                        varName = row.Field<string>("varName"),
                        fk_InquiryID = row.Field<Int64>("fk_InquiryID"),
                        fk_LookupType_DetailsId = row.Field<int>("fk_LookupType_DetailsId"),
                        varContent = row.Field<string>("varContent"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return inquiry_details;
        }

        public DataSet Update_productInquiry(ProductInquiryReportViewModel productinquiryViewModel)
        {
            Product_DA ProductDA = new Product_DA();
            try
            {
                return ProductDA.Update_ProductInquiry(productinquiryViewModel);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Product
        public List<Related_Products> GetRelatedProductList(int intGlCode = 0)
        {
            Product_DA ProductDA = new Product_DA();
            List<Related_Products> RelatedProduct_Masters = new List<Related_Products>();

            try
            {
                DataSet dsResult = ProductDA.GetRelatedProductList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    RelatedProduct_Masters = dsResult.Tables[0].AsEnumerable().Select(row => new Related_Products()
                    {
                        intGICode = row.Field<int>("intGICode"),
                        ref_OriginalProductID = row.Field<int>("ref_OriginalProductID"),
                        ref_RelatedProductID = row.Field<int>("ref_RelatedProductID"),
                        ProductCode = row.Field<string>("ProductCode"),
                        ProductName = row.Field<string>("ProductName"),
                        RelatedProductCode = row.Field<string>("RelatedProductCode"),
                        RelatedProductName = row.Field<string>("RelatedProductName"),                       
                        charActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate"),                     
                        ref_EntryBy = row.Field<long>("ref_EntryBy")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return RelatedProduct_Masters;
        }

        public DataSet InsertUpdate_RelatedProduct(RelatedProductViewModel relatedProductViewModel)
        {
            Product_DA ProductDA = new Product_DA();
            try
            {
                return ProductDA.InsertUpdate_RelatedProduct(relatedProductViewModel);

            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
