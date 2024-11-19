using Shared.DTOs.Customer;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomerByUserNameAsync(string userName);

        Task<IResult> GetCustomersAsync();

        void CreateCustomerAsync(CreateCustomerDto customerDto);

        Task<IResult> UpdateCustomerAsync(UpdateCustomerDto customerDto);

        Task<IResult> DeleteCustomerAsync(int customerId);
    }
}
