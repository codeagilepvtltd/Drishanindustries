using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Content_Master
    {
        public long intGICode { get; set; }

        public int? fk_ContentTypeID { get; set; }

        public string varTitle { get; set; }

        public string varShortDescription { get; set; }

        public string varContent { get; set; }

        public string varAuthor { get; set; }

        public DateTime? dtCreatedDate { get; set; }

        public string chrActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }
}
