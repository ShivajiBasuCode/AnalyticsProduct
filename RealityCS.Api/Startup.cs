using System;
using System.Collections.Generic;
using System.Linq;
using RealityCS.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RealityCS.DataLayer.Context;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using AutoMapper;
using RealityCS.DTO;
using RealityCS.ServiceCollectionExtensions.API;
using RealityCS.Api.Filters;
using RealityCS.SharedMethods;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using RealityCS.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using RealityCS.Core.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace RealityCS.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env/*IConfiguration configuration*/)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Env = env;
            //Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
       private IWebHostEnvironment Env;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            //Add Support for strongly typed Configuration and map to class
            services.AddOptions();
            services.Configure<JwtTokenOptions>(Configuration.GetSection("Tokens"));

            services.AddJwtConfiguration(Configuration);
            services.AddDbContextConfiguration(Configuration);

            services.AddIdentityConfiguration();

            services.Configure<FormOptions>(x => {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = null;
            });

            #region  Auto Mapper Configurations
            services.AddEntityMappingConfiguration();

            #endregion

            #region Logging

            //services.AddScoped<LoggingFilter>();
            //services.AddControllers(config =>
            //{
            //    config.Filters.Add<LoggingFilter>();
            //});
            #endregion

            services.AddControllers(options =>
            {

            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            #region Register the Swagger generator, defining 1 or more Swagger documents
            //https://github.com/Redocly/redoc/issues/692
            services.AddSwaggerConfiguration(Env);
            #endregion


            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });

            services.AddHttpContextAccessor();

            services.AddMvc();
            services.AddScoped<IIdentityManager, IdentityManager>();
            services.AddBusinessLogicConfiguration(Configuration, Env);
            AddNopMvc(services);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });

        }
        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        protected IMvcBuilder AddNopMvc(IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddControllersWithViews();

            mvcBuilder.AddRazorRuntimeCompilation();

        

            services.AddRazorPages();

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //add custom display metadata provider
           // mvcBuilder.AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new NopMetadataProvider()));

            //add custom model binder provider (to the top of the provider list)
           // mvcBuilder.AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new NopModelBinderProvider()));

            //add fluent validation
            mvcBuilder.AddFluentValidation(configuration =>
            {
                //register all available validators from Nop assemblies
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("Realitycs", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);
                configuration.RegisterValidatorsFromAssemblies(assemblies);

                //implicit/automatic validation of child properties
                configuration.ImplicitlyValidateChildProperties = true;
            });

            //register controllers as services, it'll allow to override them
            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            RealitycsEngine._serviceProvider = app.ApplicationServices;


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            #region Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api-dev";

            });
            app.UseReDoc(c =>
            {
                c.SpecUrl = "../swagger/v1/swagger.json";
                c.DocumentTitle = "RealityCS API";
                c.HideDownloadButton();
                c.HideHostname();                
                //c.ExpandResponses("200,201");
                //c.RequiredPropsFirst();
                //c.NoAutoAuth();
                //c.PathInMiddlePanel();
                //c.HideLoading();
                //c.NativeScrollbars();
                //c.DisableSearch();
                //c.OnlyRequiredInSamples();
                //c.SortPropsAlphabetically();


            });
            #endregion

            app.UseResponseCompression();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                try
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature.Error;

                    // var result = JsonConvert.SerializeObject(new { error = exception.Message, stacktrace = exception.StackTrace, innerexception = exception.InnerException });
#warning Logs will saved 

                    context.Response.ContentType = RealitycsConstants.MimeTypes.ApplicationJson;//  "application/json";
                    var messages = new List<object>() { "An error occured while processing you request!" };
                    await context.Response.WriteAsync(new ApiResponse<object>()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Error = messages,
                        IsSuccess = false
                    }.ToJson());
                    return;
                }
                catch(Exception ex)
                {

                }
            }));

            //check whether requested page is keep alive page
            app.UseKeepAlive();

            //check whether database is installed
            app.UseInstallUrl();


           app.UseRouting();

            app.UseCors(options =>
            {

                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapControllerRoute(
                //       name: "arearoute",
                //       pattern: "api/{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "api/{controller}/{action}/{id?}");

                endpoints.MapDefaultControllerRoute();

            });

            app.UseSpa(options =>
            {
                options.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    options.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }

   
}
