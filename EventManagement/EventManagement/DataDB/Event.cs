using System;
using System.Collections.Generic;

namespace EventManagement.DataDB
{
    public partial class Event
    {
        public Guid Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public decimal? Price { get; set; }
    }
}
