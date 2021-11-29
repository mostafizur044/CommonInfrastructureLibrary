using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CommandHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> SubmitAsync<TCommand, TResponse>(TCommand command)
        {
            var commandHandler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResponse>)) as ICommandHandler<TCommand, TResponse>;
            return commandHandler.HandleAsync(command);
        }
    }
}
