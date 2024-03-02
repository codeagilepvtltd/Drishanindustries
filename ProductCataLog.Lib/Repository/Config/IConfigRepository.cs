using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Repository.Config
{
    public interface IConfigRepository
    {
        #region ConfigMaster
        List<Config_Master> GetConfigMasterList(int intGlCode = 0);        

        #endregion

        #region ConfigDetails
        List<Config_Details> GetConfigDetailsList(int intGlCode = 0);

        DataSet InsertUpdate_configDetails(ConfigDetailsViewModel configDetailsViewModel);

        #endregion
    }
}
