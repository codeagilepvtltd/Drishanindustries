using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class LookupType_Master
    {
        public int intGICode { get; set; }
        public int varLookupCode { get; set; }
        public string varLookupName { get; set; }
        public string varPurpose { get; set; }
        public string chrActive { get; set; }
    }
}
