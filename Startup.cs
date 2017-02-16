using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Fluent.Core.Middleware;
using Fluent.Core.Services.Context;
using Fluent.Core.Services.Localization;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Api.Calculator
{
    public class Startup {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add localization based on JSON files
            services.AddJsonLocalization(options => options.ResourcesPath = "Resources");

            // Add fluent context
            services.AddFluentContext();

            // Add framework services
            services
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            // Add API versioning
            services.AddApiVersioning();

            // Configure framework services
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("en-GB"),
                        new CultureInfo("en-US")
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole()
                .AddDebug();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseBrowserLink();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            // }

            // Run middleware to populate fluent context
            app.UseMiddleware<FluentCorrelationMiddleware>();

            //app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}