using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Inquiry_Details
    {
        public string varName { get; set; }
        public string varContactNo { get; set; }
        public string varInquiryNo { get; set; }

        public string varEmail { get; set; }
        public string varContent { get; set; }

        public string varProductCode { get; set; }

        public string varProductName { get; set; }

        public string varShortDescription { get; set; }

        public string varLongDescription { get; set; }

        public long intGICode { get; set; }

        public long? fk_InquiryID { get; set; }

        public int? ref_ProductID { get; set; }

        public decimal? decDisplayPrice { get; set; }

        public decimal? decDiscount { get; set; }

        public int? fk_LookupType_DetailsId { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
