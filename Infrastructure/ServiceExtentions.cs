using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Infrastructure
{
    public static class ServiceExtentions
    {
        public static void AddCommandQueryHandlers(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(ServiceExtentions).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<CommandHandler>();
            services.AddSingleton<QueryHandler>();
            services.AddSingleton<IMongoDbDataContextProvider, MongoDbDataContextProvider>();
        }
    }
}
