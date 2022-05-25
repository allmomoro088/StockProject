using AutoMapper;
using StockProject.Data.Dtos;
using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Profiles
{
	public class StockProfile : Profile
	{
		public StockProfile()
		{
			CreateMap<StockInfo, ReadStockDto>();
			CreateMap<Stock, ReadStockDto>();
			CreateMap<ReadStockDto, Stock>();
		}
	}
}
