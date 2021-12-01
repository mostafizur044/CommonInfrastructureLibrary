using FluentValidation.Results;
using Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Command;
using Services.DTO;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommandHandler
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, CommonResponse>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IRepository _repository;

        public UpdateProductCommandHandler(
            ILogger<UpdateProductCommandHandler> logger,
            IRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<CommonResponse> HandleAsync(UpdateProductCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");

            var product = await _repository.GetProductById(command.Id);

            if (product == null)
            {
                var failure = new ValidationFailure[] { new ValidationFailure("Id", "Product not found") };
                return new CommonResponse
                {
                        Succeeded = false,
                        Validation = new ValidationResult(failure)
                };
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.SKU = command.SKU;
            product.ImageUrl = command.ImageUrl;
            product.UpdateDate = DateTime.Now;

            var result = await _repository.UpdateProduct(product);

            return new CommonResponse
            {
                Succeeded = result
            };
        }
    }
}
