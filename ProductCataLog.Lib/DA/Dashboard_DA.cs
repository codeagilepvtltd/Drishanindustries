using ProductCataLog.Lib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.DA
{
    public class Dashboard_DA
    {
        private DataSet resultSet;

        public DataSet Select_DashboardSummary()
        {
         
            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_DashboardSummary);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

    }
}
