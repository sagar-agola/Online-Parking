﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PBS.Api.Extensions;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.DAL;
using PBS.Business.Services;
using PBS.Business.Utilities.Configuration;
using PBS.Business.Utilities.Helpers;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Context;
using Swashbuckle.AspNetCore.Swagger;
using System.Net;
using System.Text;

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
            #region Swagger
            services.AddSwaggerGen (c =>
            {
                c.SwaggerDoc ("v1", new Info ());
            });
            #endregion

            #region Db Context
            services.AddDbContext<PbsDbContext> (item =>
            {
                item.UseSqlServer (Configuration.GetConnectionString ("DBCS"));
            });
            #endregion

            #region Mapping Service
#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper ();
#pragma warning restore CS0618 // Type or member is obsolete
            #endregion

            #region Domain Mapping
            services.AddSingleton<IUserMapping, UserMapping> ();
            #endregion

            #region JWT Token Authentication
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII
                            .GetBytes (Configuration.GetSection ("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            #endregion

            #region Utilities
            services.AddSingleton<IAppConfiguration, AppConfiguration> ();
            services.AddSingleton<ITokenManager, TokenManager> ();
            #endregion

            #region Domain Services
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddScoped<IAuthService, AuthService> ();
            #endregion

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
