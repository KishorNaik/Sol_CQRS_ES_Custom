using Api.Cores.Base.Models;
using Api.Cores.Base.Query.Model;
using System.Threading.Tasks;

namespace Api.Cores.Base.Query.Handler
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery, IModel
    {
        Task<TResult> HandleAsync(TQuery query);
    }

    public interface IQueryHandler<TResult>
    {
        Task<TResult> HandleAsync();
    }
}