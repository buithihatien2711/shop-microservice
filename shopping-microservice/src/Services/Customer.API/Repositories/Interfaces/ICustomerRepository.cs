using Contracts.Common.Interfaces;
using Customer.API.Entities;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryQueryBase<CustomerEntity, int, CustomerContext>
    {
        Task<CustomerEntity> GetCustomerByUserNameAsync(string userName);
    }
}
