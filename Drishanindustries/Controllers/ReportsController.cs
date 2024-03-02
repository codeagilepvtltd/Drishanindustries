using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.Product;
using ProductCataLog.Lib.Repository.Reports;
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
    public class ReportsController : Controller
    {
        private readonly IReportsRepository reportsRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;

        public ReportsController(IReportsRepository _reportsRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            reportsRepository = _reportsRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetContactUsList(int intGlCode = 0)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            ContactUsViewModel ContactUs = new ContactUsViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                ContactUs.ContactUsList = reportsRepository.GetContactUsList(intGlCode);
                var resultJson = JsonConvert.SerializeObject(ContactUs.ContactUsList);
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
    }
}
