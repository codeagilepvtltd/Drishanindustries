using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Gallery_Mapping
    {
        public long intGICode { get; set; }

        public int? CTM_intGlCode { get; set; }
        public int? fk_ProductID { get; set; }

        public int? fk_ContentID { get; set; }
        public int? fk_ContentTypeID { get; set; }

        public string varGalleryType { get; set; }

        public string varGalleryName { get; set; }

        public string varGalleryPath { get; set; }

        public string varGalleryURL { get; set; }

        public string varTitle { get; set; }
        public string varContent { get; set; }
        public string varProductName { get; set; }

        public string varShortDescription { get; set; }

        public string charActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

    }

}
