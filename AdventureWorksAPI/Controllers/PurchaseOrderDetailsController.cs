using AdventureWorksAPI.Controllers.Paging;
using AdventureWorksAPI.DTOModels;
using AdventureWorksAPI.Repositories.PurchaseOrderDetails;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace AdventureWorksAPI.Controllers
{
	[RoutePrefix("api/purchase-order-details")]
	public class PurchaseOrderDetailsController : ApiController
	{
		private readonly IPurchaseOrderDetailsRepository _purchaseOrderDetailsRepository;

		public PurchaseOrderDetailsController(IPurchaseOrderDetailsRepository purchaseOrderDetailsRepository)
		{
			_purchaseOrderDetailsRepository = purchaseOrderDetailsRepository ?? throw new ArgumentNullException(nameof(purchaseOrderDetailsRepository));
		}

		[HttpGet]
		[Route("")]
		[SwaggerResponse(HttpStatusCode.OK, "Searched data", typeof(List<PurchaseOrderDetailDTO>))]
		[SwaggerResponse(HttpStatusCode.NotFound, "No data you are looking for")]
		public IHttpActionResult Get(DateTime? startTime = null, DateTime? endTime = null)
		{
			var purchaseOrderDetails = _purchaseOrderDetailsRepository.Get(startTime, endTime);
			return (purchaseOrderDetails !=null && purchaseOrderDetails.Count()> 0) ? Ok(purchaseOrderDetails) : (IHttpActionResult)NotFound();
		}

		[HttpGet]
		[Route("paged")]
		[SwaggerResponse(HttpStatusCode.OK, "Searched data", typeof(PagedResult<PurchaseOrderDetailDTO>))]
		[SwaggerResponse(HttpStatusCode.NotFound, "No data you are looking for")]
		public IHttpActionResult GetPaged(DateTime? startTime = null, DateTime? endTime = null, int pageNo = 1, int pageSize = 10)
		{
			int skip = (pageNo - 1) * pageSize;

			int total = _purchaseOrderDetailsRepository.GetTotal();

			var purchaseOrderDetails = _purchaseOrderDetailsRepository.GetPaged(startTime, endTime, skip, pageSize);

			return purchaseOrderDetails!=null && purchaseOrderDetails.Count()>0 ? 
				   Ok(new PagedResult<PurchaseOrderDetailDTO>(purchaseOrderDetails, pageNo, pageSize, total)) :
				   (IHttpActionResult) NotFound();
		}

	}
}
