using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealityCS.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.ServiceCollectionExtensions.API
{
    public static class IdentityConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, tbl_AUTH_AspNet_Roles>(option =>
            {

                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;


            })
            .AddEntityFrameworkStores<AspNetIdentityDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
