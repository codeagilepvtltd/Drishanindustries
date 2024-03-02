using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.DA
{
    internal class Config_DA
    {
        private DataSet resultSet;

        public DataSet GetConfigMasterList(int ConfigTypeId = 0)
        {
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { ConfigTypeId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ConfigMasterList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }


        public DataSet GetConfigDetailsList(int ConfigDetailId = 0)
        {
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { ConfigDetailId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ConfigDetailsList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_ConfigDetails(ConfigDetailsViewModel configDetailsViewModel)
        {
            object[] objParamName = { "intGlCode", "ref_ConfigurationID", "varName", "varValue1", "varValue2",
                "varValue3","chrActive","ref_EntryBy","ref_UpdateBy" };
            object[] objParamValue = { configDetailsViewModel.config_Detail.intGICode, configDetailsViewModel.config_Detail.ref_ConfigurationID,
                                       configDetailsViewModel.config_Detail.varName,configDetailsViewModel.config_Detail.varValue1,configDetailsViewModel.config_Detail.varValue2,
                                       configDetailsViewModel.config_Detail.varValue3,configDetailsViewModel.config_Detail.chrActive,configDetailsViewModel.config_Detail.ref_EntryBy,
                                       configDetailsViewModel.config_Detail.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_Configuration_Details, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
    }
}
