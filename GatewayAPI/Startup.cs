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
            Console.WriteLine("Circuit in test mode, one request will be allowed.");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        private void OnReset()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Circuit closed, requests flow normally.");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        private void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan ts)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Circuit cut, requests will not flow.");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            services.AddAutoMapper(typeof(Mapping.MappingProfile));
            services.AddHangfire(x => x.UseSqlServerStorage("Data Source=BASEM-ой\\SQLEXPRESS;Initial Catalog=TestJob;Integrated Security=True;Pooling=False"));

            var basicCircuitBreakerPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode == System.Net.HttpStatusCode.BadRequest)
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30), OnBreak, OnReset, OnHalfOpen);

            services.AddHttpClient<IBusesHttpClient, BusesHttpClient>(client =>
           {
               client.BaseAddress = new Uri("https://localhost:44331/");
           }).AddPolicyHandler(basicCircuitBreakerPolicy);

            services.AddHttpClient<IPlanesHttpClient, PlanesHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44361/");
            }).AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromSeconds(15), OnBreak, OnReset, OnHalfOpen)).AddTransientHttpErrorPolicy(f => f.FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)));

            services.AddHttpClient<IFavoritesHttpClient, FavoritesHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44357/");
            });//.AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromSeconds(15)));
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
