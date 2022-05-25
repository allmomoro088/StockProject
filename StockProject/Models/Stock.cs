using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Models
{
	public class Stock
	{
		[Key]
		[Required]
		public string Symbol { get; set; }
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string WebSite { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public double LatestPrice { get; set; }
		[Required]
		public DateTime? LastUpdate { get; set; }
		[Required]
		public double IEXOpen { get; set; }
		[Required]
		public double ChangePercent { get; set; }
	}
}
