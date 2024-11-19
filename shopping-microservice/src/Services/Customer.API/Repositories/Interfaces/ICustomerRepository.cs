using Contracts.Common;
using Customer.API.Entities;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<CustomerEntity, int, CustomerContext>
    {
        Task<CustomerEntity> GetCustomerByUserNameAsync(string userName);

        Task<IEnumerable<CustomerEntity>> GetCustomersAsync();

        Task CreateCustomer(CustomerEntity customer);

        Task UpdateCustomer(CustomerEntity customer);

        Task DeleteCustomer(CustomerEntity customer);
    }
}
