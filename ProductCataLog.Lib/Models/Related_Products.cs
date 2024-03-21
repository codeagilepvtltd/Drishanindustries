using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Related_Products
    {
        public int intGICode { get; set; }

        public int? ref_OriginalProductID { get; set; }

        public int? ref_RelatedProductID { get; set; }

        public string charActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string RelatedProductCode { get; set; }

        public string RelatedProductName { get; set; }

    }
}
