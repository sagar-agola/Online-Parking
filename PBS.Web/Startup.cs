using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBS.Business.Utilities.Configuration;
using PBS.Business.Utilities.Helpers;
using PBS.Business.Utilities.MailClient;
using PBS.Web.Helpers;
using Rotativa.AspNetCore;

namespace PBS.Web
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions> (options =>
             {
                 options.CheckConsentNeeded = context => true;
                 options.MinimumSameSitePolicy = SameSiteMode.None;
             });

            services.AddSingleton<IApiHelper, ApiHelper> ();
            services.AddSingleton<ITokenDecoder, TokenDecoder> ();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
            services.AddSingleton<IEncryptionHelper, EncryptionHelper> ();
            services.AddSingleton<IWebConfiguration, WebConfiguration> ();
            services.AddSingleton<IMailClient, MailClient> ();
            services.AddSingleton<DataProtectionPurposeStrings> ();

            services.AddSession ();

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }
            else
            {
                app.UseExceptionHandler ("/Home/Error");
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseCookiePolicy ();
            app.UseSession ();

            RotativaConfiguration.Setup (env, "..\\Rotativa\\");

            app.UseMvc (routes =>
            {
                 routes.MapRoute (
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}"
                 );

                 routes.MapRoute (
                   name: "Admin",
                   template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                 );
            });
        }
    }
}