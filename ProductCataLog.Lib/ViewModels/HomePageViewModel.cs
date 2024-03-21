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
    public class AdminMenu
    {
        public int fk_ModuleID { get; set; }
        public string varDisplayName { get; set; }
        public int ref_ParentPageID { get; set; }
        public string varActionName { get; set; }
        public string varControllerName { get; set; }
        public int intDisplay_Order { get; set; }
        public string varIconPath { get; set; }
        public string chrMenuType { get; set; }
        public string chrActive { get; set; }
        public DateTime dtEntryDate { get; set; }
        public long ref_EntryBy { get; set; }
        public DateTime dtUpdatedDate { get; set; }
        public int ref_UpdateBy { get; set; }
    }
    public class AdminMenuViewModel
    {
        public string AdminHtmlString { get; set; }
    }
}