using AdventureWorksAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksAPI.Repositories.PurchaseOrderDetails
{
	public class PurchaseOrderDetailsRepository : IPurchaseOrderDetailsRepository
	{
		public IEnumerable<PurchaseOrderDetailDTO> Get(DateTime? startTime, DateTime? endTime)
		{
			using (var db = new ModelAdventureWorks())
			{
				var filteredPurchaseOrderDetailes = db.PurchaseOrderDetails.AsNoTracking() as IQueryable<PurchaseOrderDetail>;

				if (startTime.HasValue)
					filteredPurchaseOrderDetailes = filteredPurchaseOrderDetailes.Where(i => i.DueDate >= startTime.Value);

				if (endTime.HasValue)
					filteredPurchaseOrderDetailes = filteredPurchaseOrderDetailes.Where(i => i.DueDate <= endTime.Value);

				return filteredPurchaseOrderDetailes.GroupBy(i => i.DueDate).Select(t => new PurchaseOrderDetailDTO
				{
					TrafficSum = t.Sum(k => k.LineTotal),
					NumberOfProductUnitsSold = t.FirstOrDefault().OrderQty,
					DueDate = t.FirstOrDefault().DueDate
				}).ToList();
			}
		}

		public IEnumerable<PurchaseOrderDetailDTO> GetPaged(DateTime? startTime, DateTime? endTime, int skip, int pageSize)
		{
			using (var db = new ModelAdventureWorks())
			{
				var filteredPurchaseOrderDetailes = db.PurchaseOrderDetails.AsNoTracking() as IQueryable<PurchaseOrderDetail>;

				if (startTime.HasValue)
					filteredPurchaseOrderDetailes = filteredPurchaseOrderDetailes.Where(i => i.DueDate >= startTime.Value);

				if (endTime.HasValue)
					filteredPurchaseOrderDetailes = filteredPurchaseOrderDetailes.Where(i => i.DueDate <= endTime.Value);

				return filteredPurchaseOrderDetailes.GroupBy(i => i.DueDate).Select(t => new PurchaseOrderDetailDTO
				{
					TrafficSum = t.Sum(k => k.LineTotal),
					NumberOfProductUnitsSold = t.FirstOrDefault().OrderQty,
					DueDate = t.FirstOrDefault().DueDate
				}).OrderBy(k=>k.DueDate).Skip(skip).Take(pageSize).ToList();
			}
		}

		public int GetTotal()
		{
			using (var db = new ModelAdventureWorks())
			{
				return db.PurchaseOrderDetails.Count();
			}
		}
	}
}