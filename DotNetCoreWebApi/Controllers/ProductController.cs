using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWebApi.Controllers
{
    public class ProductController : ControllerBase
    {
        public ProductController(
            ILogger<ProductController> logger,
            CommandHandler commandHandler,
            QueryHandler queryHandler
        )
        {
            _logger = logger;
        }
    }
}
