using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Customer
{
    public class UpdateCustomerDto 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
