using System;
using System.Collections.Generic;

namespace EventManagement.DataDB
{
    public partial class Role
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
    }
}
