using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace ProductCataLog.Lib.Repository.Utility
{
    public interface IUtilityRepository
    {
        #region ConfigMaster
        List<Config_Master> GetConfigMasterList(int intGlCode = 0);

        #endregion

        #region ConfigDetails
        List<Config_Details> GetConfigDetailsList(int intGlCode = 0);

        DataSet InsertUpdate_configDetails(ConfigDetailsViewModel configDetailsViewModel);

        #endregion

        #region ContentTypeMaster
        List<ContentType_Master> GetContentTypeMasterList(int intGlCode = 0);

        #endregion

        #region GalleryMapping
        List<Gallery_Mapping> GetGalleryMappingList(int ref_ContentTypeId = 0);

        DataSet InsertUpdate_GalleryMappingDetails(GalleryMappingViewModel galleryMappingViewModel);

        #endregion
    }
}
