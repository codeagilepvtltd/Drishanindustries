using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class LookupType_Details
    {
        public int intGICode { get; set; }

        public int? fk_LookuptypeID { get; set; }

        public string varValue { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

    }
}
