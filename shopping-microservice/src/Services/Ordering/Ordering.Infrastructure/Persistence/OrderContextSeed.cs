using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        private readonly ILogger _logger;
        private readonly OrderContext _context;

        public OrderContextSeed(ILogger logger, OrderContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "An error occured while initialising the database");
                throw;
            }
        }
        
        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Orders.Any())
            {
                await _context.Orders.AddRangeAsync(new OrderEntity()
                {
                    UserName = "Customer1",
                    FirstName = "customer1",
                    LastName = "customer",
                    EmailAddress = "customer1@local.com",
                    ShippingAddress = "Da Nang",
                    InvoiceAddress = "Quang Tri",
                    TotalPrice = 250
                });
            }
        }
    }
}
