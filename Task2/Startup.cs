using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task2.Data;
using Task2.Models;
using Task2.Services;
using Task2.Services.Abstractions;

namespace Task2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            try
            {
                builder.AddXmlFile("appsettings.xml");

            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                Configuration = builder.Build();
            }

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<ISettingsService, SettingsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitAppSettings();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void InitAppSettings()
        {
            int refreshTime = Configuration.GetValue(nameof(FeedReaderSettings.RefreshTime), 60);
            FeedReaderSettings.RefreshTime = refreshTime < 60 ? 60 : refreshTime;

            FeedReaderSettings.SupportFormatting = Configuration.GetValue(nameof(FeedReaderSettings.SupportFormatting), true);

            FeedReaderSettings.Sources = Configuration.GetValue(nameof(FeedReaderSettings.Sources),
                new List<RssSource> {new RssSource("https://habr.com/ru/rss/all/all/", true)});

        }
    }
}