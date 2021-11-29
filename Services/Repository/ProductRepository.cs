using Infrastructure;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Services.DTO;
using Services.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ProductRepository : IRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoDbDataContextProvider _mongoDb;

        public ProductRepository(
            ILogger<ProductRepository> logger,
            IMongoDbDataContextProvider mongoDb
        )
        {
            _logger = logger;
            _mongoDb = mongoDb;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                var collection = _mongoDb.GetCollection<Product>();
                await collection.InsertOneAsync(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product Creation Error {DateTime.Now}: {ex.Message}");
                return false;
            }    
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                var collection = _mongoDb.GetCollection<Product>();

                var filter = Builders<Product>.Filter.Eq<string>(x => x.Id, id);

                var result = await collection.DeleteOneAsync(filter);

                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Product Deletation Error {DateTime.Now}: {ex.Message}");
                return false;
            }
        }

        public async Task<Product> GetProductById(string id)
        {
            try
            {
                var collection = _mongoDb.GetCollection<Product>();

                var filter = Builders<Product>.Filter.Eq<string>(x => x.Id, id);

                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Get Product Error {DateTime.Now}: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts(GetProducts payload)
        {
            try
            {
                var collection = _mongoDb.GetCollection<Product>();

                FindOptions<Product> findOptions = new FindOptions<Product>()
                {
                    Limit = payload.PageNo,
                    Skip = payload.PageNo * payload.Page
                };

                return (await collection.FindAsync(_ => true, findOptions)).ToEnumerable();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Get Products Error {DateTime.Now}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var collection = _mongoDb.GetCollection<Product>();

                var filter = Builders<Product>.Filter.Eq<string>(x => x.Id, product.Id);

                var result = await collection.ReplaceOneAsync(filter, product);

                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Product Update Error {DateTime.Now}: {ex.Message}");
                return false;
            }
        }
    }
}
