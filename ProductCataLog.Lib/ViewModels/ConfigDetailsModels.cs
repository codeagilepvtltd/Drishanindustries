using ProductCataLog.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.ViewModels
{    
    public class ConfigDetailsViewModel
    {
        public Config_Details config_Detail { get; set; }

        public List<Config_Details> config_Details { get; set; }

    }

    public class ConfigMasterViewModel
    {
        public Config_Master config_Master { get; set; }

        public List<Config_Master> config_Masters { get; set; }

    }
}
