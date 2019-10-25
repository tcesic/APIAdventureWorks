using AdventureWorksAPI.Controllers.Paging;
using AdventureWorksAPI.DTOModels;
using AdventureWorksAPI.Repositories.Products;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdventureWorksAPI.Controllers
{
	[RoutePrefix("api/productions")]
	public class ProductsController : ApiController
	{
		private readonly IProductsRepository _productionsRepository;

		public ProductsController(IProductsRepository productionsRepository)
		{
			_productionsRepository = productionsRepository ?? throw new ArgumentNullException(nameof(productionsRepository));
		}

		[HttpGet]
		[Route("")]
		[SwaggerResponse(HttpStatusCode.OK, "Searched data", typeof(List<ProductDTO>))]
		[SwaggerResponse(HttpStatusCode.NotFound, "No data you are looking for")]
		public IHttpActionResult Get([FromUri]List<string> keywords = null, string productName = null, DateTime? startTime = null)
		{
			var products = _productionsRepository.GetProducts(productName, startTime, keywords);

			return products!=null && products.Count()> 0 ? Ok(products) : (IHttpActionResult)NotFound();
		}


		[HttpGet]
		[Route("paged")]
		[SwaggerResponse(HttpStatusCode.OK, "Searched data", typeof(PagedResult<ProductDTO>))]
		[SwaggerResponse(HttpStatusCode.NotFound, "No data you are looking for")]
		public IHttpActionResult GetPaged([FromUri]List<string> keywords = null, string productName = null, DateTime? startTime = null, int pageNo = 1, int pageSize = 10)
		{
			int skip = (pageNo - 1) * pageSize;

			int total = _productionsRepository.GetTotal();

			var products = _productionsRepository.GetProductsPaged(productName, startTime, keywords, skip, pageSize);

			return products!=null && products.Count()>0 ? Ok(new PagedResult<ProductDTO>(products, pageNo, pageSize, total)) : (IHttpActionResult)NotFound();
		}
	}
}
