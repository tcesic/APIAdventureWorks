using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureWorksAPI;
using AdventureWorksAPI.Controllers;
using AdventureWorksAPI.Repositories.Products;
using Moq;
using System.Web.Http;
using System.Collections.Generic;
using System;
using System.Web.Http.Results;
using AdventureWorksAPI.DTOModels;
using System.Linq;

namespace AdventureWorksAPI.Tests.Controllers
{
	[TestClass]
	public class ProductControllerTest
	{
		[TestMethod]
		public void Get()
		{
			var mockedList = new List<ProductDTO>();
			mockedList.Add(			
				new ProductDTO
				{
					ProductID = 1,
					Name = "Adjustable Race",
					ProductNumber = "AR-5381",
					MakeFlag = false,
					FinishedGoodsFlag = false,
					SafetyStockLevel = 1000,
					ReorderPoint = 750,
					StandardCost = 0,
					ListPrice = 0,
					DaysToManufacture = 0,
					SellStartDate = DateTime.Now,
					rowguid = Guid.NewGuid(),
					ModifiedDate = DateTime.Now
				});

			var mockRepository = new Mock<IProductsRepository>();
			mockRepository.Setup(x => x.GetProducts(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<List<string>>()))
				.Returns(mockedList);

			var controller = new ProductsController(mockRepository.Object);

			IHttpActionResult actionResult = controller.Get(null, "", new DateTime());
			var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<ProductDTO>>;

			Assert.IsNotNull(contentResult);
			Assert.IsNotNull(contentResult.Content);
			Assert.AreEqual(mockedList.Count, contentResult.Content.Count());
			Assert.AreEqual(mockedList[0], contentResult.Content.ElementAt(0));
		}

		[TestMethod]
		public void GetReturnsNotFound()
		{
			var mockRepository = new Mock<IProductsRepository>();
			var controller = new ProductsController(mockRepository.Object);

			var keywords = new List<string>();
			keywords.Add("xyz");
			IHttpActionResult actionResult = controller.Get(keywords, "xx", DateTime.Now);

			Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
		}

		//the similar tests for paged scenario 
	}
}
