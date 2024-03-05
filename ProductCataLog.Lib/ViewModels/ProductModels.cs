using ProductCataLog.Lib.Models;
using System;
using System.Collections.Generic;

namespace ProductCataLog.Lib.ViewModels
{
    public class CategoryMasterViewModel
    {
        public Category_Master category_Master { get; set; }

        public List<Category_Master> Category_Masters { get; set; }
    }

    public class ProductMasterViewModel
    {
        public Product_Master product_master { get; set; }
        public List<Product_Master> product_masters { get; set; }

    }

    public class ProductContentTypeMasterViewModel
    {
        public ContentType_Master contentType_Master { get; set; }
        public List<ContentType_Master> contentType_Masters { get; set; }

    }

    public class ProductContentMasterViewModel
    {
        public Content_Master content_Master { get; set; }
        public List<Content_Master> content_Masters { get; set; }

    }

    public class ProductGalleryMappingViewModel
    {
        public Gallery_Mapping gallery_Mapping { get; set; }
        public List<Gallery_Mapping> gallery_Mappings { get; set; }

    }
}
