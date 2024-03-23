using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Category_Master
    {
        public int intGlCode { get; set; }
        public string varCatergoryName { get; set; }
        public string ParentCatergoryName { get; set; }
        public string varCatergoryCode { get; set; }
        public int ref_ParentID { get; set; }
        public string chrActive { get; set; }
        public DateTime? dtEntryDate { get; set; }
        public long? ref_EntryBy { get; set; }
        public DateTime? dtUpdatedDate { get; set; }
        public long? ref_UpdateBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
    }
}
