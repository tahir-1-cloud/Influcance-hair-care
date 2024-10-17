//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddDirectoryBrowser();
//try
//{
//    // TODO: This does not throw
//    builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//app.UseDeveloperExceptionPage();
//app.UseDatabaseErrorPage();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseFileServer(enableDirectoryBrowsing: true);

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Security}/{action=Login}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
