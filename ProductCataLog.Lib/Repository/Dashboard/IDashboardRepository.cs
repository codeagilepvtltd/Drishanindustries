using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProductCataLog.Lib.Repository.Product
{
    public interface IDashboardRepository
    {
        #region Category
        DashboardViewModel Select_DashboardSummary();

    
        #endregion

    }
}
