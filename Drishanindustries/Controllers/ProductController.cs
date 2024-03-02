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

namespace Drishanindustries.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;

        public ProductController(IProductRepository _productRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion

        #region Product
        public IActionResult Product()
        {
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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }

        public IActionResult GetProductList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion
    }
}
