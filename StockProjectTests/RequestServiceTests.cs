using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StockProject.Data;
using StockProject.Data.Dao;
using StockProject.Exceptions;
using StockProject.Models;
using StockProject.Services;
using StockProject.Services.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockProjectTests
{
	public class RequestServiceTests
	{
		IConfiguration _config;
		IRequestService _requestService;
		public RequestServiceTests()
		{
			_config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();


			var services = new ServiceCollection();

			services.AddScoped<IRequestService, DefaultRequestService>();

			var clientBaseUrl = _config.GetValue<string>("API:BaseUrl");
			var secretToken = _config.GetValue<string>("API:SecretToken");

			services.AddSingleton(new APISettings(secretToken));
			services.AddSingleton(new RestClient(clientBaseUrl));

			var provider = services.BuildServiceProvider();

			_requestService = provider.GetService<IRequestService>();
		}

		[Theory]
		[InlineData("aapl")]
		[InlineData("twtr")]
		[InlineData("fb")]
		public void GetStockInfoCorrect(string symbol)
		{
			//Act
			var info = _requestService.GetStockInfo(symbol);

			//Assert
			Assert.NotNull(info);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("somethingthatdoesnotexist")]
		[InlineData("2")]
		[InlineData("-1")]
		public void GetStockNonExistentSymbol(string symbol)
		{
			//Assert
			Assert.Throws<UnknownSymbolException>(() => _requestService.GetStockInfo(symbol));
		}

		[Theory]
		[InlineData("aapl")]
		[InlineData("twtr")]
		[InlineData("fb")]
		public void GetStockLatestPriceCorrect(string symbol)
		{
			//Act
			var quote = _requestService.GetStockQuote(symbol);

			//Assert
			Assert.True(quote.LatestPrice > 0.0);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("somethingthatdoesnotexist")]
		[InlineData("2")]
		[InlineData("-1")]
		public void GetStockNonExistentSymbolLatestPrice(string symbol)
		{
			//Assert
			Assert.Throws<UnknownSymbolException>(() => _requestService.GetStockQuote(symbol));
		}
	}
}
