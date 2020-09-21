using Api.Cores.Base.Models;

namespace Api.Cores.Base.Commands.Model
{
    public interface ICommand : IAggregateModel, IStateModel
    {
    }
}