using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Command;
using Services.DTO;
using Services.Query;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly CommandHandler _commandHandler;
        //private readonly QueryHandler _queryHandler;

        public ProductController(
            ILogger<ProductController> logger,
            CommandHandler commandHandler
            //QueryHandler queryHandler
        )
        {
            _logger = logger;
            _commandHandler = commandHandler;
            //_queryHandler = queryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> GetProductsByCommand([FromBody] GetProductsCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");
            var result = await _commandHandler.SubmitAsync<GetProductsCommand, GetProductsResponse>(command);
            if(result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetProductsByQuery([FromQuery] GetProductsQuery query)
        //{
        //    _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(query)}");

        //    var result = await _queryHandler.SubmitAsync<GetProductsQuery, GetProductsResponse>(query);
        //    if (result.Succeeded)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetProduct([FromRoute] GetProductQuery query)
        //{
        //    _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(query)}");

        //    var result = await _queryHandler.SubmitAsync<GetProductQuery, GetProductResponse>(query);

        //    if (result.Succeeded)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");
            var result = await _commandHandler.SubmitAsync<CreateProductCommand, CommonResponse>(command);
            if (result.Validation == null && !result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");
            var result = await _commandHandler.SubmitAsync<UpdateProductCommand, CommonResponse>(command);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().DeclaringType.FullName} - {MethodBase.GetCurrentMethod().Name} - Time {DateTime.Now} for command - {JsonConvert.SerializeObject(command)}");
            var result = await _commandHandler.SubmitAsync<DeleteProductCommand, CommonResponse>(command);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
