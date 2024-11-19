using Contracts.Common;
using Customer.API.Entities;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBaseAsync<CustomerEntity, int, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            
        }

        public Task CreateCustomer(CustomerEntity customer)
        {
            return CreateAsync(customer);
        }

        public Task DeleteCustomer(CustomerEntity customer)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerEntity> GetCustomerByUserNameAsync(string userName)
        {
            return FindByCondition(x => x.UserName.Equals(userName)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersAsync()
        {
            return await FindAll().ToListAsync();
        }

        public Task UpdateCustomer(CustomerEntity customer)
        {
            return DeleteAsync(customer);
        }
    }
}
