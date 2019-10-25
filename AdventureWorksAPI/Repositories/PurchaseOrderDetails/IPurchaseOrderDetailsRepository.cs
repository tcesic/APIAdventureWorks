using AdventureWorksAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Repositories.PurchaseOrderDetails
{
	public interface IPurchaseOrderDetailsRepository
	{
		int GetTotal();

		IEnumerable<PurchaseOrderDetailDTO> Get(DateTime? startTime, DateTime? endTime);

		IEnumerable<PurchaseOrderDetailDTO> GetPaged(DateTime? startTime, DateTime? endTime, int skip, int pageSize);
	}
}
