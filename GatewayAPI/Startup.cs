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
using Polly;
using Hangfire;
using Polly.Fallback;
using Polly.CircuitBreaker;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using GatewayAPI.AuthorizationClient;
using Polly.Extensions.Http;

namespace GatewayAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        

        

        private void OnHalfOpen()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("Circuit in test mode, one request will be allowed.");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        private void OnReset()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\n\n CIRCUIT IS OPEN!!!! \n\n\n\n");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        private void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan ts)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            
            Console.WriteLine("\n\n\n\n CIRCUIT IS OPEN!!!! \n\n\n\n");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAutoMapper(typeof(Mapping.MappingProfile));
            //services.AddHangfire(x => x.UseSqlServerStorage("Data Source=BASEM-ПК\\SQLEXPRESS;Initial Catalog=TestJob;Integrated Security=True;Pooling=False"));
            services.AddHangfire(x => x.UseSqlServerStorage("Server=localhost;Database=TestJob;User Id=sa;Password=Mypassword123;"));

            services.AddHttpClient<IBusesHttpClient, BusesHttpClient>(client =>
           {
               client.BaseAddress = new Uri("https://localhost:44331/");
           }).AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3,TimeSpan.FromSeconds(20)));

            services.AddHttpClient<IPlanesHttpClient, PlanesHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44361/");
            }).AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(20)));
            //.AddTransientHttpErrorPolicy(f => f.FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)));

            //.HandleTransientHttpError()

            services.AddHttpClient<IFavoritesHttpClient, FavoritesHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44357/");
            }).AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(20)));
            services.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5051/");
            });

            services.AddCors();

            services.AddHangfireServer();


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
            app.UseCors(
               options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
           );
            app.UseAuthorization();
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
