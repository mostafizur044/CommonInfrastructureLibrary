using Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.DTO;
using Services.Query;
using Services.Repository;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Services.QueryHandler
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IRepository _repository;

        public GetProductsQueryHandler(
            ILogger<GetProductsQueryHandler> logger,
            IRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<GetProductsResponse> HandleAsync(GetProductsQuery query)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(query)}");

            var result = await _repository.GetProducts(query);

            if (result == null)
            {
                return new GetProductsResponse
                {
                    Products = null,
                    TotalCount = 0,
                    Succeeded = false
                };
            }

            var totalCount = await _repository.GetProductsCount(query);

            return new GetProductsResponse
            {
                Products = result.ToList(),
                TotalCount = totalCount,
                Succeeded = true
            };
        }
    }
}
