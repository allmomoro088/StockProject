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

		public StockQuote GetStockQuote(string symbol)
		{
			var request = new RestRequest($"/stock/{symbol}/quote", Method.GET);
			request.AddParameter("token", _settings.SecretToken);

			IRestResponse response = Execute(request);
			return SerializeQuote(response.Content);
		}

		private IRestResponse Execute(RestRequest request)
		{
			IRestResponse response = _client.Execute(request);
			if (response.StatusCode == System.Net.HttpStatusCode.NotFound) throw new UnknownSymbolException();

			return response;
		}

		private StockQuote SerializeQuote(string json)
		{
			JObject jQuote = JObject.Parse(json);
			StockQuote quote = new StockQuote
			{
				ChangePercent = (double)jQuote.SelectToken("changePercent"),
				IEXOpen = (double)jQuote.SelectToken("iexOpen"),
				LatestPrice = (double)jQuote.SelectToken("latestPrice"),
				LatestUpdate = (new DateTime(1970,1,1) + TimeSpan.FromMilliseconds((double)jQuote.SelectToken("latestUpdate"))).ToLocalTime()
			};
			return quote;
		}
	}
}
