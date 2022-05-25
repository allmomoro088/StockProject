using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Models
{
	public class StockInfo
	{
		public string CompanyName { get; set; }
		public string Symbol { get; set; }
		public string Description { get; set; }
		public string WebSite { get; set; }
		public string Country { get; set; }
		public double IEXOpen { get; set; }
		public double ChangePercent { get; set; }
	}
}
