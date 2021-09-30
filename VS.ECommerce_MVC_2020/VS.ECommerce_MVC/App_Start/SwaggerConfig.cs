using System.Web.Http;
using WebActivatorEx;
using OAuthTokenBasedRestService;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Collections.Generic;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace OAuthTokenBasedRestService
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Shop ECommerce");
                    c.OperationFilter<AddRequiredAuthorizationHeaderParameter>();

                    //          c.ApiKey("Token")
                    //.Description("Filling bearer token here")
                    //.Name("Authorization")
                    //.In("header");

                    c.IncludeXmlComments(GetXmlCommentsPath(thisAssembly.GetName().Name));
                    c.IncludeXmlComments(GetXmlCommentsPath("VS.ECommerce_MVC"));

                })
                .EnableSwaggerUi(c =>
                {
                    c.EnableApiKeySupport("apiKey", "header");
                    // c.EnableApiKeySupport("Authorization", "header");
                });
        }
        protected static string GetXmlCommentsPath(string name)
        {
            #region Hướng dẫn làm ghi chú trong API
            //1: https://www.youtube.com/watch?v=tyesYzBnS4A //Implementing & Customizing Swagger UI in ASP.NET Web APIs using Swashbuckle
            //2: https://bitoftech.net/2014/08/25/asp-net-web-api-documentation-using-swagger/  
            //3: https://developers.de/blogs/damir_dobric/archive/2015/04/10/how-to-activate-swagger-in-your-api-app.aspx
            #endregion
            return string.Format(@"{0}\bin\{1}.XML", AppDomain.CurrentDomain.BaseDirectory, name);
        }
    }

    public class AddRequiredAuthorizationHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                //name = "Authorization",
                //@in = "header",
                //type = "string",
                //required = true,
                //description = "access token"

                name = "Authorization",
                @in = "header",
                type = "string",
                required = false,
                @default = "Bearer"
            });
        }
    }

}