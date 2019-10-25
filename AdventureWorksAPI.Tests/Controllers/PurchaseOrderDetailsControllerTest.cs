using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureWorksAPI;
using AdventureWorksAPI.Controllers;
using Rhino.Mocks;
using AdventureWorksAPI.Repositories.PurchaseOrderDetails;
using AdventureWorksAPI.DTOModels;
using Moq;
using System.Web.Http.Results;

namespace AdventureWorksAPI.Tests.Controllers
{
	[TestClass]
	public class PurchaseOrderDetailsControllerTest
	{
		[TestMethod]
		public void Get()
		{
			var mockedList = new List<PurchaseOrderDetailDTO>();
			mockedList.Add(new PurchaseOrderDetailDTO
			{
				DueDate = DateTime.Now,
				NumberOfProductUnitsSold = 54000,
				TrafficSum = 25000
			});

			var mockRepository = new Mock<IPurchaseOrderDetailsRepository>();
			mockRepository.Setup(x => x.Get(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
				.Returns(mockedList);

			var controller = new PurchaseOrderDetailsController(mockRepository.Object);

			IHttpActionResult actionResult = controller.Get(DateTime.Now,DateTime.Now);
			var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<PurchaseOrderDetailDTO>>;

			Assert.IsNotNull(contentResult);
			Assert.IsNotNull(contentResult.Content);
			Assert.AreEqual(mockedList.Count, contentResult.Content.Count());
			Assert.AreEqual(mockedList[0], contentResult.Content.ElementAt(0));
		}

		[TestMethod]
		public void GetReturnsNotFound()
		{
			var mockRepository = new Mock<IPurchaseOrderDetailsRepository>();
			var controller = new PurchaseOrderDetailsController(mockRepository.Object);
			
			IHttpActionResult actionResult = controller.Get(DateTime.Now, DateTime.Now);
			
			Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
		}

		//the similar tests for paged scenario 
	}
}
