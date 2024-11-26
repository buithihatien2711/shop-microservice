using Contracts.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Common.Interfaces
{

    public interface IOrderRepository : IRepositoryBaseAsync<OrderEntity, long>
    {
        Task<OrderEntity?> GetOrderById(long id);

        Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string username);

        Task<long> CreateOrder(OrderEntity order);

        Task UpdateOrder(OrderEntity order);

        Task DeleteOrder(OrderEntity order);
    }
}
