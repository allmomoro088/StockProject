using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockProject.Data;
using StockProject.Data.Dao;
using StockProject.Data.Dtos;
using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Services.Handlers
{
	public class DefaultStockService : IStockService
	{
		IRequestService _requestService;
		IMapper _mapper;
		AppDbContext _context;
		IStockDao _stockDao;
		public DefaultStockService(IRequestService requestService, IMapper mapper, AppDbContext context, IStockDao stockDao)
		{
			_requestService = requestService;
			_mapper = mapper;
			_context = context;
			_stockDao = stockDao;
		}

		public ReadStockDto GetStock(string symbol)
		{
			if (symbol == null) throw new ArgumentNullException("symbol", "Symbol cannot be null");
			var stock = _context.Stocks.AsNoTracking().FirstOrDefault(x => x.Symbol == symbol);
			if (stock == null || stock.LastUpdate.Value.AddMinutes(5) < DateTime.Now)
			{
				var stockInfo = _requestService.GetStockInfo(symbol);
				ReadStockDto dto = _mapper.Map<ReadStockDto>(stockInfo);
				dto.LatestPrice = _requestService.GetStockLatestPrice(symbol);

				stock = _mapper.Map<Stock>(dto);

				_stockDao.UpdateDatabase(stock);

				return dto;
			}
			return _mapper.Map<ReadStockDto>(stock);
		}
	}
}
