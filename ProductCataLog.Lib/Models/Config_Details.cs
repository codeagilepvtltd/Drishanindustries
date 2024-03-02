﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Config_Details
    {
        public int intGICode { get; set; }

        public int? ref_ConfigurationID { get; set; }

        public string ConfigType { get; set; }

        public string varName { get; set; }

        public string varValue1 { get; set; }

        public string varValue2 { get; set; }

        public string varValue3 { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }
    }
}
