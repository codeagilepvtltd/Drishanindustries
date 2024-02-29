using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.ViewModels;
using System.Data;
using System.Net;

namespace Drishanindustries.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        //private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        // GET: Controller


        public AccountController(IAccountRepository _accountRepository, IModuleErrorLogRepository _moduleErrorLogRepository)
        {
            accountRepository = _accountRepository;
            //httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        #region Login
        public ActionResult Login()
        {
            return View("Admin/Login");
        }

        [HttpPost]
        public async Task<ActionResult> ValidateLogin(AccountLoginViewModel accountLoginViewModel, string returnUrl)
        {
            try
            {
                string decodedUrl = "";

                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = WebUtility.UrlDecode(returnUrl);

                if (string.IsNullOrEmpty(accountLoginViewModel.LoginMaster.varUserName))
                {
                    TempData["ErrorMessage"] = "Please Enter User Name";
                    return RedirectToAction(nameof(Index));
                }

                if (string.IsNullOrEmpty(accountLoginViewModel.LoginMaster.varPassword))
                {
                    TempData["ErrorMessage"] = "Please Enter Password";
                    return RedirectToAction(nameof(Index));
                }

                AccountLoginViewModel accountLoginView = accountRepository.CheckAuthentication(accountLoginViewModel);
                if(!string.IsNullOrEmpty(accountLoginView.LoginMaster.varUserName))
                {
                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid User Name/Password, Please Try Again!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);

                return View();
            }
        }

        #endregion

        #region Login Master (Users)
        public ActionResult Users()
        {
            return View("Admin/Users");
        }

        //public IActionResult GetCustomerList()
        //{

        //    LoginMasterViewModel loginMasterViewModel = new LoginMasterViewModel();
        //    DataSet dsResult = new DataSet();
        //    try
        //    {
        //        loginMasterViewModel = accountRepository.loginMasterViewModel();
        //        var resultJson = JsonConvert.SerializeObject(loginMasterViewModel.login_Master);
        //        return Content(resultJson, "application/json");
        //    }
        //    catch (Exception ex)
        //    {
        //        //ModuleErrorLogRepository.Insert_Modules_Error_Log("GetPersonDetails", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), "Novapack", ex.Source, "", "", ex.Message);
        //        TempData["ErrorMessage"] = ex.Message;
        //        return RedirectToAction("ErrorForbidden", "Account");
        //    }
        //}

        //public ActionResult Save_Customer(CustomerMasterViewModel customerView)
        //{
        //    try
        //    {
        //        customerView.customer_Master.ref_EntryBy = 1;
        //        customerView.customer_Master.ref_UpdateBy = 1;
        //        customerView.customer_Master.chrActive = customerView.customer_Master.chrActive == "true" ? "Y" : "N";
        //        DataSet result = accountRepository.InsertUpdate_Customer(customerView);
        //        var resultJson = JsonConvert.SerializeObject(result);

        //        if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
        //        {
        //            TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, "Customer");
        //            return Content(resultJson, "application/json");
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, "Customer");
        //            return Content(resultJson, "application/json");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SQLHelper.writeException(ex);

        //        return Content(JsonConvert.SerializeObject(0));
        //    }
        //}
        #endregion

    }
}
