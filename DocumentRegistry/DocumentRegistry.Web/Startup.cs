using System;
using System.IO;
using DocumentRegistry.Web.Infrastructure;
using DocumentRegistry.Web.Services.CompanyService;
using DocumentRegistry.Web.Services.DocumentDirectionService;
using DocumentRegistry.Web.Services.DocumentTypeService;
using DocumentRegistry.Web.Services.EmployeeService;
using DocumentRegistry.Web.Services.HomeService;
using DocumentRegistry.Web.Services.LetterService;
using DocumentRegistry.Web.Services.PostCompanyService;
using DocumentRegistry.Web.Services.UserService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DocumentRegistry.Web
{
    public class Startup
    {
        public Startup()
        {
            Configuration.SetConfiguration();
        }

        public Configuration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            services.AddControllersWithViews().AddSessionStateTempDataProvider();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILetterService, LetterService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPostCompanyService, PostCompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IDocumentDirectionService, DocumentDirectionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            Log.Information(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"hane/apps/DocumentsRegistry/Web");
            
            app.UseRouting();

            app.UseSession();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}