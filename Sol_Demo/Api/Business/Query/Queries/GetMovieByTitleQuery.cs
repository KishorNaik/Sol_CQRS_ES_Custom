using Api.Cores.Base.Query.Model;
using System;
using System.Runtime.Serialization;

namespace Api.Business.Query.Queries
{
    [DataContract]
    public class GetMovieByTitleQuery : IQuery
    {
        [DataMember(EmitDefaultValue = false)]
        public String Title { get; set; }
    }
}