using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.Services;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using MineSweeper.Infra.Context;
using MineSweeper.Infra.Repositories;
using MineSweeper.Infra.UoW;
using System;

namespace MineSweeper.API
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
            services.AddControllers();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MineSweeper API",
                    Description = "Swagger documentation",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Guerreiro",
                        Email = "pedro.prcgm@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/pedro-guerreiro-b43789101/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/pedroprcgm/minesweerper-api/blob/main/LICENSE.md")
                    }
                });
            });

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "MineSweeper API v1.0");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMineSweeperContext, MineSweeperContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IGameRepository, GameRepository>();

            // Services
            services.AddScoped<IGameAppService, GameAppService>();
        }
    }
}
