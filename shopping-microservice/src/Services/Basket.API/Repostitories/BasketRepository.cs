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

        public async Task<CartEntity?> GetBasketByUserName(string username)
        {
            _logger.Information($"BEGIN: GetBasketByUserName {username}");
            var basket = await _redisCacheService.GetStringAsync(username);
            _logger.Information($"END: GetBasketByUserName {username}");
            return string.IsNullOrEmpty(basket) ? null : _serializeService.Deserilize<CartEntity>(basket);
        }

        public async Task<CartEntity> UpdateBasket(CartEntity cart, DistributedCacheEntryOptions options = null)
        {
            _logger.Information($"BEGIN: UpdateBasket for {cart.UserName}");
            if (options != null)
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
            }
            else
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
            }
            _logger.Information($"END: UpdateBasket for {cart.UserName}");
            return await GetBasketByUserName(cart.UserName);
        }

        public async Task<bool> DeleteBasketFromUserName(string username)
        {
            _logger.Information($"BEGIN: DeleteBasketFromUserName for {username}");
            try
            {
                await _redisCacheService.RemoveAsync(username);
                _logger.Information($"END: DeleteBasketFromUserName for {username}");
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
