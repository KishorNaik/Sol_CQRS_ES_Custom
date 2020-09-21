using Api.Business.Query.Queries;
using Api.Cores.Base.Api.Query;

namespace Api.Cores.Api.Queries
{
    public interface IGetMovieByTitleApiQueryHandler : IApiQueryHandler<GetMovieByTitleQuery>
    {
    }
}