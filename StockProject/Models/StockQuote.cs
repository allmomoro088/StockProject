using Newtonsoft.Json.Converters;
using StockProject.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockProject.Models
{
	public class StockQuote
	{
		[JsonConverter(typeof(MicrosecondEpochConverter))]
		public DateTime LatestUpdate { get; set; }
		public double LatestPrice { get; set; }
		public double ChangePercent { get; set; }
		public double IEXOpen { get; set; }
	}
}
