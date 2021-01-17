using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.Customer;
using RealityCS.BusinessLogic.Global;
using RealityCS.BusinessLogic.GraphicalEntity;
using RealityCS.BusinessLogic.Import;
using RealityCS.BusinessLogic.Installation;
using RealityCS.BusinessLogic.KPIEntity;
using RealityCS.BusinessLogic.Security;
using RealityCS.Core.Helper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.BaseContext;
using RealityCS.DataLayer.Context.GraphicalEntity;
using RealityCS.DataLayer.Context.KPIEntity;
using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsEnumeration;
using RealityCS.DataLayer.Context.RealitycsShared;
using RealityCS.SharedMethods;
using RealityCS.SharedMethods.FileProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.ServiceCollectionExtensions.API
{
   public static class BusinessLogicConfigurationServiceCollectionExtensions
    {

        public static IServiceCollection AddBusinessLogicConfiguration(this IServiceCollection services,IConfiguration configuration,IWebHostEnvironment hostingEnvironment)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //data layer
            services.AddTransient<IDataProviderManager, DataProviderManager>();

            //data provider
            services.AddTransient(provider => provider.GetService<IDataProviderManager>().DataProvider);

            services.AddScoped<IRealitycsBaseContext>(provider => new RealitycsClientContext(provider.GetService<DbContextOptions<RealitycsClientContext>>()));

            services.AddScoped<IRealitycsBaseContext>(provider => new RealitycsSharedContext(provider.GetService<DbContextOptions<RealitycsSharedContext>>()));
            services.AddScoped<IRealitycsBaseContext>(provider => new RealitycsEnumerationContext(provider.GetService<DbContextOptions<RealitycsEnumerationContext>>()));
            //Added KPI Elements Context to data provider
            services.AddScoped<IRealitycsBaseContext>(provider => new RealitycsKPIContext(provider.GetService<DbContextOptions<RealitycsKPIContext>>()));
            //Added Graphical Elements Context to data provider
            services.AddScoped<IRealitycsBaseContext>(provider => new RealitycsGraphicalContext(provider.GetService<DbContextOptions<RealitycsGraphicalContext>>()));

            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<IInstallationService, CodeFirstInstallationService>();


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddSingleton<IRealitycsDataProvider, MsSqlDataProvider>();
            RealitycsCommonHelper.DefaultFileProvider = new RealitycsFileProvider(hostingEnvironment);

            services.AddScoped<IWebHelper, WebHelper>();
            //services.AddScoped<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IRealitycsFileProvider, RealitycsFileProvider>();
            services.AddScoped<IInstallationLocalizationService, InstallationLocalizationService>();


            #region  Add logics to service collection           
            services.AddScoped<IFillComboLogic, FillComboLogic>();
            services.AddScoped<IRealitycsImportManagerConfiguration, RealitycsImportManager>();
            services.AddScoped<IClientUser, ClientUser>();
            services.AddScoped<IRealitycsUserRegistrationService, RealitycsUserRegistrationService>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IEncryptionService, EncryptionService>();

            //Service for Legal Entity operations
            services.AddScoped<ILegalEntityService, LegalEntityService>();
            //Service for Industry operations
            services.AddScoped<IIndustryService, IndustryService>();
            //Service for Department
            services.AddScoped<IDepartmentService, DepartmentService>();
            //Service for Division
            services.AddScoped<IDivisionService, DivisionService>();
            //Services for Country, State and City 
            services.AddScoped<IRealitycsGeographicLocationService, RealitycsGeographicLocationService>();
            //Service for KPI SP generation in configuration mode
            services.AddScoped<IKPIStoredProcedureHandler, KPIStoredProcedureHandler>();
            //in visulisation mode
            services.AddScoped<IKPIStoredProcedureExecutionerInVisualisation, KPIStoredProcedureHandler>();

            //Service for KPI Entities in configuration mode
            services.AddScoped<IKPIEntityConfigurationService, KPIEntityService>();
            //in visualisation mode
            services.AddScoped<IKPIEntityVisualisationService, KPIEntityService>();

            //Service for Graphical Entities in configuration mode
            services.AddScoped<IGraphicalEntityConfigurationService, GraphicalEntityService>();
            //in visualization mode
            services.AddScoped<IGraphicalEntityVisualisationService, GraphicalEntityService>();

            #endregion


            return services;
        }



    }
}
