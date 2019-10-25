using System.Web.Http;
using WebActivatorEx;
using AdventureWorksAPI;
using Swashbuckle.Application;
using System;
using System.Xml.XPath;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace AdventureWorksAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
			GlobalConfiguration.Configuration
				.EnableSwagger(c =>
				{
					c.SingleApiVersion("v1", "AdventureWorksAPI");
					c.DescribeAllEnumsAsStrings();
					c.PrettyPrint();
				})
				.EnableSwaggerUi(c => c.DisableValidator());
		}
	}
}
