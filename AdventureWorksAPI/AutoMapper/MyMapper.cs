using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdventureWorksAPI.DTOModels;
using AutoMapper;

namespace AdventureWorksAPI.AutoMapper
{
	public class MyMapper : IMyMapper
	{
		public IMapper GetMapper()
		{
			var config = new MapperConfiguration(cfg => {
				cfg.CreateMap<Product, ProductDTO>();
			});
			return config.CreateMapper();
		}
	}
}