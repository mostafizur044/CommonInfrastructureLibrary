using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class QueryHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> SubmitAsync<TQuery, TResult>(TQuery query)
        {
            var querydHandler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) as IQueryHandler<TQuery, TResult>;
            return querydHandler.HandleAsync(query);
        }
    }
}
