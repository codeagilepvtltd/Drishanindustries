using ProductCataLog.Web.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.Utility;
using ProductCataLog.Lib.ViewModels;
using System;
using System.Data;
using System.Text;
using ProductCataLog.Lib.Repository.Product;
using Microsoft.Extensions.Primitives;

namespace ProductCataLog.Web.Components
{
    public class AdminMenuComponent : ViewComponent
    {
        private readonly IAccountRepository accountRepository;
        private readonly IProductRepository productRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        public AdminMenuComponent(IProductRepository _productRepository, IAccountRepository _accountRepository, IHttpContextAccessor _httpContextAccessor)
        {
            accountRepository = _accountRepository;
            productRepository = _productRepository;
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
            if (Action.ToLower() == "UserMenu".ToLower())
            {
                AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
                adminMenuViewModel = await UserMenu();
                return await Task.Run(() => View("~/Views/Shared/Component/_AdminMenu.cshtml", adminMenuViewModel));
            }
            if (Action.ToLower() == "CategoryMenu".ToLower())
            {
                AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
                adminMenuViewModel = await CategoryLeftMenu();
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
                    sbContent.Append("<li class=\"nav-item\"><a class=\"nav-link collapsed\" href=\"#\" data-toggle=\"collapse\" data-target=\"#collapse" + varDisplayName + "\" aria-expanded=\"true\" aria-controls=\"collapse" + varDisplayName + "\"> <i class=\"" + IconPath + "\"></i><span style='margin-left:7px'>" + varDisplayName + "</span></a>");
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
                    string activeclass = "";
                    if (PageTitle == varDisplayName.Trim().ToLower().Replace(" ", ""))
                    {
                        activeclass = "active";
                    }
                    sbContent.Append("<a class=\"collapse-item " + activeclass + "\" href=\"" + UrlName + "\"><i class=\"" + IconPath + "\" ></i><span style='margin-left:10px'>" + varDisplayName + "</span></a>");
                }
            }
            sbContent.Append("</div></div>");
            sbContent.Append("</li>");
            adminMenuViewModel.AdminHtmlString = sbContent.ToString();
            return adminMenuViewModel;
        }

        private async Task<AdminMenuViewModel> CategoryLeftMenu()
        {
            SessionManager sessionManager = new SessionManager(httpContextAccessor);

            AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
            List<Category_Master> category_Masters = productRepository.GetCategoryList(0).Where(m => m.ref_ParentID == 0).OrderBy(m => m.RankNumber).ToList();

            StringBuilder sbContent = new StringBuilder();
            foreach (Category_Master cm_parent in category_Masters.Where(m => m.ref_ParentID == 0))
            {
                sbContent.Append("<li class=\"accordion block\">");
                sbContent.Append("<div class=\"acc-btn\">");
                sbContent.Append("<div class=\"icon-outer\"><i class=\"fa-solid fa-angle-down\"></i></div><h4>" + cm_parent.varCatergoryName + "</h4> </div>");
            
                List<Category_Master> second_category = productRepository.GetCategoryList(0).Where(m => m.ref_ParentID == cm_parent.intGlCode).OrderBy(m => m.RankNumber).ToList();

                if (second_category != null && second_category.Count > 0)
                {

                    sbContent.Append("<div class=\"acc-content\" style=\"display: none;\">");
                    sbContent.Append("<div class=\"payment-info\">");
                    sbContent.Append("<div class=\"category-widget\">");

                    foreach (Category_Master child_category in second_category)
                    {

                        sbContent.Append("<h3>" + child_category.varCatergoryName + "</h3>");
                        sbContent.Append("<ul class=\"category-list clearfix mb-3\">");
                        string categoryname = child_category.varCatergoryName;

                        List<Product_Master> lstProducts = productRepository.GetProductList(0).Where(m => m.ref_CategoryId == child_category.intGlCode).OrderBy(m => m.RankNumber).ToList();
                        foreach (Product_Master products in lstProducts)
                        {
                            {
                                sbContent.Append("<li><a href='\\products\\" + categoryname.Replace(" ", "-").ToLower() + "\\" + products.varProductName.Replace(" ", "-").ToLower() + "\'>" + products.varProductName + "</a></li>");
                            }
                        }
                        sbContent.Append("</ul>");
                    }
                    sbContent.Append("</div></div></div>");
                }
                else
                {
                    sbContent.Append("<div class=\"acc-content\" style=\"display: none;\">");
                    sbContent.Append("<div class=\"payment-info\">");
                    sbContent.Append("<div class=\"category-widget\">");
                    //sbContent.Append("<h3>" + child_category.varCatergoryName + "</h3>");
                    sbContent.Append("<ul class=\"category-list clearfix mb-3\">");
                    string categoryname = cm_parent.varCatergoryName;
                    List<Product_Master> lstProducts = productRepository.GetProductList(0).Where(m => m.ref_CategoryId == cm_parent.intGlCode).OrderBy(m => m.RankNumber).ToList();

                    foreach (Product_Master products in lstProducts)
                    {
                        {
                            sbContent.Append("<li><a href='\\products\\" + categoryname .Replace(" ","-").ToLower()+"\\"+ products.varProductName.Replace(" ","-").ToLower()+ "\'>" + products.varProductName + "</a></li>");
                        }
                    }
                    sbContent.Append("</ul></div></div></div>");
                }
                sbContent.Append("</li>");
            }
            adminMenuViewModel.AdminHtmlString = sbContent.ToString();
            return adminMenuViewModel;
        }

            private async Task<AdminMenuViewModel> UserMenu()
            {
                SessionManager sessionManager = new SessionManager(httpContextAccessor);

                AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
                DataSet result = accountRepository.Select_MenuMasterList("A");

                string? HeaderName = string.Empty;
                string? Html = string.Empty;
                StringBuilder sbContent = new StringBuilder();
                sbContent.Append("<li class=\"nav-item dropdown no-arrow\"><a class=\"nav-link dropdown-toggle\" href=\"#\" id=\"userDropdown\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"><img class=\"img-profile rounded-circle\" src=\"../img/Admin/img/boy.png\" style=\"max-width: 60px\"><span class=\"ml-2 d-none d-lg-inline text-white small\">" + sessionManager.UserName + "</span> </a>");
                sbContent.Append("<div class=\"dropdown-menu dropdown-menu-right shadow animated--grow-in\" aria-labelledby=\"userDropdown\">");
                foreach (DataRow dr in result.Tables[0].Rows)
                {
                    string? varDisplayName = Convert.ToString(dr["varDisplayName"])?.Trim();
                    string? varControllerName = Convert.ToString(dr["varControllerName"]);
                    string? varActionName = Convert.ToString(dr["varActionName"]);
                    int ref_ParentPageID = Convert.ToInt32(dr["ref_ParentPageID"]);
                    string? UrlName = Convert.ToString(dr["UrlName"]);
                    string? IconPath = Convert.ToString(dr["varIconPath"]);

                    if (varControllerName == "Account" && ref_ParentPageID > 0)
                    {
                        string controllername = httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();
                        sbContent.Append(" <a class=\"dropdown-item\" href=\"" + UrlName + "\"><i class=\"" + IconPath + "\"></i><span style='margin-left:10px'>" + varDisplayName + "</span></a>");


                    }
                }
                sbContent.Append("</div></li>");
                adminMenuViewModel.AdminHtmlString = sbContent.ToString();
                return adminMenuViewModel;
            }
        }
    }
