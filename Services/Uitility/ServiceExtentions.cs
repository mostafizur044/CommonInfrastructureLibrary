using Microsoft.Extensions.DependencyInjection;
using Services.Repository;
using Services.Validator;

namespace Services.Uitility
{
    public static class ServiceExtentions
    {
        public static void ConfigueProductServices(this IServiceCollection services)
        {
            services.AddSingleton<IRepository, ProductRepository>();

            services.AddSingleton<CreateProductValidator>();
            services.AddSingleton<DeleteProductValidator>();
        }
    }
}
