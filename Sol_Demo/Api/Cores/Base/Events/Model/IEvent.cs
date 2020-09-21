using Api.Cores.Base.Models;
using System;

namespace Api.Cores.Base.Events.Model
{
    public interface IEvent : IAggregateModel, IStateModel
    {
        public decimal? EventId { get; set; }

        public String EventName { get; set; }

        public String NewPayLoad { get; set; }

        public String OldPayLoad { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}