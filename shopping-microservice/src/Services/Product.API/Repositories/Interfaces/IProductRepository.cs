using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBaseAsync<ProductEntity, long, ProductContext>
    {
        Task<IEnumerable<ProductEntity>> GetProducts();

        Task<ProductEntity> GetProduct(long id);

        Task<ProductEntity> GetProductByNo(string productNo);

        Task CreateProduct(ProductEntity product);

        Task UpdateProduct(ProductEntity product);

        Task DeleteProduct(long id);
    }
}
