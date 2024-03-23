﻿using ProductCataLog.Web.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.Repository.Product;
using ProductCataLog.Lib.Repository.Utility;
using ProductCataLog.Lib.ViewModels;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace ProductCataLog.Web.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IUtilityRepository utilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // GET: Controller

        public UtilityController(IWebHostEnvironment webHostEnvironment, IUtilityRepository _utilityRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            utilityRepository = _utilityRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        #region Config Detail
        public IActionResult Config()
        {
            return View("Admin/Config");
        }

        [HttpPost]
        public ActionResult Save_ConfigDetails(ConfigDetailsViewModel configView)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            try
            {
                configView.config_Detail.ref_EntryBy = Convert.ToInt64(sessionManager.IntGlCode);
                configView.config_Detail.ref_UpdateBy = Convert.ToInt64(sessionManager.IntGlCode);
                configView.config_Detail.chrActive = configView.config_Detail.chrActive == "true" ? "Y" : "N";
                DataSet result = utilityRepository.InsertUpdate_configDetails(configView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Config");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Config");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetConfigDetailsList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            ConfigDetailsViewModel Config_Details = new ConfigDetailsViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Config_Details.config_Details = utilityRepository.GetConfigDetailsList(intGlCode);
                var resultJson = JsonConvert.SerializeObject(Config_Details.config_Details);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        public IActionResult GetConfigMasterList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            ConfigMasterViewModel Config_Masters = new ConfigMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Config_Masters.config_Masters = utilityRepository.GetConfigMasterList(intGlCode);
                var resultJson = JsonConvert.SerializeObject(Config_Masters.config_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion        

        #region Blogs
        public IActionResult Content()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.MessageType = TempData["MessageType"];

            return View("Admin/Content");
        }

        private string UploadedFile(GalleryMappingViewModel model, string RootFolder)
        {
            string uniqueFileName = string.Empty;
            string filePath = string.Empty;
            if (model.Gallery_Mapping.UploadedImage != null)
            {
                string uploadsFolder = string.Concat(_webHostEnvironment.WebRootPath, RootFolder);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(model.Gallery_Mapping.UploadedImage.FileName);
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Gallery_Mapping.UploadedImage.CopyTo(fileStream);
                }
            }
            else if (!string.IsNullOrEmpty(model.Gallery_Mapping.varGalleryPath))
            {
                filePath = model.Gallery_Mapping.varGalleryPath;
            }
            return RootFolder + Path.GetFileName(filePath);
        }

        public IActionResult GetGalleryMappingList(int ref_ContentTypeId = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            GalleryMappingViewModel Gallery_Mapping = new GalleryMappingViewModel();
            DataSet dsResult = new DataSet();
            try
            {

                Gallery_Mapping.Gallery_Mappings = utilityRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Utility.ToString()).ToList();
                var resultJson = JsonConvert.SerializeObject(Gallery_Mapping.Gallery_Mappings);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Content.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

         [HttpPost]
        public ActionResult Save_Gallery(IFormFile image, GalleryMappingViewModel galleyView)
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            try
            {                
                galleyView.Gallery_Mapping.CTM_intGlCode = galleyView.Gallery_Mapping.CTM_intGlCode;
                galleyView.Gallery_Mapping.varGalleryType = galleyView.Gallery_Mapping.varGalleryType;
                galleyView.Gallery_Mapping.varGalleryPath = UploadedFile(galleyView, "/UploadFiles/content/");
                if (!string.IsNullOrEmpty(galleyView.Gallery_Mapping.varGalleryPath))
                {
                    galleyView.Gallery_Mapping.varGalleryName = Path.GetFileName(galleyView.Gallery_Mapping.varGalleryPath);
                }
                galleyView.Gallery_Mapping.ref_EntryBy = Convert.ToInt64(sessionManager.IntGlCode);
                galleyView.Gallery_Mapping.ref_UpdateBy = Convert.ToInt64(sessionManager.IntGlCode);
                galleyView.Gallery_Mapping.charActive = galleyView.Gallery_Mapping.charActive == "true" ? "Y" : "N";
                galleyView.Gallery_Mapping.fk_ContentTypeID = galleyView.Gallery_Mapping.fk_ContentTypeID;
                DataSet result = utilityRepository.InsertUpdate_GalleryMappingDetails(galleyView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["Message"] = string.Format(Common_Messages.Save_Success_Message, PageNames.Content.ToString());
                    TempData["MessageType"] = "Success";
                }
                else
                {
                    TempData["Message"] = string.Format(Common_Messages.Save_Failed_Message, PageNames.Content.ToString());
                    TempData["MessageType"] = "Error";                   
                }
                return RedirectToAction(nameof(Content));
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Content.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetContentTypeMasterList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            ContentTypeViewModel Content_Master = new ContentTypeViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Content_Master.ContentType_Masters = utilityRepository.GetContentTypeMasterList(ProductCataLog.Lib.Common.ContentTypePurpose.Utility.ToString());
                var resultJson = JsonConvert.SerializeObject(Content_Master.ContentType_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Content.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion
    }
}