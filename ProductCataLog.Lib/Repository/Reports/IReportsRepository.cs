using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;


namespace ProductCataLog.Lib.Repository.Reports
{
    public interface IReportsRepository
    {
        #region ContactUs
        List<ContactUs> GetContactUsList(int intGlCode = 0);
        #endregion
    }
}
