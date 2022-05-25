using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Data.Dao
{
	public interface IStockDao
	{
		void UpdateDatabase(Stock stock);
	}
}
