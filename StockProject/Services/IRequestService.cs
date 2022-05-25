using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Services
{
	public interface IRequestService
	{
		StockQuote GetStockQuote(string symbol);
		StockInfo GetStockInfo(string symbol);
	}
}
