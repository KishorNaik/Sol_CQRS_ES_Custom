using Api.Cores.Base.Commands.Model;
using System;
using System.Runtime.Serialization;

namespace Api.Business.Command.Commands
{
    [DataContract]
    public class MovieDeleteCommand : ICommand
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid AggregateId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid StateId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid MovieIdentity { get; set; }
    }
}