using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.Utility;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Data;
using System.Text;

namespace Drishanindustries.Components
{
    public class AdminMenuComponent : ViewComponent
    {
        private readonly IAccountRepository accountRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AdminMenuComponent(IAccountRepository _accountRepository, IHttpContextAccessor _httpContextAccessor)
        {
            accountRepository = _accountRepository;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Action)
        {
            if (Action.ToLower() == "AdminMenu".ToLower())
            {
                AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
                adminMenuViewModel = await AdminMenu();
                return await Task.Run(() => View("~/Views/Shared/Component/_AdminMenu.cshtml", adminMenuViewModel));
            }
            return View();
        }
        private async Task<AdminMenuViewModel> AdminMenu()
        {
            AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
            DataSet result = accountRepository.Select_MenuMasterList("A");

            string? HeaderName = string.Empty;
            string? Html = string.Empty;
            StringBuilder sbContent = new StringBuilder();
            foreach (DataRow dr in result.Tables[0].Rows)
            {
                string? varDisplayName = Convert.ToString(dr["varDisplayName"])?.Trim();
                string? varControllerName = Convert.ToString(dr["varControllerName"]);
                string? varActionName = Convert.ToString(dr["varActionName"]);
                int ref_ParentPageID = Convert.ToInt32(dr["ref_ParentPageID"]);
                string? UrlName = Convert.ToString(dr["UrlName"]);
                string? IconPath = Convert.ToString(dr["varIconPath"]);

                string controllername = httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();

                if (ref_ParentPageID == 0)
                {

                    sbContent.Append("<li class=\"nav-item\"><a class=\"nav-link collapsed\" href=\"#\" data-toggle=\"collapse\" data-target=\"#collapse" + varDisplayName + "\" aria-expanded=\"true\" aria-controls=\"collapse" + varDisplayName + "\"> <i class=\"" + IconPath + "\"></i><span>" + varDisplayName + "</span></a>");
                    if (controllername?.ToLower() == varControllerName?.ToLower())
                        sbContent.Append("<div id = \"collapse" + varDisplayName + "\" class=\"collapse show\" aria-labelledby=\"headingBootstrap\" data-parent=\"#accordionSidebar\">");
                    else
                        sbContent.Append("<div id = \"collapse" + varDisplayName + "\" class=\"collapse\" aria-labelledby=\"headingBootstrap\" data-parent=\"#accordionSidebar\">");
                    sbContent.Append("<div class=\"bg-white py-2 collapse-inner rounded\">");
                    HeaderName = varDisplayName;
                    if (HeaderName != null && HeaderName != varDisplayName)
                    {
                        sbContent.Append("</div></div>");
                        sbContent.Append("</li>");
                    }
                }
                else
                {
                    string? PageTitle = Convert.ToString(ViewData["Title"]).Split('-')[1].Trim().ToLower().Replace(" ", "");

                    if (PageTitle == varDisplayName.Trim().ToLower().Replace(" ", ""))
                    {
                        sbContent.Append("<a class=\"collapse-item active\" href=\"" + UrlName + "\">" + varDisplayName + "</a>");
                    }
                    else

                    {
                        sbContent.Append("<a class=\"collapse-item\" href=\"" + UrlName + "\">" + varDisplayName + "</a>");
                    }
                }
            }
            sbContent.Append("</div></div>");
            sbContent.Append("</li>");
            adminMenuViewModel.AdminHtmlString = sbContent.ToString();
            return adminMenuViewModel;
        }

    }
}
