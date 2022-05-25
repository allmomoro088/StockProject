using Microsoft.AspNetCore.Mvc;
using StockProject.Data.Dtos;
using StockProject.Exceptions;
using StockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Controllers
{
	[ApiController]
	[Route("api/live")]
	public class StocksApiController : ControllerBase
	{
		IStockService _stockService;

		public StocksApiController(IStockService stockService)
		{
			_stockService = stockService;
		}

		[HttpGet("{symbol}")]
		public IActionResult GetLatestPrice(string symbol)
		{
			try
			{
				var dto = _stockService.GetStock(symbol);
				return Ok(dto.LatestPrice);
			}
			catch (UnknownSymbolException)
			{
				return NotFound(null);
			}
			catch (ArgumentNullException)
			{
				return NotFound(null);
			}
		}
	}
}
