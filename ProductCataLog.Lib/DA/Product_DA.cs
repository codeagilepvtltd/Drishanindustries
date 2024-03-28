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
        public DataSet Select_ProductDetails(string varCategoryName, string varProductName)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varProductName", "varCategoryName" };
            object[] objParamValue = { varProductName, varCategoryName };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ProductDetails, objParamName, objParamValue);
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
            object[] objParamName = { "intGlCode", "varCatergoryCode", "varCatergoryName", "ref_ParentID", "chrActive", "MetaKeyword", "MetaDescription", "ref_EntryBy", "ref_UpdateBy", "intRankNumber" };
            object[] objParamValue = { categoryViewModel.category_Master.intGlCode, categoryViewModel.category_Master.varCatergoryCode, categoryViewModel.category_Master.varCatergoryName,
                      categoryViewModel.category_Master.ref_ParentID, categoryViewModel.category_Master.chrActive, categoryViewModel.category_Master.MetaKeyword,
                 categoryViewModel.category_Master.MetaDescription, categoryViewModel.category_Master.ref_EntryBy,
                categoryViewModel.category_Master.ref_UpdateBy,categoryViewModel.category_Master.RankNumber };

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
                "varShortDescription","decOriginalPrice","decDisplayPrice","varLongDescription", "chrActive", "MetaKeyword", "MetaDescription", "ref_EntryBy", "ref_UpdateBy","ShowOnHomePage","RankNumber" };
            object[] objParamValue = { productViewModel.product_master.intGiCode,productViewModel.product_master.ProductPriceID,productViewModel.product_master.ref_CategoryId,
                      productViewModel.product_master.varProductCode,productViewModel.product_master.varProductName,productViewModel.product_master.varShortDescription,
                productViewModel.product_master.decOriginalPrice,productViewModel.product_master.decDisplayPrice,productViewModel.product_master.varLongDescription
            ,productViewModel.product_master.chrActive,productViewModel.product_master.MetaKeyword,
                productViewModel.product_master.MetaDescription,productViewModel.product_master.ref_EntryBy,productViewModel.product_master.ref_UpdateBy,productViewModel.product_master.ShowOnHomePage,productViewModel.product_master.RankNumber};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Product_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetContentTypeMasterList(string Purpose)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "@varPurpose" };
            object[] objParamValue = { Purpose };

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

        public DataSet GetGalleryMappingList(string varPurpose)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "@varPurpose" };
            object[] objParamValue = { varPurpose };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_GalleryMappingList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet InsertUpdate_GalleryMapping(ProductContentTypeMasterViewModel contentViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "ref_ContentId", "ref_GalleryId", "fk_ProductID", "fk_ContentTypeID", "varTitle",
                "varGalleryType", "varGalleryName", "varGalleryURL", "varGalleryPath","varAuthor","varShortDescription","varContent","chrActive",
            "ref_EntryBy","ref_UpdateBy"};
            object[] objParamValue = { contentViewModel.content_Master.intGICode, contentViewModel.gallery_Mapping.intGICode, contentViewModel.gallery_Mapping.fk_ProductID,
                      contentViewModel.gallery_Mapping.fk_ContentTypeID, contentViewModel.gallery_Mapping.varTitle, contentViewModel.gallery_Mapping.varGalleryType,
                 contentViewModel.gallery_Mapping.varGalleryName, contentViewModel.gallery_Mapping.varGalleryPath,
                contentViewModel.gallery_Mapping.varGalleryPath,contentViewModel.gallery_Mapping.ref_EntryBy,contentViewModel.content_Master.varShortDescription,
                contentViewModel.contentType_Master.varContentDescription,contentViewModel.gallery_Mapping.charActive,contentViewModel.gallery_Mapping.ref_EntryBy,
            contentViewModel.gallery_Mapping.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_GalleryMapping, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetProductInquiryList(int intGlCode)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "@intGlCode" };
            object[] objParamValue = { intGlCode };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ProductInquiryListUpdate, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet Update_ProductInquiry(ProductInquiryReportViewModel productinquiryViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode", "varFeedbackResponse", "fk_LookupType_DetailsId", "ref_UpdateBy" };
            object[] objParamValue = { productinquiryViewModel.InquiryDetails.intGICode,productinquiryViewModel.InquiryDetails.varContent,
                productinquiryViewModel.InquiryDetails.fk_LookupType_DetailsId,productinquiryViewModel.InquiryDetails.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_Update_InquiryDetails, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }

        public DataSet GetRelatedProductList(int intGlCode = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGICode" };
            object[] objParamValue = { intGlCode };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_RelatedProductList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_RelatedProduct(RelatedProductViewModel relatedProductViewModel)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGICode", "ref_OriginalProductID", "ref_RelatedProductID", "chrActive", "ref_EntryBy",
                "ref_UpdateBy" };
            object[] objParamValue = { relatedProductViewModel.Related_Product.intGICode, relatedProductViewModel.Related_Product.ref_OriginalProductID,
            relatedProductViewModel.Related_Product.ref_RelatedProductID,relatedProductViewModel.Related_Product.charActive,
            relatedProductViewModel.Related_Product.ref_EntryBy, relatedProductViewModel.Related_Product.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Related_Product_Master, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
    }
}
