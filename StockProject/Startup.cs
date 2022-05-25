using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using StockProject.Data;
using StockProject.Data.Dao;
using StockProject.Models;
using StockProject.Services;
using StockProject.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(opts => opts.UseMySQL(Configuration.GetConnectionString("stocksdb")));
			services.AddControllersWithViews();
			services.AddControllers();

			services.AddScoped<IStockService, DefaultStockService>();
			services.AddScoped<IRequestService, DefaultRequestService>();
			services.AddScoped<IStockDao, DefaultStockDao>();

			var clientBaseUrl = Configuration.GetValue<string>("API:BaseUrl");
			var secretToken = Configuration.GetValue<string>("API:SecretToken");

			services.AddSingleton(new APISettings(secretToken));
			services.AddSingleton(new RestClient(clientBaseUrl));

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Stocks}/{action=Search}/{id?}");
				endpoints.MapControllers();
			});
		}
	}
}
