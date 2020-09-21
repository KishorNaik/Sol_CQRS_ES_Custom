using Api.Cores.Base.Commands.Model;
using Api.Cores.Base.Models;
using System.Threading.Tasks;

namespace Api.Cores.Base.Commands.Handler
{
    public interface ICommandHandler<TCommand> where TCommand : class, ICommand, IModel
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : class, ICommand, IModel
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}