using MongoDB.Driver;
using System.Linq;

namespace Infrastructure
{
    public interface IMongoDbDataContextProvider
    {
        IMongoDatabase GetDatabase();
        IQueryable<T> GetQueryableCollection<T>();
    }
}
