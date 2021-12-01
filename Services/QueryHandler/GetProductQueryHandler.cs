using Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.DTO;
using Services.Query;
using Services.Repository;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.QueryHandler
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResponse>
    {
        private readonly ILogger<GetProductQueryHandler> _logger;
        private readonly IRepository _repository;

        public GetProductQueryHandler(
            ILogger<GetProductQueryHandler> logger,
            IRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<GetProductResponse> HandleAsync(GetProductQuery query)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(query)}");

            var result = await _repository.GetProductById(query.Id);

            if (result == null)
            {
                return new GetProductResponse
                {
                    Product = null,
                    Succeeded = false
                };
            }

            return new GetProductResponse
            {
                Product = result,
                Succeeded = true
            };
        }
    }
}
