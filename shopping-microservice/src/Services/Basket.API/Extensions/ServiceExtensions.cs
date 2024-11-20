using Basket.API.Repostitories;
using Basket.API.Repostitories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;

namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services.AddScoped<IBasketRepository, BasketRepository>()
                           .AddTransient<ISerializeService, SerializeService>();
        }

        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new ArgumentNullException("Redis connection string is not configured.");
            }
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = redisConnectionString;
            });
        }
    }
}
