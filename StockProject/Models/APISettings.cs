using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Models
{
	public class APISettings
	{
		public string SecretToken { get; set; }

		public APISettings(string secretToken)
		{
			SecretToken = secretToken;
		}
	}
}
