using AutoMapper;
using BusinessManager.DataAccess.Abstractions;
using BusinessManager.DataAccess.Repositories;
using BusinessManager.DataAccess.UnitOfWork;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManagerApi.Data;
using BusinessManagerApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace BusinessManagerApi
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));

            RegisterRepositories(services);

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddControllers().AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );

            // Register the Swagger Generator service. This service is responsible for genrating Swagger Documents.
            // Note: Add this service at the end after AddMvc() or AddMvcCore().
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Business Manager API",
                    Version = "v1",
                    Description = "This project is made by Milos Mitrovic",
                    Contact = new OpenApiContact
                    {
                        Name = "Milos Mitrovic",
                        Email = "milos.mitrovicbgnp@gmail.com",
                        Url = new Uri("https://instagram.com/milosbgnp"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            //app.ConfigureExceptionHandler();
            app.ConfigureCustomExceptionMiddleware();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Manager API v1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IClientsRepository, ClientsRepository>();
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
        }
    }
}
