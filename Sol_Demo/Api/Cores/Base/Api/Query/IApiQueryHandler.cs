using Api.Cores.Base.Query.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Cores.Base.Api.Query
{
    public interface IApiQueryHandler
    {
        Task<IActionResult> HandleAsync(ControllerBase controllerBase);
    }

    public interface IApiQueryHandler<TQuery> where TQuery : class, IQuery
    {
        Task<IActionResult> HandleAsync(ControllerBase controllerBase, TQuery query);
    }
}