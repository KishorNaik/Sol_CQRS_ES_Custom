using Api.Cores.Base.Commands.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Cores.Base.Api.Command
{
    public interface IApiCommandHandler<TCommand> where TCommand : class, ICommand
    {
        Task<IActionResult> HandleAsync(ControllerBase controllerBase, TCommand command);
    }
}