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
        public List<ContactUs> GetContactUsList(int fk_LookupType_DetailsId = 0)
        {
            Reports_DA ReportsDA = new Reports_DA();
            List<ContactUs> ContactUs_List = new List<ContactUs>();

            try
            {
                DataSet dsResult = ReportsDA.GetContactUsList(fk_LookupType_DetailsId);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    ContactUs_List = dsResult.Tables[0].AsEnumerable().Select(row => new ContactUs()
                    {
                        intGICode = row.Field<Int64>("intGICode"),
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

        public List<LookupType_Details> GetLookUpTypeList(string varPurpose = "")
        {
            Reports_DA ReportsDA = new Reports_DA();
            List<LookupType_Details> LookupType_Details = new List<LookupType_Details>();

            try
            {
                DataSet dsResult = ReportsDA.GetLookUpTypeList(varPurpose);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    LookupType_Details = dsResult.Tables[0].AsEnumerable().Select(row => new LookupType_Details()
                    {
                        intGICode = row.Field<int>("intGICode"),
                        varValue = row.Field<string>("varValue")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return LookupType_Details;
        }

        #endregion

        #region Inquiry List
        public List<Inquiry_Details> GetProductInquiryList(int ref_LookupTypeDetailId = 0)
        {
            Reports_DA ReportsDA = new Reports_DA();
            List<Inquiry_Details> Inquiry_Details_List = new List<Inquiry_Details>();

            try
            {
                DataSet dsResult = ReportsDA.GetProductInquiryList(ref_LookupTypeDetailId);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    Inquiry_Details_List = dsResult.Tables[0].AsEnumerable().Select(row => new Inquiry_Details()
                    {
                        intGICode = row.Field<Int64>("intGICode"),
                        varInquiryNo = row.Field<string>("varInquiryNo"),
                        varName = row.Field<string>("varName"),
                        varContactNo = row.Field<string>("varContactNo"),
                        varEmail = row.Field<string>("varEmail"),
                        varContent = row.Field<string>("varContent"),
                        chrActive = row.Field<string>("chrActive"),
                        decDisplayPrice = row.Field<decimal>("decDisplayPrice"),
                        decDiscount = row.Field<decimal>("decDiscount"),

                        varProductCode = row.Field<string>("varProductCode"),
                        varProductName = row.Field<string>("varProductName"),
                        varShortDescription = row.Field<string>("varShortDescription"),
                        varLongDescription = row.Field<string>("varLongDescription"),
                        dtEntryDate = row.Field<DateTime>("dtEntryDate")

                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return Inquiry_Details_List;
        }
        #endregion
    }
}
