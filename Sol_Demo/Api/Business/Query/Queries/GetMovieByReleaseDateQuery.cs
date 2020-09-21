using Api.Cores.Base.Query.Model;
using System;
using System.Runtime.Serialization;

namespace Api.Business.Query.Queries
{
    [DataContract]
    public class GetMovieByReleaseDateQuery : IQuery
    {
        [DataMember(EmitDefaultValue = false)]
        public DateTime? ReleaseStartDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? ReleaseEndDate { get; set; }
    }
}