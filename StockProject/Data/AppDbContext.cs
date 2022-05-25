using Microsoft.EntityFrameworkCore;
using StockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions opts) : base (opts)
		{

		}
		public DbSet<Stock> Stocks { get; set; }
	}
}
