using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.ViewModels
{
    public class DashboardViewModel
    {
        public List<DashboardSummary> dashboardSummary { get; set; }
        public List<DashboardProductInquiry> dashboarProductInquiry { get; set; }

        public List<DashboardContactusInquiry> dashboardContactusInquiry { get; set; }

        public string ProductInquirychartData { get; set; }
        public string ProductInquirychartMonths { get; set; }
    }

    public class DashboardSummary
    {
        public string SectionName { get; set; }
        public int RecordCount { get; set; }
        public int CurrentYearCount { get; set; }
        public decimal PercentageCount { get; set; }
        public string ClassName { get; set; }
    }
    public class DashboardProductInquiry
    {
        public string InquiryId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string ContatcNo { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
    }
    public class DashboardContactusInquiry
    {
        public string ContactUsCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}
