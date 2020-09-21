using System;

namespace Api.Cores.Base.Models
{
    public interface IStateModel : IModel
    {
        Guid StateId { get; set; }
    }
}