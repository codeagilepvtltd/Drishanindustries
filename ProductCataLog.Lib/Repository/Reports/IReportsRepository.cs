using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;


namespace ProductCataLog.Lib.Repository.Reports
{
    public interface IReportsRepository
    {
        #region ContactUs
        List<ContactUs> GetContactUsList(int fk_LookupType_DetailsId = 0);
        List<LookupType_Details> GetLookUpTypeList(string varPurpose = "");
        #endregion
    }
}
