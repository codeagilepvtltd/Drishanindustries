using ProductCataLog.Lib.Models;
using System;
using System.Collections.Generic;

namespace ProductCataLog.Lib.ViewModels
{
    public class ContactUsViewModel
    {
        public ContactUs ContactUs { get; set; }

        public List<ContactUs> ContactUsList { get; set; }
    }

    public class LookUpTypeViewModel
    {
        public LookupType_Master LookupType_Master { get; set; }

        public List<LookupType_Master> LookupType_Masters { get; set; }

        public LookupType_Details LookupType_Details { get; set; }
        public List<LookupType_Details> LookupType_DetailsList { get; set; }
    }
    public class ProductInquiryReportViewModel
    {
        public Inquiry_Details InquiryDetails { get; set; }

        public List<Inquiry_Details> InquiryDetailsList { get; set; }
    }
}
