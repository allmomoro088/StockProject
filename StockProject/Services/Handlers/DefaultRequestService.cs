using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using StockProject.Exceptions;
using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Services.Handlers
{
	public class DefaultRequestService : IRequestService
	{
		RestClient _client;
		APISettings _settings;

		public DefaultRequestService(RestClient client, APISettings settings)
		{
			_client = client;
			_settings = settings;
		}

		public StockInfo GetStockInfo(string symbol)
		{
			var request = new RestRequest($"/stock/{symbol}/company", Method.GET);
			request.AddParameter("token", _settings.SecretToken);

			IRestResponse response = Execute(request);
			return JsonConvert.DeserializeObject<StockInfo>(response.Content, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });
		}

		public double GetStockLatestPrice(string symbol)
		{
			var request = new RestRequest($"/stock/{symbol}/quote", Method.GET);
			request.AddParameter("token", _settings.SecretToken);

			IRestResponse response = Execute(request);
			return (double)JObject.Parse(response.Content).SelectToken("latestPrice");
		}

		private IRestResponse Execute(RestRequest request)
		{
			IRestResponse response = _client.Execute(request);
			if (response.StatusCode == System.Net.HttpStatusCode.NotFound) throw new UnknownSymbolException();

			return response;
		}
	}
}
