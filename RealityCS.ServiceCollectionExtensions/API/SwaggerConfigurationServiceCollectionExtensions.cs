using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RealityCS.ServiceCollectionExtensions.API
{
    public static class SwaggerConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IWebHostEnvironment env)
        {
            #region Register the Swagger generator, defining 1 or more Swagger documents
            //https://github.com/Redocly/redoc/issues/692
            services.AddSwaggerGen(c =>
            {
               
                //FileInfo fi =new FileInfo();

                string apidoc = File.ReadAllText(env.ContentRootPath + "/Docs/api-documentation.txt");


                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RealityCS Api",
                    Version = "",
                    Description = apidoc
                });

                

                c.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });
                
                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}");
                c.DocumentFilter<XLogoDocumentFilter>();
                c.DocumentFilter<XCodeSamplesFilter>();



                c.ExampleFilters();
                c.EnableAnnotations();
                c.OperationFilter<CustomOperationNameFilter>();
                c.OperationFilter<AddResponseHeadersFilter>();
                c.IgnoreObsoleteActions();
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerExampleConfiguration();

            #endregion

            return services;
        }

        public class XLogoDocumentFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                // need to check if extension already exists, otherwise swagger 
                // tries to re-add it and results in error  

                if (!swaggerDoc.Info.Extensions.ContainsKey("x-logo"))
                    swaggerDoc.Info.Extensions.Add("x-logo", new OpenApiObject
            {
                {"url", new OpenApiString("http://122.176.31.190/realitycs-apollo/images/logo.jpg")},
                {"backgroundColor", new OpenApiString("#FFFFFF")},
                {"altText", new OpenApiString("Realitycs Logo")}
            });


            }

        }

        public class XCodeSamplesFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                string source = @"PetStore.v1.Pet pet = new PetStore.v1.Pet();
                                pet.setApiKey(""your api key"");
                                pet.petType = PetStore.v1.Pet.TYPE_DOG;
                                pet.name = ""Rex"";
                                // set other fields
                                PetStoreResponse response = pet.create();
                                if (response.statusCode == HttpStatusCode.Created)
                                {
                                  // Successfully created
                                }
                                else
                                {
                                  // Something wrong -- check response for errors
                                  Console.WriteLine(response.getRawResponse());
                                }";
                // need to check if extension already exists, otherwise swagger 
                // tries to re-add it and results in error  

                if (!swaggerDoc.Info.Extensions.ContainsKey("x-codeSamples"))
                    swaggerDoc.Info.Extensions.Add("x-codeSamples", new OpenApiObject
                    {
                        {"lang", new OpenApiString("C#")},
                        {"label", new OpenApiString("dot net")},
                        {"source", new OpenApiString(source)},
                    });

            }


        }

        public class CustomOperationNameFilter : IOperationFilter
        {
          
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                
                //operation.OperationId = $"{context.ApiDescription.ActionDescriptor.RouteValues["controller"]}_{context.ApiDescription.ActionDescriptor.RouteValues["action"]}";
                operation.OperationId = $"{operation.Tags[0].Name}_{operation.OperationId}";
            }
        }
    }

    public static class SwaggerExamplesConfigurationServiceCollectionExtensions
    {

        public static IServiceCollection AddSwaggerExampleConfiguration(this IServiceCollection services)
        {
            var assembly = Assembly.Load("RealityCS.DTO");
            services.AddSwaggerExamplesFromAssemblies(assembly);
            return services;
        }
    }
}
