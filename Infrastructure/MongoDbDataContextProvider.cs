using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure
{
    public class MongoDbDataContextProvider : IMongoDbDataContextProvider
    {
        private readonly IMongoDatabase _database;
        public MongoDbDataContextProvider(IConfiguration configuration)
        {
            _database = new MongoClient(configuration["DataBase:ConnectionString"]).GetDatabase(configuration["DataBase:DbName"]);
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        public IQueryable<T> GetQueryableCollection<T>()
        {
            return _database.GetCollection<T>($"{typeof(T).Name}s").AsQueryable();
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>($"{typeof(T).Name}s");
        }
    }
}
