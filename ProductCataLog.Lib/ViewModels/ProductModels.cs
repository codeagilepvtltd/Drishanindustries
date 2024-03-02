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
}
