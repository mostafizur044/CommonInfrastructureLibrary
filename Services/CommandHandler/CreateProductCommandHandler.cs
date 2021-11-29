using FluentValidation.Results;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Command;
using Services.DTO;
using Services.Entity;
using Services.Repository;
using Services.Validator;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommandHandler
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CommonResponse>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly CreateProductValidator _validator;
        private readonly IRepository _repository;

        public CreateProductCommandHandler(
            ILogger<CreateProductCommandHandler> logger,
            CreateProductValidator validator,
            IRepository repository
        )
        {
            _logger = logger;
            _validator = validator;
            _repository = repository;
        }
        public async Task<CommonResponse> HandleAsync(CreateProductCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");

            ValidationResult validationResult = _validator.Validate(command);

            if(!validationResult.IsValid)
            {
                return new CommonResponse
                {
                    Succeeded = false,
                    Validation = validationResult
                };
            }

            var product = new Product
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                SKU = command.SKU,
                ImageUrl = command.ImageUrl,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            var result = await _repository.CreateProduct(product);

            if(result)
            {
                return new CommonResponse
                {
                    Succeeded = true
                };
            }

            return new CommonResponse
            {
                Succeeded = false
            };

        }
    }
}
