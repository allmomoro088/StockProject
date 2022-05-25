using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Models
{
	public class StockQuote
	{
		public double LatestPrice { get; set; }
		public double ChangePercent { get; set; }
		public double IEXOpen { get; set; }
	}
}
