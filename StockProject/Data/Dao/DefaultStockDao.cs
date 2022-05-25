using Microsoft.EntityFrameworkCore;
using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Data.Dao
{
	public class DefaultStockDao : IStockDao
	{
		AppDbContext _context;

		public DefaultStockDao(AppDbContext context)
		{
			_context = context;
		}

		public void UpdateDatabase(Stock stock)
		{
			stock.LastUpdate = DateTime.Now;
			if (_context.Stocks.AsNoTracking().FirstOrDefault(x => x.Symbol == stock.Symbol) == null)
			{
				_context.Add(stock);
			}
			else
			{
				_context.Update(stock);
			}
			_context.SaveChanges();
		}
	}
}
