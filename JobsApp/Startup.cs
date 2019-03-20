using JobsApp.Migrations;
using JobsApp.Services;
using JobsApp.Services.DatabaseService;
using JobsApp.Services.DatabaseService.Impl;
using JobsApp.Services.HeadHunterService;
using JobsApp.Services.HeadHunterService.Impl;
using JobsApp.Services.Impl;
using JobsApp.Services.StoreService;
using JobsApp.Services.StoreService.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobsApp
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
            services.AddSingleton(_ => new HeadHunterServiceConfig()
            {
                ApiUrl = Configuration.GetValue<string>("HeadHunterApiUrl"),
                AreaId = Configuration.GetValue<string>("HeadHunterAreaId"),
                PerPage = Configuration.GetValue<int>("HeadHunterPerPage"),
            });

            var databaseSettings = new DatabaseServiceConfig()
            {
                ConnectionString = Configuration.GetValue<string>("PotsgresConnectionString")
            };
            services.AddSingleton<Migrator>();
            services.AddSingleton(_ => databaseSettings);
            services.AddTransient<IHeadHunterService, HeadHunterService>();
            services.AddTransient<IHeadHunterVacancyConverter, HeadHunterVacancyConverter>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IDatabaseService, DatabaseService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
