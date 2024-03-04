using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.Repository.Product;
using ProductCataLog.Lib.Repository.Reports;
using ProductCataLog.Lib.Repository.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IReportsRepository, ReportsRepository>();
builder.Services.AddSingleton<IUtilityRepository, UtilityRepository>();
builder.Services.AddSingleton<IModuleErrorLogRepository, ModuleErrorLogRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRouting();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true; // Set for Session Data : https://stackoverflow.com/questions/49770491/session-variable-value-is-getting-null-in-asp-net-core
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.AccessDeniedPath = "/Home/ErrorForbidden";
                   options.LoginPath = "/Account/Login";
               }
           );

/*For TempData required to register*/
builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
});



var app = builder.Build();
SQLHelper.InitializeConfiguration(app.Configuration);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//app.MapRazorPages();

app.Run();
