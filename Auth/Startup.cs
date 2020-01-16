using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth
{
    public class Startup
    {
        private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options => {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = false,
                          ValidateAudience = false,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          ValidIssuer = "https://localhost:5001",
                          ValidAudience = "https://localhost:5001",
                          LifetimeValidator = LifetimeValidator,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                      };
                  });


            services.AddDbContextPool<DbContext>(options => options.UseSqlServer("Server = localhost; Database = TokenDB; User Id = sa; Password = Mypassword123;"));

            //services.AddDbContextPool<DbContext>(options => options.UseSqlServer("Data Source=BASEM-ПК\\SQLEXPRESS;Initial Catalog=TokenDB;Integrated Security=True;Pooling=False"));

            services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddDbContextPool<BusDbContext>(options => options.UseSqlServer("Data Source=BASEM-ПК\\SQLEXPRESS;Initial Catalog=BusesDB;Integrated Security=True;Pooling=False"));
            services.AddCors();

            /*services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Testing JWT with Web API",
                    Version = "v1",

                });
            });*/


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSwagger();
            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API JWT v1"); });


            app.UseHttpsRedirection();
            app.UseCors(
               options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
           );
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
