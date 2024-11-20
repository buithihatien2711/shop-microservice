using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<ProductEntity, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            
        }

        public Task<ProductEntity> GetProduct(long id)
        {
            return GetByIdAsync(id);
        }

        public Task<ProductEntity> GetProductByNo(string productNo)
        {
            return FindByCondition(p => p.No.Equals(productNo)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            return await FindAll().ToListAsync();
        }

        public Task CreateProduct(ProductEntity product)
        {
            return CreateAsync(product);
        }

        public Task UpdateProduct(ProductEntity product)
        {
            return UpdateAsync(product);
        }

        public async Task DeleteProduct(long id)
        {
            var product = await GetProduct(id);
            if (product != null)
            {
                DeleteAsync(product);
            }
        }
    }
}
