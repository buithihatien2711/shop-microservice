using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repostitories.Interfaces
{
    public interface IBasketRepository
    {
        Task<CartEntity?> GetBasketByUserName(string userName);

        Task<CartEntity> UpdateBasket(CartEntity cart, DistributedCacheEntryOptions options = null);

        Task<bool> DeleteBasketFromUserName(string userName);
    }
}
