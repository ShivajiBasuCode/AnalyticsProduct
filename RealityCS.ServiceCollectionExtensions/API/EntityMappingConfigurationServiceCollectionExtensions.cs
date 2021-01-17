using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
//using RealityCS.DTO.Admin.EntityMapping;
using RealityCS.DTO.RealitycsClient.EntityMapping;

namespace RealityCS.ServiceCollectionExtensions.API
{
    public static class EntityMappingConfigurationServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityMappingConfiguration(this IServiceCollection services)
        {
            #region  Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
               
 //               mc.AddProfile(new NrtoaDashboardEntityMapping());
                mc.AddProfile(new RealitycsClientEntityMapping());

            });
            
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion


            return services;
        }
    }
}
