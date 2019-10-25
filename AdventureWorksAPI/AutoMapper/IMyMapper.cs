using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksAPI.AutoMapper
{
	public interface IMyMapper
	{
		IMapper GetMapper();
	}
}
