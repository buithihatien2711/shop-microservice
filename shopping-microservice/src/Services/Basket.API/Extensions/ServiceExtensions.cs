using Basket.API.Repostitories;
using Basket.API.Repostitories.Interfaces;
using Contracts.Common.Interfaces;
using EventBus.Messages.Interfaces;
using Infrastructure.Common;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;

namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();
            var cacheSettings = configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>();

            services.AddSingleton<EventBusSettings>(eventBusSettings);

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services.AddScoped<IBasketRepository, BasketRepository>()
                           .AddTransient<ISerializeService, SerializeService>();
        }

        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            //var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
            var settings = services.GetOptions<CacheSettings>("CacheSettings");
            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentNullException("Redis connection string is not configured.");
            }
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = settings.ConnectionString;
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
            if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
            {
                throw new ArgumentNullException("EventBusSettings is not configured");
            };

            var mqConnect = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnect);
                });
                config.AddRequestClient<IBasketCheckoutEvent>();
            });
        }
    }
}
