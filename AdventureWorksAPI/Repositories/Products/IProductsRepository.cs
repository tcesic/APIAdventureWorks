using AdventureWorksAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Repositories.Products
{
	public interface IProductsRepository
	{
		int GetTotal();

		IEnumerable<ProductDTO> GetProducts(string name, DateTime? startTime, List<string> description);

		IEnumerable<ProductDTO> GetProductsPaged(string name, DateTime? startTime, List<string> keywords, int skip, int pageSize);
	}
}
