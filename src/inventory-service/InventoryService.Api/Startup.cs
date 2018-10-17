﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InventoryService.Api.Database;
using InventoryService.Api.Services;
using NSwag.AspNetCore;
using NJsonSchema;
using InventoryService.Api.Hubs;
using Newtonsoft.Json.Serialization;

namespace InventoryService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwagger();
            services.AddDbContext<InventoryContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("InventoryContext"));
            });
            services.AddCors();
            services.AddSignalR()
                .AddJsonProtocol(builder =>
                    builder.PayloadSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddScoped<InventoryManager>();
            services.AddScoped<IInventoryData, SqlInventoryData>();
            services.AddScoped<IInventoryNotificationService, SignalRInventoryNotificationService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
                settings.GeneratorSettings.Title = "Inventory Service";
            });
            app.UseSignalR(builder =>
            {
                builder.MapHub<InventoryHub>("/signalr/inventory");
            });
            app.UseMvc();
            app.UseFileServer("/www");
        }
    }
}
