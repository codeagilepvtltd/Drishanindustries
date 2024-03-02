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

        public DataSet GetProductInquiryList(int InquiryId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { InquiryId };

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

        public DataSet GetContactUsList(int ContactId = 0)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { ContactId };

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
    }
}
