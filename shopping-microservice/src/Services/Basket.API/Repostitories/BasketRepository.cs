using Basket.API.Entities;
using Basket.API.Repostitories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repostitories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger;
        }

        public async Task<CartEntity?> GetBasketByUserName(string userName)
        {
            var basket = await _redisCacheService.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null : _serializeService.Deserilize<CartEntity>(basket);
        }

        public async Task<CartEntity> UpdateBasket(CartEntity cart, DistributedCacheEntryOptions options = null)
        {
            if (options != null)
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
            }
            else
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
            }
            return await GetBasketByUserName(cart.UserName);
        }

        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await _redisCacheService.RemoveAsync(userName);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("DeleteBasketFromUserName: " + e.Message);
                throw;
            }
        }
    }
}
