using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksAPI.DTOModels
{
	public class PurchaseOrderDetailDTO
	{
		public DateTime DueDate { get; set; }

		public decimal TrafficSum { get; set; }

		public int NumberOfProductUnitsSold { get; set; }

		public decimal TotalSum { get
			{
				return TrafficSum + NumberOfProductUnitsSold;
			}
		}
	}
}