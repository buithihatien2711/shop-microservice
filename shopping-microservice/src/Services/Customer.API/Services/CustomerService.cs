using AutoMapper;
using Customer.API.Entities;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            var customer = await _repository.GetCustomerByUserNameAsync(userName);
            if (customer == null)
            {
                return Results.NotFound();
            }
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Results.Ok(customerDto);
        }
    }
}
