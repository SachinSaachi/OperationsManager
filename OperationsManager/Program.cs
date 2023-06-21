using OManager_Core.DataAccessLayer;
using OManager_Core.businessLogic;
using Common.AppConfiguration.Common;
using HRMS_Core.DataAccessLayer.DatabaseHelper.Datablase.DatabaseHelper;
using Microsoft.CodeAnalysis;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAppConfiguration, AppConfiguration>();
builder.Services.AddScoped<IExecuteQuery, ExecuteQuery>();
builder.Services.AddScoped<IDLItIssue, DLItIssue>();
builder.Services.AddScoped<IBLItissue, BLItIssue>();
builder.Services.AddRazorPages();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(20); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

