﻿using ProductCataLog.Lib.Models;
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

        public Content_Master content_Master { get; set; }
        public List<Content_Master> content_Masters { get; set; }

        public Gallery_Mapping gallery_Mapping { get; set; }
        public List<Gallery_Mapping> gallery_Mappings { get; set; }
    }

    public class RelatedProductViewModel
    {
        public Product_Master product_master { get; set; }
        public List<Product_Master> product_masters { get; set; }

        public Related_Products Related_Product { get; set; }
        public List<Related_Products> Related_Products { get; set; }

    }

    public class ProductDetailViewModel
    {
        public Product_Master product_master { get; set; }

        public List<Gallery_Mapping> gallery_Mappings { get; set; }

        public ProductDetailViewModel()
        {

            product_master = new Product_Master();
            gallery_Mappings= new List<Gallery_Mapping>();
        }
    }

}
