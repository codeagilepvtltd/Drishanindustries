using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Models
{
    public class Gallery_Mapping
    {
        public string CTM_varContentType { get; set; }

        public string CTM_varPurpose { get; set; }

        public IFormFile UploadedImage { get; set; }
        public long CM_intGlCode { get; set; }

        public string CM_varTitle { get; set; }

        public string CM_varShortDescription { get; set; }

        public string CM_varContent { get; set; }

        public string CM_varAuthor { get; set; }

        public long intGICode { get; set; }

        public int? CTM_intGlCode { get; set; }
        public int? fk_ProductID { get; set; }

        public long? fk_ContentID { get; set; }
        public int? fk_ContentTypeID { get; set; }

        public string varGalleryType { get; set; }

        public string varGalleryName { get; set; }

        public string varGalleryPath { get; set; }

        public string varGalleryURL { get; set; }

        public string varTitle { get; set; }
        public string varContent { get; set; }
        public string varProductName { get; set; }

        public string varShortDescription { get; set; }
        public string varContentDescription { get; set; }

        public string charActive { get; set; }

        public DateTime? dtEntryDate { get; set; }

        public long? ref_EntryBy { get; set; }

        public DateTime? dtUpdatedDate { get; set; }

        public long? ref_UpdateBy { get; set; }

        public int? PM_intGlCode { get; set; }

    }

}
