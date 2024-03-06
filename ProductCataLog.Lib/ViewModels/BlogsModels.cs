using ProductCataLog.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.ViewModels
{
    public class ContentTypeViewModel
    {
        public ContentType_Master ContentType_Master { get; set; }

        public List<ContentType_Master> ContentType_Masters { get; set; }
    }

    public class GalleryMappingViewModel
    {
        public Gallery_Mapping Gallery_Mapping { get; set; }

        public List<Gallery_Mapping> Gallery_Mappings { get; set; }
    }
}
