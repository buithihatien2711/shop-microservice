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

        public void CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<CreateCustomerDto, CustomerEntity>(createCustomerDto);
            _repository.CreateCustomer(customer);
            _repository.SaveChangeAsync();
        }

        public async Task<IResult> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var customer = _mapper.Map<UpdateCustomerDto, CustomerEntity>(updateCustomerDto);
            await _repository.UpdateCustomer(customer);
            await _repository.SaveChangeAsync();
            var customerDto = _mapper.Map<CustomerEntity, CustomerDto>(customer);
            return Results.Ok(customerDto);
        }

        public async Task<IResult> DeleteCustomerAsync(int customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return Results.NotFound();
            }
            await _repository.DeleteCustomer(customer);
            await _repository.SaveChangeAsync();
            return Results.NoContent();
        }

        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            var customer = await _repository.GetCustomerByUserNameAsync(userName);
            if (customer == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(customer);
        }

        public async Task<IResult> GetCustomersAsync()
        {
            return Results.Ok(await _repository.GetCustomersAsync());
        }
    }
}
