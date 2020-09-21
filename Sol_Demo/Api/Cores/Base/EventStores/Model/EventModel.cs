using Api.Cores.Base.Events.Model;
using System;

namespace Api.Cores.Base.EventStores.Model
{
    public class EventModel : IEvent
    {
        public decimal? EventId { get; set; }

        public Guid AggregateId { get; set; }

        public Guid StateId { get; set; }

        public String EventName { get; set; }

        public String NewPayLoad { get; set; }

        public String OldPayLoad { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}