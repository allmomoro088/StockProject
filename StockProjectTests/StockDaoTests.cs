using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
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
	public class StockDaoTests
	{
		IConfiguration _config;
		IStockDao _stockDao;
		AppDbContext _context;
		public StockDaoTests()
		{
			_config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();


			var services = new ServiceCollection();
			services.AddDbContext<AppDbContext>(opts => opts.UseMySQL(_config.GetConnectionString("stocksdb")));
			services.AddScoped<IStockDao, DefaultStockDao>();

			var provider = services.BuildServiceProvider();
			_context = provider.GetService<AppDbContext>();
			_stockDao = provider.GetService<IStockDao>();
		}

		[Fact]
		public void UpdateDatabaseCorrect()
		{
			//Arrange
			Stock existing = _context.Stocks.AsNoTracking().FirstOrDefault(x => x.Symbol == "aapl");
			Stock stock = new Stock
			{
				Symbol = "aapl",
				CompanyName = "Apple Inc.",
				Country = "US",
				Description = "Apple's description",
				LastUpdate = DateTime.Now,
				LatestPrice = 117.3,
				WebSite = "apple.com"
			};

			//Act
			_stockDao.UpdateDatabase(stock);
			var forAssert = _context.Stocks.AsNoTracking().FirstOrDefault(x => x.Symbol == "aapl");

			//Assert
			Assert.NotNull(forAssert);

			//Cleanup
			if (existing != null)
			{
				_context.Update(existing);
			}
			else
			{
				_context.Remove(stock);
			}
		}
	}
}
