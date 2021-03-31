using HistoricalTrainApi.Models;
using HistoricalTrainApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace HistoricalTrainApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var username = Configuration["Credentials:username"];
            var password = Configuration["Credentials:password"];

            var base64EncodedAuthenticationString = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            services.AddHttpClient("darwin", c =>
            {
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            });

            services.AddOptions<HistoricServiceUris>()
            .Bind(Configuration.GetSection(nameof(HistoricServiceUris)));

            services.AddSingleton<IHistoricServiceRepository, HistoricServiceRepository>();
            services.AddSingleton<IStationCodeRepository, StationCodeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Trains!");
                });
                endpoints.MapControllers();
            });
        }
    }
}
