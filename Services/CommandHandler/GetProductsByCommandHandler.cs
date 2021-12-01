using Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Command;
using Services.DTO;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommandHandler
{
    public class GetProductsByCommandHandler : ICommandHandler<GetProductsCommand, GetProductsResponse>
    {
        private readonly ILogger<GetProductsByCommandHandler> _logger;
        private readonly IRepository _repository;

        public GetProductsByCommandHandler(
            ILogger<GetProductsByCommandHandler> logger,
            IRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<GetProductsResponse> HandleAsync(GetProductsCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");

            var result = await _repository.GetProducts(command);

            if(result == null)
            {
                return new GetProductsResponse
                {
                    Products = null,
                    TotalCount = 0,
                    Succeeded = false
                };
            }

            var totalCount = await _repository.GetProductsCount(command);

            return new GetProductsResponse
            {
                Products = result.ToList(),
                TotalCount = totalCount,
                Succeeded = true
            };

        }
    }
}
