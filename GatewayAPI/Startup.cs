using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using AutoMapper;
using BusAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GatewayAPI.BusesClient;
using GatewayAPI.PlanesClient;
using GatewayAPI.FavoritesClient;
namespace GatewayAPI
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
            services.AddControllers();
            services.AddHttpClient<IBusesHttpClient, BusesHttpClient>(client =>
           {
               client.BaseAddress = new Uri("https://localhost:44331/");
           });

            services.AddHttpClient<IPlanesHttpClient, PlanesHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44361/");
            });

            services.AddHttpClient<IFavoritesHttpClient, FavoritesHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44357/");
            });
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
