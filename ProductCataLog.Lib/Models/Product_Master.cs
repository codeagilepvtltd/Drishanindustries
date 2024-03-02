using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Product_Master
    {
        public int intGiCode { get; set; }
        public string varProductCode { get; set; }
        public string varProductName { get; set; }
        public string varShortDescription { get; set; }
        public string varLongDescription { get; set; }
        public string chrActive { get; set; }
        public DateTime dtEntryDate { get; set; }
        public long ref_EntryBy { get; set; }
        public DateTime dtUpdatedDate { get; set; }
        public long ref_UpdateBy { get; set; }
        public long ref_CategoryId { get; set; }
        public int ProductPriceID { get; set; }
        public decimal decOriginalPrice { get; set; }
        public decimal decDisplayPrice { get; set; }
        public string varCatergoryCode { get; set; }
        public string varCatergoryName { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
    }
}
