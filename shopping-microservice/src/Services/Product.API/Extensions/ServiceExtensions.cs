﻿using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Product.API.Persistence;
using Product.API.Repositories;
using Product.API.Repositories.Interfaces;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureProductDbContext(configuration);
            services.AddInfrastrureServices();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            return services;
        }

        private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var builder = new MySqlConnectionStringBuilder(connectionString);

            services.AddDbContext<ProductContext>(m => m.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString), e =>
            {
                e.MigrationsAssembly("Product.API");
                e.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore);
            }));

            return services;
        }

        private static IServiceCollection AddInfrastrureServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
