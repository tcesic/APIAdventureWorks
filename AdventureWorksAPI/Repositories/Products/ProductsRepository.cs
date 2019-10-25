using AdventureWorksAPI.AutoMapper;
using AdventureWorksAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksAPI.Repositories.Products
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly IMyMapper _myMapper;

		public ProductsRepository(IMyMapper myMapper)
		{
			_myMapper = myMapper;
		}

		public int GetTotal()
		{
			using (var db = new ModelAdventureWorks())
			{
				return db.Products.Count();
			}
		}


		public IEnumerable<ProductDTO> GetProducts(string name, DateTime? startTime, List<string> keywords)
		{
			using (var db = new ModelAdventureWorks())
			{
				var filteredProducts = db.Products.AsNoTracking() as IQueryable<Product>;

				if (!string.IsNullOrEmpty(name))
					filteredProducts = filteredProducts.Where(i => i.Name == name);

				if (startTime.HasValue)
					filteredProducts = filteredProducts.Where(i => i.SellStartDate == startTime);

				if (keywords != null && keywords.Count > 0)
					filteredProducts = filteredProducts.Where(i => i.ProductModel.ProductModelProductDescriptionCultures.Where(pd => keywords.Any(kw => pd.ProductDescription.Description.Contains(kw))).Count() > 0);

				return filteredProducts.AsEnumerable().Select(product => _myMapper.GetMapper().Map<Product,ProductDTO>(product)).ToList();

			}
		}

		public IEnumerable<ProductDTO> GetProductsPaged(string name, DateTime? startTime, List<string> keywords, int skip, int pageSize)
		{
			using (var db = new ModelAdventureWorks())
			{
				var filteredProducts = db.Products.AsNoTracking() as IQueryable<Product>;

				if (!string.IsNullOrEmpty(name))
					filteredProducts = filteredProducts.Where(i => i.Name == name);

				if (startTime.HasValue)
					filteredProducts = filteredProducts.Where(i => i.SellStartDate == startTime);

				if (keywords != null && keywords.Count > 0)
					filteredProducts = filteredProducts.Where(i => i.ProductModel.ProductModelProductDescriptionCultures.Where(pd => keywords.Any(kw => pd.ProductDescription.Description.Contains(kw))).Count() > 0);

				filteredProducts = filteredProducts.OrderBy(p=>p.ProductID).Skip(skip).Take(pageSize);

				return filteredProducts.AsEnumerable().Select(product => _myMapper.GetMapper().Map<Product, ProductDTO>(product)).ToList();

			}
		}
	}
}