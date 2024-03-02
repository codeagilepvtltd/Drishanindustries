using ProductCataLog.Lib.DA;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProductCataLog.Lib.Repository.Reports
{
    public class ReportsRepository : IReportsRepository
    {
        #region ContactUs
        public List<ContactUs> GetContactUsList(int intGlCode = 0)
        {
            Reports_DA ReportsDA = new Reports_DA();
            List<ContactUs> ContactUs_List = new List<ContactUs>();

            try
            {
                DataSet dsResult = ReportsDA.GetContactUsList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    ContactUs_List = dsResult.Tables[0].AsEnumerable().Select(row => new ContactUs()
                    {
                        intGICode = row.Field<int>("intGICode"),
                        varContactUsCode = row.Field<string>("varContactUsCode"),
                        varName = row.Field<string>("varName"),
                        varContactNo = row.Field<string>("varContactNo"),
                        varEmail = row.Field<string>("varEmail"),
                        varContent = row.Field<string>("varContent"),
                        fk_LookupType_DetailsId = row.Field<int>("fk_LookupType_DetailsId"),
                        chrActive = row.Field<string>("chrActive"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return ContactUs_List;
        }
        #endregion
    }
}
