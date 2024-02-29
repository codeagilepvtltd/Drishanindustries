﻿using PranicAhmedbad.Common;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Models;
using PranicAhmedbad.Models;
using System;
using System.Data;
using System.Text;

namespace ProductCataLog.Lib.DA
{
    public class ModuleErrorLogDA
    {
        DataSet resultSet = new DataSet();

        private StringBuilder sqlQuery;
        public void Insert_Modules_Error_Log(string varPageName, string varMethodName, string varUserId, string varStackTrace, string varModuleName, string varSourceSystem, string varExtra1, string varExtra2, string varExtraa3, string varExceptionMessage)
        {
            sqlQuery = new StringBuilder();
            object[] objParamName = { "varPageName", "varMethodName", "varUserId", "varStackTrace", "varModuleName", "varSourceSystem", "varExtra1", "varExtra2", "varExtraa3", "varExceptionMessage" };
            object[] objParamValue = { varPageName, varMethodName, varUserId, varStackTrace, varModuleName, varSourceSystem, varExtra1, varExtra2, varExtraa3, varExceptionMessage };

            try
            {
                SQLHelper.GetData(StoredProcedures.USP_Insert_Modules_Error_Log, objParamName, objParamValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet Insert_Modules_Error_Log(ModuleErrorLogModel moduleErrorLogModel)
        {
            moduleErrorLogModel.varPageName = moduleErrorLogModel.varPageName == null ? "" : moduleErrorLogModel.varPageName;
            moduleErrorLogModel.varMethodName = moduleErrorLogModel.varMethodName == null ? "" : moduleErrorLogModel.varMethodName;
            moduleErrorLogModel.varUserId = moduleErrorLogModel.varUserId == null ? "" : moduleErrorLogModel.varUserId;
            moduleErrorLogModel.varStackTrace = moduleErrorLogModel.varStackTrace == null ? "" : moduleErrorLogModel.varStackTrace;
            moduleErrorLogModel.varModuleName = moduleErrorLogModel.varModuleName == null ? "" : moduleErrorLogModel.varModuleName;
            moduleErrorLogModel.varSourceSystem = moduleErrorLogModel.varSourceSystem == null ? "" : moduleErrorLogModel.varSourceSystem;
            moduleErrorLogModel.varExtra1 = moduleErrorLogModel.varExtra1 == null ? "" : moduleErrorLogModel.varExtra1;
            moduleErrorLogModel.varExtra2 = moduleErrorLogModel.varExtra2 == null ? "" : moduleErrorLogModel.varExtra2;
            moduleErrorLogModel.varExtraa3 = moduleErrorLogModel.varExtraa3 == null ? "" : moduleErrorLogModel.varExtraa3;
            moduleErrorLogModel.varExceptionMessage = moduleErrorLogModel.varExceptionMessage == null ? "" : moduleErrorLogModel.varExceptionMessage;

            sqlQuery = new StringBuilder();
            object[] objParamName = { "varPageName", "varMethodName", "varUserId", "varStackTrace", "varModuleName", "varSourceSystem", "varExtra1", "varExtra2", "varExtraa3", "varExceptionMessage" };
            object[] objParamValue = { moduleErrorLogModel.varPageName, moduleErrorLogModel.varMethodName, moduleErrorLogModel.varUserId, moduleErrorLogModel.varStackTrace, moduleErrorLogModel.varModuleName, moduleErrorLogModel.varSourceSystem, moduleErrorLogModel.varExtra1, moduleErrorLogModel.varExtra2, moduleErrorLogModel.varExtraa3, moduleErrorLogModel.varExceptionMessage };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Insert_Modules_Error_Log, objParamName, objParamValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultSet;

        }

    }
}
