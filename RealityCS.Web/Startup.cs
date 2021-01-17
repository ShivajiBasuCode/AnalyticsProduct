using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Routing;
using RealityCS.Web.Utility;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RealityCS.Web.Filters;

namespace RealityCS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //   .AddCookie();




            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme
            /*options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }*/)
         .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
         {
             // cookie expiration should be set the same as the token expiry (the default is 5
             // mins). The token generator doesn't provide auto-refresh of an expired token so the
             // user will be logged out the next time they try to access a secured endpoint. They
             // will simply have to re-login and acquire a new token and by extension a new cookie.
             // Perhaps in the future I can add some kind of hooks in the token generator that can
             // let the referencing application know that the token has expired and the developer
             // can then request a new token without the user having to re-login.
             // options.Cookie.Expiration = TimeSpan.FromMinutes(1);

             // Specify the TicketDataFormat to use to validate/create the ASP.NET authentication
             // ticket. Its important that the same validation parameters are passed to this class
             // so that the token validation works correctly. The framework will call the
             // appropriate methods in JwtAuthTicketFormat based on whether the cookie is being
             // sent out or coming in from a previously authenticated user. Please bear in mind
             // that if the incoming token is invalid (may be it was tampered or spoofed) the
             // Unprotect() method in JwtAuthTicketFormat will simply return null and the
             // authentication will fail.
             //options.TicketDataFormat=


             options.LoginPath = new PathString("/");
             options.LogoutPath = new PathString("/Account/Logout");
             options.AccessDeniedPath = options.LoginPath;


             //  options.ReturnUrlParameter = authUrlOptions?.ReturnUrlParameter ?? "returnUrl";
         });            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });

          
            

            services.AddControllersWithViews(option =>
            {
             
            })            
            .AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // add here your route constraint   
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("CompanyName", typeof(CompanyNameConstraint));
                routeOptions.ConstraintMap.Add("LoginType", typeof(LoginTypeConstraint));
            });

            #region Logging

            services.AddScoped<LoggingFilter>();
            services.AddControllers(config =>
            {
                config.Filters.Add<LoggingFilter>();
            });
            #endregion

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
            }

            
            app.UseRequestLocalization(new string[] { "en-GB" });

            app.UseResponseCompression();

            app.UseStaticFiles();
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message, stacktrace = exception.StackTrace, innerexception = exception.InnerException });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Return custom header value
            app.Use(async (context, next) =>
            {
                var headerValues = context.Request.Headers.Where(x => x.Key.Contains("X-Frmwk-")).ToDictionary(x => x.Key, x => x.Value);

                foreach (var item in headerValues)
                {
                    context.Response.Headers.Add(item.Key, item.Value);
                }

                await next();
            });


            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                        name: "arearoute",                        
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");

               // endpoints.MapControllers();

            });


        }
    }
}
