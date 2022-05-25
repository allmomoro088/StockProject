using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Data.Dtos
{
	public class ReadStockDto
	{
		public double LatestPrice { get; set; }
		public string CompanyName { get; set; }
		public string Symbol { get; set; }
		public string Description { get; set; }
		public string WebSite { get; set; }
		public string Country { get; set; }
	}
}
