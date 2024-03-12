using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.Product;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.ViewModels;
using System.Data;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Drishanindustries.Common;
using System;
using Microsoft.AspNetCore.Hosting;

namespace Drishanindustries.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment, IProductRepository _productRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            productRepository = _productRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        #region Category
        public IActionResult Index()
        {
            return View("Admin/Category_Master");
        }

        [HttpPost]
        public ActionResult Save_Category(CategoryMasterViewModel categoryView)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            try
            {
                categoryView.category_Master.ref_EntryBy = Convert.ToInt64(sessionManager.IntGlCode);
                categoryView.category_Master.ref_UpdateBy = Convert.ToInt64(sessionManager.IntGlCode);
                categoryView.category_Master.chrActive = categoryView.category_Master.chrActive == "true" ? "Y" : "N";
                DataSet result = productRepository.InsertUpdate_category(categoryView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Category");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Category");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetCategoryList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            CategoryMasterViewModel Category_Master = new CategoryMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Category_Master.Category_Masters = productRepository.GetCategoryList(intGlCode);
                var resultJson = JsonConvert.SerializeObject(Category_Master.Category_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion

        #region Product
        public IActionResult Product()
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            sessionManager.SelectedProductId = 0;
            return View("Admin/Product_Master");
        }

        [HttpPost]
        public ActionResult Save_Product(ProductMasterViewModel productView)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            try
            {
                productView.product_master.ref_EntryBy = Convert.ToInt64(sessionManager.IntGlCode);
                productView.product_master.ref_UpdateBy = Convert.ToInt64(sessionManager.IntGlCode);
                productView.product_master.chrActive = productView.product_master.chrActive == "true" ? "Y" : "N";
                DataSet result = productRepository.InsertUpdate_product(productView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Product");
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Product");
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetProductList(int intGlCode = 0)
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            if (sessionManager.SelectedProductId > 0)
                intGlCode = sessionManager.SelectedProductId;

            ProductMasterViewModel Product_Master = new ProductMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                Product_Master.product_masters = productRepository.GetProductList(intGlCode);
                var resultJson = JsonConvert.SerializeObject(Product_Master.product_masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion

        #region ProductImage/Video
        public IActionResult ProductContentMaster(int id)
        {
            if (id > 0)
            {
                SessionManager sessionManager = new SessionManager(httpContextAccessor);
                sessionManager.SelectedProductId = id;
            }
            ViewBag.Message = TempData["Message"];
            ViewBag.MessageType = TempData["MessageType"];
            return View("Admin/Content_Master");
        }

        public IActionResult GetContentTypeMasterList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            ProductContentTypeMasterViewModel ProductContent_Master = new ProductContentTypeMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                ProductContent_Master.contentType_Masters = productRepository.GetContentTypeMasterList(ProductCataLog.Lib.Common.ContentTypePurpose.Product.ToString());
                var resultJson = JsonConvert.SerializeObject(ProductContent_Master.contentType_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        public IActionResult GetGalleryMappingList(int intGlCode = 0)
        {
            int intProductId = 0;
          
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            if (sessionManager.SelectedProductId > 0)
                intProductId = sessionManager.SelectedProductId;


            ProductContentTypeMasterViewModel ProductContent_Master = new ProductContentTypeMasterViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                if (intProductId > 0)
                    ProductContent_Master.gallery_Mappings = productRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Product.ToString()).Where(p => p.fk_ProductID == intProductId).ToList();
                else
                    ProductContent_Master.gallery_Mappings = productRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Product.ToString()).Where(p => p.fk_ProductID != 0).ToList();

                var resultJson = JsonConvert.SerializeObject(ProductContent_Master.gallery_Mappings);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        [HttpPost]
        public ActionResult Save_Gallery(ProductContentTypeMasterViewModel galleyView)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            try
            {
                galleyView.gallery_Mapping.ref_EntryBy = Convert.ToInt64(sessionManager.IntGlCode);
                galleyView.gallery_Mapping.ref_UpdateBy = Convert.ToInt64(sessionManager.IntGlCode);
                galleyView.gallery_Mapping.charActive = galleyView.gallery_Mapping.charActive == "true" ? "Y" : "N";
                if (galleyView.gallery_Mapping.varGalleryType == "Gallery")
                {
                    galleyView.gallery_Mapping.varGalleryPath = UploadedFile(galleyView, "/UploadFiles/Product/images");
                }
                else if (galleyView.gallery_Mapping.varGalleryType == "Document")
                {
                    galleyView.gallery_Mapping.varGalleryPath = UploadedFile(galleyView, "/UploadFiles/Product/document");
                }
                else if (galleyView.gallery_Mapping.varGalleryType == "Video")
                {
                    galleyView.gallery_Mapping.varGalleryURL = galleyView.gallery_Mapping.varGalleryPath;
                }
                if (!string.IsNullOrEmpty(galleyView.gallery_Mapping.varGalleryPath))
                {
                    galleyView.gallery_Mapping.varGalleryName = Path.GetFileName(galleyView.gallery_Mapping.varGalleryPath);
                }
                DataSet result = productRepository.InsertUpdate_GalleryMapping(galleyView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["Message"] = string.Format(Common_Messages.Save_Success_Message, PageNames.ProductContent.ToString());
                    TempData["MessageType"] = "Success";
                }
                else
                {
                    TempData["Message"] = string.Format(Common_Messages.Save_Failed_Message, PageNames.ProductContent.ToString());
                    TempData["MessageType"] = "Error";
                }
                return RedirectToAction(nameof(ProductContentMaster));
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        private string UploadedFile(ProductContentTypeMasterViewModel model, string RootFolder)
        {
            string uniqueFileName = string.Empty;
            string filePath = string.Empty;
            if (model.gallery_Mapping.UploadedImage != null)
            {
                string uploadsFolder = string.Empty;
                uploadsFolder = string.Concat(_webHostEnvironment.WebRootPath, RootFolder);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(model.gallery_Mapping.UploadedImage.FileName);
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.gallery_Mapping.UploadedImage.CopyTo(fileStream);
                }
            }
            else if (!string.IsNullOrEmpty(model.gallery_Mapping.varGalleryPath))
            {
                filePath = model.gallery_Mapping.varGalleryPath;
            }
            string ReturntValue = string.Empty;
            if (model.gallery_Mapping.varGalleryType == "Gallery")
            {
                ReturntValue = @"\UploadFiles\product\images\" + Path.GetFileName(filePath);
            }
            else if (model.gallery_Mapping.varGalleryType == "Document")
            {
                ReturntValue = @"\UploadFiles\product\document\" + Path.GetFileName(filePath);
            }
            return ReturntValue;
        }
        #endregion
    }
}
