using Services.DTO;
using Services.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProducts(GetProducts payload);
        Task<long> GetProductsCount(GetProducts payload);
        Task<Product> GetProductById(string id);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
