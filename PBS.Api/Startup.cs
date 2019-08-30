using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBS.Api.Extensions;
using PBS.Api.ServiceInstallers;
using System.Net;

namespace PBS.Api
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
            services.InstallServices (Configuration);
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }
            else
            {
                app.UseExceptionHandler (builder =>
                {
                    builder.Run (async context =>
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature> ();
                        if (error != null)
                        {
                            context.Response.AddApplicationError (error.Error.Message);
                            await context.Response.WriteAsync (error.Error.Message);
                        }
                    });
                });
                //app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyHeader ().AllowAnyMethod ());
            app.UseAuthentication ();
            app.UseMvc ();
            app.UseSwagger ();
            app.UseSwaggerUI (x =>
            {
                x.SwaggerEndpoint ("/swagger/v1/swagger.json", "Parking Booking System API");
            });
        }
    }
}
