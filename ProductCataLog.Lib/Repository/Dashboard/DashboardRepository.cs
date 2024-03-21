using ProductCataLog.Lib.DA;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProductCataLog.Lib.Repository.Product
{
    public class DashboardRepository : IDashboardRepository
    {
        #region Dashboard
        public DashboardViewModel Select_DashboardSummary()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.dashboardSummary = new List<DashboardSummary>();
            dashboardViewModel.dashboarProductInquiry = new List<DashboardProductInquiry>();
            dashboardViewModel.dashboardContactusInquiry = new List<DashboardContactusInquiry>();
            Dashboard_DA dashboard_DA = new Dashboard_DA();
            try
            {
                DataSet dsResult = dashboard_DA.Select_DashboardSummary();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    dashboardViewModel.dashboardSummary.Add(new DashboardSummary()
                    {
                        ClassName = Convert.ToString(dr["ClassName"]),
                        SectionName = Convert.ToString(dr["SectionName"]),
                        RecordCount = Convert.ToInt32(dr["RecordCount"]),
                        CurrentYearCount = Convert.ToInt32(dr["CurrentYearCount"]),
                        PercentageCount = Convert.ToDecimal(dr["PercentageCount"])
                    });
                }
                foreach (DataRow dr in dsResult.Tables[1].Rows)
                {
                    dashboardViewModel.dashboarProductInquiry.Add(new DashboardProductInquiry()
                    {
                        InquiryId = Convert.ToString(dr["varInquiryNo"]),
                        CustomerName = Convert.ToString(dr["varName"]),
                        Email = Convert.ToString(dr["varEmail"]),
                        ContatcNo = Convert.ToString(dr["varContactNo"]),
                        ProductName = Convert.ToString(dr["varProductName"]),
                        Status = Convert.ToString(dr["StatusName"])
                    });
                }
                foreach (DataRow dr in dsResult.Tables[2].Rows)
                {
                    dashboardViewModel.dashboardContactusInquiry.Add(new DashboardContactusInquiry()
                    {
                        ContactUsCode = Convert.ToString(dr["varContactUsCode"]),
                        CustomerName = Convert.ToString(dr["varName"]),
                        Email = Convert.ToString(dr["varEmail"]),
                        ContactNo = Convert.ToString(dr["varContactNo"]),
                        Content = Convert.ToString(dr["varContent"]),
                        Status = Convert.ToString(dr["varValue"])
                    });
                }
                string ProductChartString = string.Empty;
                string ProductMonthString = string.Empty;
                foreach (DataRow dr in dsResult.Tables[3].Rows)
                {
                    ProductChartString += Convert.ToString(dr["InquiryCount"]) + ",";
                    ProductMonthString += Convert.ToString(dr["MonthWise"]) + ",";
                }
                dashboardViewModel.ProductInquirychartData = ProductChartString.TrimEnd(',');
                dashboardViewModel.ProductInquirychartMonths = ProductMonthString.TrimEnd(',');
            }
            catch
            {
                throw;
            }
            return dashboardViewModel;
        }
        #endregion


    }
}
