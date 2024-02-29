using ProductCataLog.Lib.Common;
using ProductCataLog.Lib.Repository.Account;
using ProductCataLog.Lib.Repository.ModuleErrorLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IModuleErrorLogRepository, ModuleErrorLogRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRouting();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

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
