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
using System.IO;
using Xunit;

namespace StockProjectTests
{
	public class StockServiceTests
	{
		IConfiguration _config;
		IStockService _stockService;
		IMapper _mapper;
		IRequestService _requestService;
		IStockDao _stockDao;
		public StockServiceTests()
		{
			_config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();


			var services = new ServiceCollection();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddDbContext<AppDbContext>(opts => opts.UseMySQL(_config.GetConnectionString("stocksdb")));

			services.AddScoped<IStockService, DefaultStockService>();
			services.AddScoped<IRequestService, DefaultRequestService>();
			services.AddScoped<IStockDao, DefaultStockDao>();

			var clientBaseUrl = _config.GetValue<string>("API:BaseUrl");
			var secretToken = _config.GetValue<string>("API:SecretToken");

			services.AddSingleton(new APISettings(secretToken));
			services.AddSingleton(new RestClient(clientBaseUrl));

			var provider = services.BuildServiceProvider();

			_mapper = provider.GetService<IMapper>();
			_requestService = provider.GetService<IRequestService>();
			_stockDao = provider.GetService<IStockDao>();
			_stockService = provider.GetService<IStockService>();
		}

		[Theory]
		[InlineData("aapl")]
		[InlineData("twtr")]
		[InlineData("fb")]
		public void GetStockCorrect(string symbol)
		{
			//Act
			var result = _stockService.GetStock(symbol);

			//Assert
			Assert.NotNull(result);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("somethingthatdoesnotexist")]
		[InlineData("2")]
		[InlineData("-1")]
		public void GetStockNonExistentSymbol(string symbol)
		{
			//Assert
			Assert.Throws<UnknownSymbolException>(() => _stockService.GetStock(symbol));
		}

		[Fact]
		public void GetStockNull()
		{
			//Arrange
			string symbol = null;

			//Assert
			Assert.Throws<ArgumentNullException>(() => _stockService.GetStock(symbol));
		}
	}
}
