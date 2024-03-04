using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.ViewModels;
using System.Data;
using System.Text;

namespace ProductCataLog.Lib.DA
{
    internal class Reports_DA
    {
        private StringBuilder sqlQuery;
        private DataSet resultSet;

        public DataSet GetContactUsList(int fk_LookupType_DetailsId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "fk_LookupType_DetailsId" };
            object[] objParamValue = { fk_LookupType_DetailsId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ContactUsReport, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet GetProductInquiryList(int @ref_LookupTypeDetailId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "ref_LookupTypeDetailId" };
            object[] objParamValue = { ref_LookupTypeDetailId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ProductInquiryList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
        public DataSet GetLookUpTypeList(string varPurpose = "")
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varPurpose" };
            object[] objParamValue = { varPurpose };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_LookUpTypeList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }
    }
}
