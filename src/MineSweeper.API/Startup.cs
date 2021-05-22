using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.Services;
using MineSweeper.CrossCutting.Auth.Facades;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Facades;
using MineSweeper.Domain.Interfaces.Repositories;
using MineSweeper.Domain.Settings;
using MineSweeper.Infra.Context;
using MineSweeper.Infra.Repositories;
using MineSweeper.Infra.Settings;
using MineSweeper.Infra.UoW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            DatabaseConnectionSettings databaseSettings = Configuration.GetSection(nameof(DatabaseConnectionSettings)).Get<DatabaseConnectionSettings>();
            TokenConfigurationsSettings tokenConfigurationsSettings = Configuration.GetSection(nameof(TokenConfigurationsSettings)).Get<TokenConfigurationsSettings>();

            services.AddIdentity<User, Role>()
                .AddMongoDbStores<User, Role, Guid>
                (
                    databaseSettings.ConnectionString,
                    databaseSettings.Database
                )
                .AddDefaultTokenProviders();

            services.AddControllers();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfigurationsSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
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

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Name = "Authorization",
                        Description = "Insert your bearer token"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

                options.IncludeXmlComments(Path.Combine("wwwroot", "api-docs.xml"));
            });

            services.AddHttpContextAccessor();

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

            app.UseAuthentication();

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

            // Settings
            services.Configure<DatabaseConnectionSettings>(Configuration.GetSection(nameof(DatabaseConnectionSettings)));

            // Repositories
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IGameAppService, GameAppService>();
            services.AddScoped<IUserAppService, UserAppService>();

            // Facades
            services.AddScoped<IAuthFacade, AuthFacade>();
        }
    }
}
