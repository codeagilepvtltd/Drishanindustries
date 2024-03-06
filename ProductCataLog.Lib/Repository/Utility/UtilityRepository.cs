using ProductCataLog.Lib.DA;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.Repository.Utility
{
    public class UtilityRepository : IUtilityRepository
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
                DataSet dsResult = ConfigDA.GetConfigDetailsList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    config_Details = dsResult.Tables[0].AsEnumerable().Select(row => new Config_Details()
                    {
                        intGICode = row.Field<int>("intGICode"),
                        ref_ConfigurationID = row.Field<int>("ref_ConfigurationID"),
                        ConfigType = row.Field<string>("ConfigType"),
                        varName = row.Field<string>("ConfigName"),
                        varValue1 = row.Field<string>("varValue1"),
                        varValue2 = row.Field<string>("varValue2"),
                        varValue3 = row.Field<string>("varValue3"),
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

        #region ContentTypeMaster
        public List<ContentType_Master> GetContentTypeMasterList(int intGlCode = 0)
        {
            Blogs_DA BlogsDA = new Blogs_DA();
            List<ContentType_Master> contentType_Master = new List<ContentType_Master>();

            try
            {
                DataSet dsResult = BlogsDA.GetContentTypeMasterList(intGlCode);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    contentType_Master = dsResult.Tables[0].AsEnumerable().Select(row => new ContentType_Master()
                    {
                        intGICOde = row.Field<int>("intGICOde"),
                        varContentType = row.Field<string>("varContentType"),
                        varContentDescription = row.Field<string>("varContentDescription"),
                        chrActive = row.Field<string>("chrActive"),                        
                        dtEntryDate = row.Field<DateTime?>("dtEntryDate")                       
                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return contentType_Master;
        }
        #endregion

        #region GalleryMapping
        public List<Gallery_Mapping> GetGalleryMappingList(int ref_ContentTypeId = 0)
        {
            Blogs_DA blogsDA = new Blogs_DA();
            List<Gallery_Mapping> gallery_Mappings = new List<Gallery_Mapping>();

            try
            {
                DataSet dsResult = blogsDA.GetGalleryMappingList(ref_ContentTypeId);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    gallery_Mappings = dsResult.Tables[0].AsEnumerable().Select(row => new Gallery_Mapping()
                    {
                        CTM_intGlCode = row.Field<int>("CTM_intGlCode"),
                        CTM_varContentType = row.Field<string>("CTM_varContentType"),
                        CM_intGlCode = row.Field<int>("CM_intGlCode"),
                        CM_varAuthor = row.Field<string>("CM_varAuthor"),
                        CM_varTitle = row.Field<string>("CM_varTitle"),
                        CM_varShortDescription = row.Field<string>("CM_varShortDescription"),
                        CM_varContent = row.Field<string>("CM_varContent"),
                        intGICode = row.Field<int>("GM_intGlCode"),
                        varGalleryType = row.Field<string>("GM_varGalleryType"),
                        varGalleryName = row.Field<string>("GM_varGalleryName"),
                        varGalleryPath = row.Field<string>("GM_varGalleryPath"),
                        varGalleryURL = row.Field<string>("GM_varGalleryURL"),
                        varTitle = row.Field<string>("GM_varTitle"),
                        varShortDescription = row.Field<string>("GM_varShortDescription"),
                        varContent = row.Field<string>("GM_varContent"),
                        PM_intGlCode = row.Field<int>("PM_intGlCode"),
                        varProductName = row.Field<string>("PM_varProductName"),
                        charActive = row.Field<string>("GM_chrActive")                        
                    }).ToList();

                }
            }
            catch
            {
                throw;
            }
            return gallery_Mappings;
        }

        public DataSet InsertUpdate_GalleryMappingDetails(GalleryMappingViewModel galleryMappingViewModel)
        {
            Blogs_DA blogsDA = new Blogs_DA();
            try
            {
                return blogsDA.InsertUpdate_GalleryMappingDetails(galleryMappingViewModel);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
