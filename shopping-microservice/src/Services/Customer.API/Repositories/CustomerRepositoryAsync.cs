using Contracts.Common;
using Customer.API.Entities;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryQueryBase<CustomerEntity, int, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext) : base(dbContext)
        {
            
        }

        public Task<CustomerEntity> GetCustomerByUserNameAsync(string userName)
        {
            return FindByCondition(x => x.UserName.Equals(userName)).SingleOrDefaultAsync();
        }
    }
}
