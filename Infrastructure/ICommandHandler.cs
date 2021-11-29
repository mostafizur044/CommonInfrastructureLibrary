using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ICommandHandler<TCommand, TResponse>  
    {  
        Task<TResponse> HandleAsync(TCommand command);
    }  
}
