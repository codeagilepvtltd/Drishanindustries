﻿using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCataLog.Lib.DA
{
    internal class Blogs_DA
    {
        private DataSet resultSet;

        public DataSet GetContentTypeMasterList(int intGlCode = 0)
        {
            object[] objParamName = { "intGlCode" };
            object[] objParamValue = { intGlCode };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_ContentTypeMasterList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet GetGalleryMappingList(int ref_ContentTypeId = 0)
        {
            object[] objParamName = { "ref_ContentTypeId" };
            object[] objParamValue = { ref_ContentTypeId };

            try
            {
                resultSet = SQLHelper.GetData(StoredProcedures.USP_Select_GalleryMappingList, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }
            return resultSet;

        }

        public DataSet InsertUpdate_GalleryMappingDetails(GalleryMappingViewModel galleryMappingViewModel)
        {
            object[] objParamName = { "ref_ContentId", "ref_GalleryId", "fk_ProductID", "fk_ContentTypeID", "varTitle",
                "varGalleryType","varGalleryName","varGalleryURL","varGalleryPath", "varAuthor", "varShortDescription",
                "varContent", "chrActive", "ref_EntryBy", "@ref_UpdateBy" };
            object[] objParamValue = { galleryMappingViewModel.Gallery_Mapping.CM_intGlCode, galleryMappingViewModel.Gallery_Mapping.CTM_intGlCode,
            galleryMappingViewModel.Gallery_Mapping.fk_ProductID,galleryMappingViewModel.Gallery_Mapping.fk_ContentID,
            galleryMappingViewModel.Gallery_Mapping.varTitle,galleryMappingViewModel.Gallery_Mapping.varGalleryType,
            galleryMappingViewModel.Gallery_Mapping.varGalleryName,galleryMappingViewModel.Gallery_Mapping.varGalleryURL,
            galleryMappingViewModel.Gallery_Mapping.varGalleryPath,galleryMappingViewModel.Gallery_Mapping.CM_varAuthor,
            galleryMappingViewModel.Gallery_Mapping.varShortDescription,galleryMappingViewModel.Gallery_Mapping.varContent,
            galleryMappingViewModel.Gallery_Mapping.charActive,galleryMappingViewModel.Gallery_Mapping.ref_EntryBy,
            galleryMappingViewModel.Gallery_Mapping.ref_UpdateBy};

            try
            {
                return SQLHelper.GetData(StoredProcedures.USP_InsertUpdate_GalleryMapping, objParamName, objParamValue);
            }
            catch
            {
                throw;
            }

        }
    }
}