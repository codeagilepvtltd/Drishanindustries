using ProductCataLog.Lib.DA;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Repository.Config
{
    public class ConfigRepository : IConfigRepository
    {
        #region ConfigMaster
        public List<Config_Master> GetConfigMasterList(int intGlCode = 0)
        {
            Config_DA ConfigDA = new Config_DA();
            List<Config_Master> config_Master = new List<Config_Master>();

            try
            {
                DataSet dsResult = ConfigDA.GetConfigMasterList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    config_Master = dsResult.Tables[0].AsEnumerable().Select(row => new Config_Master()
                    {
                        intGICode = row.Field<int>("intGICode"),
                        varName = row.Field<string>("varName"),
                        chrActive = row.Field<string>("chrActive"),
                        ref_EntryBy = row.Field<long?>("ref_EntryBy"),
                        ref_UpdateBy = row.Field<long?>("ref_UpdateBy"),
                        dtEntryDate = row.Field<DateTime?>("dtEntryDate"),                        
                        dtUpdatedDate = row.Field<DateTime?>("dtUpdatedDate")                                               
                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return config_Master;
        }
        #endregion

        #region ConfigDetails
        public List<Config_Details> GetConfigDetailsList(int intGlCode = 0)
        {
            Config_DA ConfigDA = new Config_DA();
            List<Config_Details> config_Details = new List<Config_Details>();

            try
            {
                DataSet dsResult = ConfigDA.GetConfigMasterList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    config_Details = dsResult.Tables[0].AsEnumerable().Select(row => new Config_Details()
                    {
                        intGICode = row.Field<int>("intGICode"),
                        varName = row.Field<string>("varName"),
                        chrActive = row.Field<string>("chrActive"),
                        ref_EntryBy = row.Field<long?>("ref_EntryBy"),
                        ref_UpdateBy = row.Field<long?>("ref_UpdateBy"),
                        dtEntryDate = row.Field<DateTime?>("dtEntryDate"),
                        dtUpdatedDate = row.Field<DateTime?>("dtUpdatedDate")
                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return config_Details;
        }

        public DataSet InsertUpdate_configDetails(ConfigDetailsViewModel configDetailsViewModel)
        {
            Config_DA ConfigDA = new Config_DA();
            try
            {
                return ConfigDA.InsertUpdate_ConfigDetails(configDetailsViewModel);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
