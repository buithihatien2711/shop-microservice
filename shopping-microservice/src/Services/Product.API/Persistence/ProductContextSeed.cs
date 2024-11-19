using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
        {
            if (!productContext.Products.Any())
            {
                productContext.AddRange(GetCatalogProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seeded data for Product DB associated with context {DbContextName}", nameof(ProductContext));
            }
        }

        private static IEnumerable<ProductEntity> GetCatalogProducts()
        {
            return new List<ProductEntity>()
            {
                new()
                {
                    No = "Lotus",
                    Name = "Esprit",
                    Summary = "summary lotus",
                    Description = "description lotus",
                    Price = (decimal) 177940.49
                },
                new()
                {
                    No = "Cadillac",
                    Name = "CTS",
                    Summary = "summary cadillac",
                    Description = "description cadillac",
                    Price = (decimal) 114628.21
                },
            };
        }
    }
}
