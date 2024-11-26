using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBaseAsync<OrderEntity, long, OrderContext>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext, IUnitOfWork<OrderContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            
        }
        public async Task<OrderEntity?> GetOrderById(long id)
        {
            var result = await FindByCondition(o => o.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string username)
        {
            var result = await FindByCondition(x => x.UserName.Equals(username)).ToListAsync();
            return result;
        }

        public async Task<long> CreateOrder(OrderEntity order)
        {
            var id = await CreateAsync(order);
            return id;
        }

        public async Task UpdateOrder(OrderEntity order)
        {
            await UpdateAsync(order);           
        }

        public async Task DeleteOrder(OrderEntity order)
        {
            await DeleteAsync(order);
        }
    }
}
