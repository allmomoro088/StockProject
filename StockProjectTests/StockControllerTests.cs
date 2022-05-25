using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StockProject.Controllers;
using StockProject.Data;
using StockProject.Data.Dao;
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
	public class StockControllerTests
	{
		IConfiguration _config;
		IStockService _stockService;
		IMapper _mapper;
		IRequestService _requestService;
		IStockDao _stockDao;
		StocksController _controller;
		public StockControllerTests()
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

			_controller = new StocksController(_stockService);
		}

		[Theory]
		[InlineData("aapl")]
		[InlineData("twtr")]
		[InlineData("fb")]
		public void StocksController_ResultCorrect(string symbol)
		{
			//Act
			var result = _controller.Result(symbol);

			//Assert
			Assert.IsType<ViewResult>(result);
			Assert.NotNull(((ViewResult)result).Model);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("somethingthatdoesnotexist")]
		[InlineData("2")]
		[InlineData("-1")]
		public void StocksController_ResultNonExistingSymbol(string symbol)
		{
			//Act
			var result = _controller.Result(symbol);

			//Assert
			Assert.IsType<ViewResult>(result);
			Assert.Null(((ViewResult)result).Model);
		}
	}
}
