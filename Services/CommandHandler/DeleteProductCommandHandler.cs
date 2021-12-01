using FluentValidation.Results;
using Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Command;
using Services.DTO;
using Services.Repository;
using Services.Validator;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommandHandler
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, CommonResponse>
    {
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly DeleteProductValidator _validator;
        private readonly IRepository _repository;

        public DeleteProductCommandHandler(
            ILogger<DeleteProductCommandHandler> logger,
            DeleteProductValidator validator,
            IRepository repository
        )
        {
            _logger = logger;
            _validator = validator;
            _repository = repository;
        }
        public async Task<CommonResponse> HandleAsync(DeleteProductCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");

            ValidationResult validationResult = _validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return new CommonResponse
                {
                    Succeeded = false,
                    Validation = validationResult
                };
            }

            var result = await _repository.DeleteProduct(command.Id);

            return new CommonResponse
            {
                Succeeded = result
            };


        }
    }
}
