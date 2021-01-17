using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealityCS.BusinessLogic.GraphicalEntity;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.BaseContext;
using RealityCS.DataLayer.Context.GraphicalEntity;
using RealityCS.DataLayer.Context.KPIEntity;
using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsEnumeration;
using RealityCS.DataLayer.Context.RealitycsShared;
using RealityCS.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.ServiceCollectionExtensions.API
{
    public static class DbContextConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContextPool<RealitycsBaseContext>(option =>
            //{
            //    var dataSettings = DataSettingsManager.LoadSettings();

            //    if (!dataSettings?.IsValid ?? true)
            //        return;

            //    option.UseSqlServer(dataSettings.ConnectionString)
            //    .UseLazyLoadingProxies()
            //    .EnableDetailedErrors()
            //    .EnableSensitiveDataLogging();
            //});

            services.AddDbContext<AspNetIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"))
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            });

            services.AddDbContext<RealitycsClientContext>(option =>
            {
                var dataSettings = DataSettingsManager.LoadSettings();

                if (!dataSettings?.IsValid ?? true)
                    return;

                option.UseSqlServer(dataSettings.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            });
            services.AddDbContext<RealitycsEnumerationContext>(option =>
            {
                var dataSettings = DataSettingsManager.LoadSettings();

                if (!dataSettings?.IsValid ?? true)
                    return;

                option.UseSqlServer(dataSettings.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            });
            services.AddDbContext<RealitycsSharedContext>(option =>
            {
                var dataSettings = DataSettingsManager.LoadSettings();

                if (!dataSettings?.IsValid ?? true)
                    return;

                option.UseSqlServer(dataSettings.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            });
            //Added RealitycsKPIContext
            services.AddDbContext<RealitycsKPIContext>(option =>
            {
                var dataSettings = DataSettingsManager.LoadSettings();

                if (!dataSettings?.IsValid ?? true)
                    return;

                option.UseSqlServer(dataSettings.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            });
            //Added RealitycsGraphicalContext
            services.AddDbContext<RealitycsGraphicalContext>(option =>
            {
                var dataSettings = DataSettingsManager.LoadSettings();

                if (!dataSettings?.IsValid ?? true)
                    return;

                option.UseSqlServer(dataSettings.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
            });

            /* Removed all nrto references - Shivaji
                        services.AddDbContext<NrtoaDashboardContext>(option =>
                        {
                            option.UseSqlServer(configuration.GetConnectionString("NrtoaDashboardConnectionString"))
                            //.UseLazyLoadingProxies()
                            .EnableDetailedErrors()
                            .EnableSensitiveDataLogging();
                        });

                        services.AddDbContext<NrtoaParserContext>(option =>
                        {
                            option.UseSqlServer(configuration.GetConnectionString("NrtoaParserConnectionString"))
                            //.UseLazyLoadingProxies()
                            .EnableDetailedErrors()
                            .EnableSensitiveDataLogging();
                        });
            */
            return services;
        }
    }
}
