using Api.Cores.Base.Models;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Api.Models
{
    [DataContract]
    public class MovieModel : IAggregateModel, IStateModel
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid AggregateId { get; set; }

        [JsonIgnore]
        public Guid StateId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid MovieIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Title { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? ReleaseDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean? IsDelete { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? ReleaseStartDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? ReleaseEndDate { get; set; }
    }
}