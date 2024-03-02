using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class ContactUs
    {
        public long intGICode { get; set; }
        public string varContactUsCode { get; set; }
        public string varName { get; set; }
        public string varContactNo { get; set; }
        public string varEmail { get; set; }
        public string varContent { get; set; }
        public int fk_LookupType_DetailsId { get; set; }
        public string chrActive { get; set; }
        public DateTime dtEntryDate { get; set; }
        public long ref_EntryBy { get; set; }
        public DateTime dtUpdatedDate { get; set; }
        public long ref_UpdateBy { get; set; }
    }
}
