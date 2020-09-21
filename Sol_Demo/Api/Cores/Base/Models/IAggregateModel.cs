using System;

namespace Api.Cores.Base.Models
{
    public interface IAggregateModel : IModel
    {
        Guid AggregateId { get; set; }
    }
}