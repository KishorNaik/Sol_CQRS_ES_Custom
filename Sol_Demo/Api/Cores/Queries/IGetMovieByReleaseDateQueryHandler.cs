﻿using Api.Business.Query.Queries;
using Api.Cores.Base.Query.Handler;

namespace Api.Cores.Queries
{
    public interface IGetMovieByReleaseDateQueryHandler : IQueryHandler<GetMovieByReleaseDateQuery, object>
    {
    }
}