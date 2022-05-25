using Microsoft.AspNetCore.Mvc;
using StockProject.Exceptions;
using StockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Controllers
{
	public class StocksController : Controller
	{
		IStockService _stockService;

		public StocksController(IStockService stockService)
		{
			_stockService = stockService;
		}

		public IActionResult Search()
		{
			return View();
		}

		public IActionResult Result(string symbol = null)
		{
			try
			{
				var dto = _stockService.GetStock(symbol);
				return View(dto);
			}
			catch (UnknownSymbolException)
			{
				return View(null);
			}
			catch (ArgumentNullException)
			{
				return View(null);
			}
		}
	}
}
