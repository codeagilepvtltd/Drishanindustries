using ProductCataLog.Web.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.Repository.Product;
using ProductCataLog.Lib.ViewModels;
using System.Data;

namespace ProductCataLog.Web.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashboardRepository dashboardRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DashBoardController(IWebHostEnvironment webHostEnvironment, IDashboardRepository _dashboardRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            dashboardRepository = _dashboardRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        public IActionResult Index()
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            DataSet dsResult = new DataSet();
            try
            {
                dashboardViewModel = dashboardRepository.Select_DashboardSummary();
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.ProductContent.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), ProductCataLog.Web.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
            return View("Admin/Index", dashboardViewModel);
        }
    }
}
