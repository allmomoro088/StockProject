using StockProject.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Services
{
	public interface IStockService
	{
		ReadStockDto GetStock(string symbol);
	}
}
