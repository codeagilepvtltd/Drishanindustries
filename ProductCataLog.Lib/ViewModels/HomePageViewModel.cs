using ProductCataLog.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.ViewModels
{
    public class BannerViewModel
    {
        public List<Gallery_Mapping> gallery_mapping { get; set; }
    }
    public class FeturedProductViewModel
    {
        public List<Gallery_Mapping> featuredProducts { get; set; }
    }
    public class OurServicesViewModel
    {
        public List<Gallery_Mapping> our_services { get; set; }
    }
    public class OurClientsViewModel
    {
        public List<Gallery_Mapping> our_clients { get; set; }
    }
    
    public class AdminMenuViewModel
    {
        public string AdminHtmlString { get; set; }
    }
    public class FrontLeftMenuViewModel
    {
        public string CategoryMenuHtmlString { get; set; }
    }
}