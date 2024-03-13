using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.ViewModels;
using System.Data;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Drishanindustries.Common;
using System;
using PranicAhmedbad.Common;

namespace Drishanindustries.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IModuleErrorLogRepository moduleErrorLogRepository;
        // GET: Controller


        public AccountController(IAccountRepository _accountRepository, IModuleErrorLogRepository _moduleErrorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            accountRepository = _accountRepository;
            httpContextAccessor = _httpContextAccessor;
            moduleErrorLogRepository = _moduleErrorLogRepository;
        }

        #region Login
        public ActionResult Login()
        {
            return View("Admin/Login");
        }


        [SessionTimeout]
        public async Task<ActionResult> Logout()
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            int fk_LDGLCode = 0;
            string IPAddress = string.Empty;
            string systemName = string.Empty;

            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                fk_LDGLCode = sessionManager.FK_LGLGlCode;
                var IPAddressHostName = (Common_Functions.GetSystemIP()).Split(' ');
                IPAddress = IPAddressHostName[0].ToString();
                systemName = IPAddressHostName[1].ToString();
                InsertUpdate_LoginDetails(fk_LDGLCode, sessionManager.IntGlCode, IPAddress, systemName, "U");
                HttpContext.Session.Clear();
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Login.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        public DataSet InsertUpdate_LoginDetails(int intGlCode, int fk_PersonGlCode, string varSystemIP, string varSystemName, string chrFlag)
        {
            string strResult = string.Empty;
            SessionManager sessionManager = new SessionManager(httpContextAccessor);
            try
            {
                return accountRepository.InsertUpdate_LoginDetails(intGlCode, fk_PersonGlCode,  varSystemIP, varSystemName, chrFlag);
            }
            catch (Exception ex)
            {
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Login.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);
            }
            return null;
        }
        [HttpPost]
        public async Task<ActionResult> ValidateLogin(AccountLoginViewModel accountLoginViewModel, string returnUrl)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

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
                if (!string.IsNullOrEmpty(accountLoginView.LoginMaster.varUserName))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, accountLoginViewModel.LoginMaster.varUserName));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    /*Set Session*/
                    sessionManager.IntGlCode = accountLoginView.LoginMaster.intGlCode;
                    sessionManager.UserName = accountLoginView.LoginMaster.varUserName;

                    var IPAddressHostName = (Common_Functions.GetSystemIP()).Split(' ');
                    string IPAddress = IPAddressHostName[0].ToString();
                    string systemName = IPAddressHostName[1].ToString();

                    DataSet dsResult_Login = accountRepository.InsertUpdate_LoginDetails(0, sessionManager.IntGlCode, IPAddress,systemName,"I");

                    if (dsResult_Login != null && dsResult_Login.Tables.Count > 0 && dsResult_Login.Tables[0].Rows.Count > 0)
                    {
                        int fk_LDLGlCode = 0;
                        int.TryParse(Convert.ToString(dsResult_Login.Tables[0].Rows[0]["intGlCode"]), out fk_LDLGlCode);
                        sessionManager.FK_LGLGlCode = fk_LDLGlCode;
                    }


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
                moduleErrorLogRepository.Insert_Modules_Error_Log("Login", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return View();
            }
        }

        #endregion

        #region Login Master (Users)
        public ActionResult Users()
        {
            return View("Admin/Users");
        }

        public IActionResult GetLoginMasterList()
        {

            LoginMasterViewModel loginMasterViewModel = new LoginMasterViewModel();
            DataSet dsResult = new DataSet();

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            try
            {
                loginMasterViewModel = accountRepository.GetLoginMasterlist();
                var resultJson = JsonConvert.SerializeObject(loginMasterViewModel.login_Masters);
                return Content(resultJson, "application/json");
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Login.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorForbidden", "Account");
            }
        }

        [HttpPost]
        public ActionResult Save_LoginMaster(LoginMasterViewModel loginmasterView)
        {

            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            try
            {

                loginmasterView.login_Master.ref_EntryBy = Convert.ToInt64(sessionManager.IntGlCode);
                loginmasterView.login_Master.ref_UpdateBy = Convert.ToInt64(sessionManager.IntGlCode);
                loginmasterView.login_Master.chrActive = loginmasterView.login_Master.chrActive == "Active" ? "Y" : "N";
                loginmasterView.login_Master.chrLock = loginmasterView.login_Master.chrActive == "Yes" ? "Y" : "N";

                DataSet result = accountRepository.InsertUpdate_LoginMaster(loginmasterView);
                var resultJson = JsonConvert.SerializeObject(result);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Failed_Message, PageNames.Login.ToString());
                    return Content(resultJson, "application/json");
                }
                else
                {
                    TempData["ErrorMessage"] = string.Format(Common_Messages.Save_Success_Message, PageNames.Login.ToString());
                    return Content(resultJson, "application/json");
                }
            }
            catch (Exception ex)
            {
                SQLHelper.writeException(ex);
                moduleErrorLogRepository.Insert_Modules_Error_Log(PageNames.Login.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), Convert.ToString(sessionManager.IntGlCode), ex.StackTrace, this.GetType().Name.ToString(), Drishanindustries.Common.Common.AppName, ex.Source, "", "", ex.Message);

                return Content(JsonConvert.SerializeObject(0));
            }
        }
        #endregion


    }
}
