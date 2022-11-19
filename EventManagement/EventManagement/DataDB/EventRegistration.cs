using System;
using System.Collections.Generic;

namespace EventManagement.DataDB
{
    public partial class EventRegistration
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Event1Id { get; set; }
        public int? Event2Id { get; set; }
        public int? Event3Id { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
