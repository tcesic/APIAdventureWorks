
using AdventureWorksAPI.AutoMapper;
using AdventureWorksAPI.Repositories.Products;
using AdventureWorksAPI.Repositories.PurchaseOrderDetails;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AdventureWorksAPI
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			RegisterAutofac();		
		}

		private void RegisterAutofac()
		{
			var builder = new ContainerBuilder();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<ProductsRepository>().As<IProductsRepository>();
			builder.RegisterType<PurchaseOrderDetailsRepository>().As<IPurchaseOrderDetailsRepository>();
			builder.RegisterType<MyMapper>().As<IMyMapper>();

			var config = GlobalConfiguration.Configuration;
			var container = builder.Build();
			
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}
