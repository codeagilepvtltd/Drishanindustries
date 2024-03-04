using Drishanindustries.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.Repository.Product;
using ProductCataLog.Lib.Repository.Utility;
using ProductCataLog.Lib.ViewModels;
using System.Data;

namespace Drishanindustries.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IUtilityRepository utilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        // GET: Controller


        public UtilityController(IUtilityRepository _utilityRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        #endregion
    }
}
