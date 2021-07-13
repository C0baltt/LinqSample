using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting;
using Accounting.Tracking;
using Common;
using Currencies.Apis.Byn;
using Currencies.Apis.Rub;
using Currencies.Common;
using Currencies.Common.Caching;
using Currencies.Common.Conversion;
using Currencies.Common.Infos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication4
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
            services.AddTransient<IEventBus, EventBus>();
            services.AddSingleton<ICurrenciesApiCacheService, CurrenciesApiCacheService>();
            services.AddSingleton<IAccountsRepository, AccountsRepository>();
            services.AddSingleton<IAccountOperationsTrackingService, AccountOperationsTrackingService>();
            services.AddSingleton<GetNowAtSite>(() => DateTime.UtcNow);

            RegisterCurrenciesApi(services);

            services.AddTransient<ICurrencyInfoService, CurrencyInfoService>();
            services.AddTransient<IAccountAcquiringService, AccountAcquiringService>();
            services.AddTransient<ICurrencyConversionService, CurrencyConversionService>();
            services.AddTransient<IAccountTransferService, AccountTransferService>();
            services.AddTransient<IAccountManagementService, AccountManagementService>();

            // scoped

            services.AddControllersWithViews();
        }

        private void RegisterCurrenciesApi(IServiceCollection services)
        {
            switch (Configuration["CurrencyApi"])
            {
                case "BYN":
                    services.AddTransient<ICurrenciesApi, BynCurrenciesApi>();
                    break;
                case "RUB":
                    services.AddTransient<ICurrenciesApi, RubCurrenciesApi>();
                    break;
                default:
                    throw new InvalidOperationException("Cannot register currencies API: settings not provided.");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
